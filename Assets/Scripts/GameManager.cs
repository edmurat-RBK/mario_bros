using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDM_Luigi;
using EDM_Mario;

public class GameManager : MonoBehaviour
{
    // Managers
    private LuigiManager luigiManager;
    private MarioManager marioManager;
    private TruckManager truckManager;

    [Header("Game loop")]
    public float gameLoopSpeed;
    public int tickCount;
    public bool gameLoopActive = true;

    [Header("Boxes")]
    public float boxSpawnChance;
    public int spawnTickRate;
    [HideInInspector] public List<Box> conveyorBelt;

    [Header("Tilt position")]
    public List<int> luigiTiltPosition;
    public List<int> marioTiltPosition;

    [Header("Scores")]
    public int score = 0;
    public int miss = 0;
    public bool chanceTime = false;
    public bool event300point = false;

    [Header("Truck")]
    public int boxLoaded = 0;
    public bool dropInFirstColumn = true; // First column is the left one, second is the right one

    // 7-segment digit displays
    private SevenDigitDisplay digitDisplay1;
    private SevenDigitDisplay digitDisplay10;
    private SevenDigitDisplay digitDisplay100;

    private void Start()
    {
        // Get Manager
        luigiManager = GameObject.FindGameObjectWithTag("LuigiManager").GetComponent<LuigiManager>();
        marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
        truckManager = GameObject.FindGameObjectWithTag("TruckManager").GetComponent<TruckManager>();

        // Get digit displays
        digitDisplay1 = GameObject.FindGameObjectWithTag("DigitDisplay1").GetComponent<SevenDigitDisplay>();
        digitDisplay10 = GameObject.FindGameObjectWithTag("DigitDisplay10").GetComponent<SevenDigitDisplay>();
        digitDisplay100 = GameObject.FindGameObjectWithTag("DigitDisplay100").GetComponent<SevenDigitDisplay>();

        // Initialise convoyor belt as a list of "Box"
        conveyorBelt = new List<Box>();

        // Start game loop
        tickCount = 0;
        StartCoroutine(InitLoop());
    }

    private void Update()
    {
        // Send correct digit to 7-digit displays
        UpdateScoreDisplay();

        // Trigger "300-point events"
        Update300PointEvent();

        UpdateTruckLoaded();
    }

    // Send correct digit to each 7-segment digit displays
    private void UpdateScoreDisplay()
    {
        // Maximum score is 999
        if (score <= 999)
        {
            if (score <= 9)
            {
                // Only the first digit needs to be active
                // digitToPrint = -1 disable the display
                digitDisplay100.digitToPrint = -1;
                digitDisplay10.digitToPrint = -1;
                digitDisplay1.digitToPrint = score % 10;
            }
            else if (score <= 99)
            {
                // Only the two first digit needs to be active
                digitDisplay100.digitToPrint = -1;
                digitDisplay10.digitToPrint = (int)Mathf.Floor(score % 100 / 10);
                digitDisplay1.digitToPrint = score % 10;
            }
            else
            {
                // All displays are active
                digitDisplay100.digitToPrint = (int)Mathf.Floor(score / 100);
                digitDisplay10.digitToPrint = (int)Mathf.Floor(score % 100 / 10);
                digitDisplay1.digitToPrint = score % 10;
            }
        }
        // If score is above 999, display 999
        else
        {
            digitDisplay100.digitToPrint = 9;
            digitDisplay10.digitToPrint = 9;
            digitDisplay1.digitToPrint = 9;
        }
    }

    // Check if 300-point event need to be launch
    private void Update300PointEvent()
    {
        // If player hits 300 points and the event hasn't triggered yet
        if (score == 300 && !event300point)
        {
            // If there is no miss, when 300 points are hit, active "Chance Time"
            if (miss <= 0)
            {
                chanceTime = true;
            }
            // If there is at least one miss, when 300 points are hit, reset all misses
            else
            {
                miss = 0;
            }

            // Tell that the 300-point event has been triggered
            event300point = true;
        }
    }


    private void UpdateTruckLoaded()
    {
        if(boxLoaded == 8 && gameLoopActive)
        {
            gameLoopActive = false;
            StartCoroutine(TakeBreakLoop());
        }
    }


    IEnumerator GameLoop()
    {
        while (gameLoopActive)
        {
            Tick();

            // Reset player sprites on "unstable" states
            luigiManager.ResetState();
            marioManager.ResetState();
            // Destroy boxes on falling state
            YellAtSomeone(KillBox());

            switch (tickCount%4) 
            {
                case 0:
                    MoveBoxInTruck();
                    MoveOddConvoyer();
                    break;

                case 2:
                    MoveBoxInTruck();
                    MoveEvenConvoyer();
                    break;

                default:
                    // Nothing
                    break;
            }

            if (tickCount%spawnTickRate == 0) { SpawnBox(); }
            yield return new WaitForSeconds(gameLoopSpeed / 4);
        }
    }


