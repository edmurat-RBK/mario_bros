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

    private void Start()
    {
        // Get Manager
        luigiManager = GameObject.FindGameObjectWithTag("LuigiManager").GetComponent<LuigiManager>();
        marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();

        // Initialise convoyor belt as a list of "Box"
        conveyorBelt = new List<Box>();

        // Start game loop
        tickCount = 0;
        StartCoroutine(GameLoop());
    }

    //GAME LOOP TICK = 4 TICKS
    /* 
     * 0 : 
     *      Even convoyer moves
     *      Reset states
     *      Update states
     *      Spawn Box
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
    */

    // Game loop function
    IEnumerator GameLoop()
    {
        while (loopActive)
        {
            Tick();
            switch(tickCount%4) 
            {
                case 0:
                    luigiManager.ResetState();
                    marioManager.ResetState();

                    MoveOddConvoyer();
                    SpawnBox();
                    break;

                case 1:
                    luigiManager.ResetState();
                    marioManager.ResetState();
                    break;

                case 2:
                    luigiManager.ResetState();
                    marioManager.ResetState();

                    MoveEvenConvoyer();
                    break;

                case 3:
                    luigiManager.ResetState();
                    marioManager.ResetState();
                    break;
            }
            yield return new WaitForSeconds(gameLoopSpeed / 4);
        }
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
                }
                else
                {
                    box.Tilt();
                }
            }
            else if (box.position == luigiTiltPosition[1])
            {
                if (luigiManager.state == LuigiState.AT_FLOOR_2_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    luigiManager.UpdateState();
                }
                else
                {
                    box.Tilt();
                }
            }
            else if (box.position == luigiTiltPosition[2])
            {
                if (luigiManager.state == LuigiState.AT_FLOOR_3)
                {
                    box.MoveToNextPosition();
                    luigiManager.UpdateState();
                }
                else
                {
                    box.Tilt();
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
                }
                else
                {
                    box.Tilt();
                }
            }
            else if (box.position == marioTiltPosition[1])
            {
                if (marioManager.state == MarioState.AT_FLOOR_2_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    marioManager.UpdateState();
                }
                else
                {
                    box.Tilt();
                }
            }
            else if (box.position == marioTiltPosition[2])
            {
                if (marioManager.state == MarioState.AT_FLOOR_3_ARMS_DOWN)
                {
                    box.MoveToNextPosition();
                    marioManager.UpdateState();
                }
                else
                {
                    box.Tilt();
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
