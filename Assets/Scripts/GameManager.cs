using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDM_Luigi;
using EDM_Mario;

public class GameManager : MonoBehaviour
{
    private LuigiManager luigiManager;
    private MarioManager marioManager;

    [Header("Game loop")]
    public float gameLoopSpeed;
    public int tickCount;
    public bool loopActive = true;

    [Header("Boxes")]
    public float boxSpawnChance;
    [HideInInspector] public List<Box> conveyorBelt;

    [Header("Tilt position")]
    public List<int> luigiTiltPosition;
    public List<int> marioTiltPosition;

    [Header("Scores")]
    public int score = 0;
    public int miss = 0;

    // 7-segment digit displays
    private SevenDigitDisplay digitDisplay1;
    private SevenDigitDisplay digitDisplay10;
    private SevenDigitDisplay digitDisplay100;

    private void Start()
    {
        // Get Manager
        luigiManager = GameObject.FindGameObjectWithTag("LuigiManager").GetComponent<LuigiManager>();
        marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();

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
        if(score <= 999)
        {
            if(score <= 9)
            {
                digitDisplay100.digitToPrint = -1;
                digitDisplay10.digitToPrint = -1;
                digitDisplay1.digitToPrint = score % 10;
            }
            else if (score <= 99)
            {
                digitDisplay100.digitToPrint = -1;
                digitDisplay10.digitToPrint = (int)Mathf.Floor(score % 100 / 10);
                digitDisplay1.digitToPrint = score % 10;
            }
            else
            {
                digitDisplay100.digitToPrint = (int)Mathf.Floor(score / 100);
                digitDisplay10.digitToPrint = (int)Mathf.Floor(score % 100 / 10);
                digitDisplay1.digitToPrint = score % 10;
            }
        }
        else
        {
            digitDisplay100.digitToPrint = 9;
            digitDisplay10.digitToPrint = 9;
            digitDisplay1.digitToPrint = 9;
        }
        
    }

    // Game loop function
    IEnumerator GameLoop()
    {
    //GAME LOOP TICK = 4 TICKS
    /* 
        * 0 : 
        *      Even convoyer moves
        *      Reset states
        *      Update states
        * 1 : 
        *      Reset states
        *      Update states
        * 2 : 
        *      Odd convoyer moves
        *      Reset states
        *      Update states
        * 3 : 
        *      Reset states
        *      Update states
        * 
        * Every 4 game loop (16 ticks), spawn box
    */
        while (loopActive)
        {
            Tick();
            switch(tickCount%4) 
            {
                case 0:
                    luigiManager.ResetState();
                    marioManager.ResetState();
                    KillBox();
                    MoveOddConvoyer();
                    break;

                case 1:
                    luigiManager.ResetState();
                    marioManager.ResetState();
                    KillBox();
                    break;

                case 2:
                    luigiManager.ResetState();
                    marioManager.ResetState();
                    KillBox();
                    MoveEvenConvoyer();
                    break;

                case 3:
                    luigiManager.ResetState();
                    marioManager.ResetState();
                    KillBox();
                    break;
            }
            if(tickCount%16 == 0) { SpawnBox(); }
            yield return new WaitForSeconds(gameLoopSpeed / 4);
        }
    }

    IEnumerator InitLoop()
    {
        //INIT LOOP TICK = 12 TICKS
        /* 
            * 0 : 
            *      Mario and Luigi are bowing up at their boss
            * 1 : 
            *      nothing
            * 2 : 
            *      Mario and Luigi are bowing down at their boss
            * 3 : 
            *      nothing
            * 
            * After 12 ticks, break loop and switch to game
        */
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

                case 1:
                    // Nothing
                    break;

                case 2:
                    luigiManager.state = LuigiState.GETTING_YELLED_BOWING;
                    marioManager.state = MarioState.GETTING_YELLED_BOWING;
                    break;

                case 3:
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

    public void Tick()
    {
        tickCount++;
    }

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

    void KillBox()
    {
        for(int i=0; i<conveyorBelt.Count; i++)
        {
            if(conveyorBelt[i].fall)
            {
                conveyorBelt.Remove(conveyorBelt[i]);
            }
        }
    }

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
            #region Check Luigi position
            if(box.position == luigiTiltPosition[0])
            {
                if(luigiManager.state == LuigiState.AT_FLOOR_1_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    luigiManager.UpdateState();
                    score++;
                }
                else
                {
                    if(box.Tilt()) { miss++; }
                }
            }
            else if (box.position == luigiTiltPosition[1])
            {
                if (luigiManager.state == LuigiState.AT_FLOOR_2_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    luigiManager.UpdateState();
                    score++;
                }
                else
                {
                    if (box.Tilt()) { miss++; }
                }
            }
            else if (box.position == luigiTiltPosition[2])
            {
                if (luigiManager.state == LuigiState.AT_FLOOR_3)
                {
                    box.MoveToNextPosition();
                    luigiManager.UpdateState();
                    score++;
                }
                else
                {
                    if (box.Tilt()) { miss++; }
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
            #region Check Mario position
            if (box.position == marioTiltPosition[0])
            {
                if (marioManager.state == MarioState.RECEIVING)
                {
                    box.MoveToNextPosition();
                    marioManager.UpdateState();
                    score++;
                }
                else
                {
                    if (box.Tilt()) { miss++; }
                }
            }
            else if (box.position == marioTiltPosition[1])
            {
                if (marioManager.state == MarioState.AT_FLOOR_2_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    marioManager.UpdateState();
                    score++;
                }
                else
                {
                    if (box.Tilt()) { miss++; }
                }
            }
            else if (box.position == marioTiltPosition[2])
            {
                if (marioManager.state == MarioState.AT_FLOOR_3_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    marioManager.UpdateState();
                    score++;
                }
                else
                {
                    if (box.Tilt()) { miss++; }
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