    IEnumerator InitLoop()
    {
        int initLoopCount = 0;
        while (initLoopCount <= 12)
        {
            initLoopCount++;
            Tick();
            switch (tickCount % 4)
            {
                case 0:
                    luigiManager.state = LuigiState.GETTING_YELLED;
                    marioManager.state = MarioState.GETTING_YELLED;
                    break;

                case 2:
                    luigiManager.state = LuigiState.GETTING_YELLED_BOWING;
                    marioManager.state = MarioState.GETTING_YELLED_BOWING;
                    break;

                default:
                    // Nothing
                    break;
            }
            yield return new WaitForSeconds(gameLoopSpeed / 4);
        }

        // Reset states
        luigiManager.state = LuigiState.AT_FLOOR_1_ARMS_DOWN;
        marioManager.state = MarioState.RECEIVING;
        StartCoroutine(GameLoop());
    }


    IEnumerator LuigiYelledLoop()
    {
        int initLoopCount = 0;
        while (initLoopCount <= 12)
        {
            initLoopCount++;
            Tick();
            switch (tickCount % 4)
            {
                case 0:
                    luigiManager.state = LuigiState.GETTING_YELLED;
                    break;

                case 2:
                    luigiManager.state = LuigiState.GETTING_YELLED_BOWING;
                    break;

                default:
                    // Nothing
                    break;
            }
            yield return new WaitForSeconds(gameLoopSpeed / 4);
        }

        // Reset states
        luigiManager.state = LuigiState.AT_FLOOR_1_ARMS_DOWN;
        gameLoopActive = true;
        StartCoroutine(GameLoop());
    }


    IEnumerator MarioYelledLoop()
    {
        int initLoopCount = 0;
        while (initLoopCount <= 12)
        {
            initLoopCount++;
            Tick();
            switch (tickCount % 4)
            {
                case 0:
                    marioManager.state = MarioState.GETTING_YELLED;
                    break;

                case 2:
                    marioManager.state = MarioState.GETTING_YELLED_BOWING;
                    break;

                default:
                    // Nothing
                    break;
            }
            yield return new WaitForSeconds(gameLoopSpeed / 4);
        }

        // Reset states
        marioManager.state = MarioState.RECEIVING;
        gameLoopActive = true;
        StartCoroutine(GameLoop());
    }


    IEnumerator TakeBreakLoop()
    {
        // Wait a bit before excuting Take Break
        new WaitForSeconds(gameLoopSpeed / 2);

        int loopCount = 0;
        while (loopCount <= 12)
        {
            loopCount++;
            Tick();
            switch (tickCount % 4)
            {
                case 0:
                    marioManager.state = MarioState.TAKING_BREAK;
                    luigiManager.state = LuigiState.TAKING_BREAK;
                    break;

                case 2:
                    marioManager.state = MarioState.TAKING_BREAK_BREEZE;
                    luigiManager.state = LuigiState.TAKING_BREAK_BREEZE;
                    break;

                default:
                    // Nothing
                    break;
            }

            // Add score bonus (+10 points)
            if(2 <= loopCount && loopCount <= 12)
            {
                score++;
            }

            yield return new WaitForSeconds(gameLoopSpeed / 4);
        }

        // Reset states
        ResetTruck();
        luigiManager.state = LuigiState.AT_FLOOR_1_ARMS_DOWN;
        marioManager.state = MarioState.RECEIVING;
        gameLoopActive = true;
        StartCoroutine(GameLoop());
    }

    // Actions repeated each tick
    public void Tick()
    {
        // Increment tick count by 1
        tickCount++;
    }

    // Move boxes that are on second or fourth conveyor
    public void MoveEvenConvoyer()
    {
        foreach (Box b in conveyorBelt)
        {
            if ((12 <= b.position && b.position <= 20) || (30 <= b.position && b.position <= 38))
            {
                MoveBox(b);
            }
        }
    }

    // Move boxes that are on first, third or fifth conveyor
    public void MoveOddConvoyer()
    {
        foreach (Box b in conveyorBelt)
        {
            if ((0 <= b.position && b.position <= 11) || (21 <= b.position && b.position <= 29) || (39 <= b.position && b.position <= 47))
            {
                MoveBox(b);
            }
        }
    }

    // Move boxes that are loaded in truck
    public void MoveBoxInTruck()
    {
        foreach(Box b in conveyorBelt)
        {
            if(48 <= b.position && b.position <= 57)
            {
                b.MoveInTruck();
            }
        }
    }

    // Spawn box on incoming conveyor
    void SpawnBox()
    {
        if(Random.Range(0f,1f) < boxSpawnChance)
        {
        // IF DRAW IN RANGE...
            // Create new Box and add it to Box list
            Box nextBoxSpawned = new Box();
            conveyorBelt.Add(nextBoxSpawned);
        }
    }

    // Delete boxes in falling state from conveyor belt list
    // Return position of destoryed box
    private int KillBox()
    {
        int position = 0;
        for(int i=0; i<conveyorBelt.Count; i++)
        {
            if(conveyorBelt[i].fall)
            {
                position = conveyorBelt[i].position;
                conveyorBelt.Remove(conveyorBelt[i]);
                // Break game loop
                gameLoopActive = false;
            }
        }
        return position;
    }

    // Yell the correct player if a box fall
    private void YellAtSomeone(int positionBroken)
    {
        if(positionBroken != 0)
        {
            if(positionBroken == -3)
            {
                StartCoroutine(LuigiYelledLoop());
            }
            else if(positionBroken == -1 || positionBroken == -2)
            {
                StartCoroutine(MarioYelledLoop());
            }
        }
    }


    private void ResetTruck()
    {
        boxLoaded = 0;
        for(int i = 0; i<conveyorBelt.Count; i++)
        {
            if(conveyorBelt[i].position >= 48)
            {
                conveyorBelt.RemoveAt(i);
            }
        }
    }

    // We are in trouble...
    // Need to refactor
    void MoveBox(Box box)
    {
        // IF BOX IN TILT POSITION (Luigi-side)
        if(luigiTiltPosition.Contains(box.position))
        {
            // Check 3 tilting position
            /*
                * If Luigi is on the right state (next to a tilting box)
                *      Move box to next position
                * Else (if Luigi is not next to the tilting box)
                *      Set box in a "confirmed" tilting state
            */
            #region Luigi
            if(box.position == luigiTiltPosition[0])
            {
                if(luigiManager.state == LuigiState.AT_FLOOR_1_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    luigiManager.UpdateState();
                    score++;
                    if(chanceTime) { score++; }
                }
                else
                {
                    if(box.Tilt())
                    {
                        miss++;
                        chanceTime = false;
                    }
                }
            }
            else if (box.position == luigiTiltPosition[1])
            {
                if (luigiManager.state == LuigiState.AT_FLOOR_2_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    luigiManager.UpdateState();
                    score++;
                    if (chanceTime) { score++; }
                }
                else
                {
                    if (box.Tilt())
                    {
                        miss++;
                        chanceTime = false;
                    }
                }
            }
            else if (box.position == luigiTiltPosition[2])
            {
                if (luigiManager.state == LuigiState.AT_FLOOR_3)
                {
                    boxLoaded++;
                    box.Load(dropInFirstColumn,boxLoaded);
                    dropInFirstColumn = !dropInFirstColumn;
                    luigiManager.UpdateState();
                    score++;
                    if (chanceTime) { score++; }
                }
                else
                {
                    if (box.Tilt())
                    {
                        miss++;
                        chanceTime = false;
                    }
                }
            }
            #endregion
        }
        else if (marioTiltPosition.Contains(box.position))
        {
            // Check 3 tilting position (same thing as Luigi but for Mario side)
            /*
                * If Mario is on the right state (next to a tilting box)
                *      Move box to next position
                * Else (if Mario is not next to the tilting box)
                *      Set box in a "confirmed" tilting state
            */
            #region Mario
            if (box.position == marioTiltPosition[0])
            {
                if (marioManager.state == MarioState.RECEIVING)
                {
                    box.MoveToNextPosition();
                    marioManager.UpdateState();
                    score++;
                    if (chanceTime) { score++; }
                }
                else
                {
                    if (box.Tilt())
                    {
                        miss++;
                        chanceTime = false;
                    }
                }
            }
            else if (box.position == marioTiltPosition[1])
            {
                if (marioManager.state == MarioState.AT_FLOOR_2_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    marioManager.UpdateState();
                    score++;
                    if (chanceTime) { score++; }
                }
                else
                {
                    if (box.Tilt())
                    {
                        miss++;
                        chanceTime = false;
                    }
                }
            }
            else if (box.position == marioTiltPosition[2])
            {
                if (marioManager.state == MarioState.AT_FLOOR_3_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    marioManager.UpdateState();
                    score++;
                    if (chanceTime) { score++; }
                }
                else
                {
                    if (box.Tilt())
                    {
                        miss++;
                        chanceTime = false;
                    }
                }
            }
            #endregion
        }
        else
        {
            // ALL OTHER STATES
            box.MoveToNextPosition();
        }
    }
}
