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
    public float tickSpeed;
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
        StartCoroutine(Tick());
    }

    // Game loop function
    IEnumerator Tick()
    {
        while(loopActive)
        {
        // EACH TICK...
            // Check to randomly spawn box
            SpawnBox();

            //Reset all "unstable" state
            luigiManager.ResetState();
            marioManager.ResetState();

            // Move boxes to next position
            MoveAllBoxes();
            
            yield return new WaitForSeconds(tickSpeed);
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

    void MoveAllBoxes()
    {
        foreach(Box b in conveyorBelt)
        {
        // FOR EACH BOX IN CONVEYOR BELT LIST...
            // IF BOX IN TILT POSITION (Luigi-side)
            if(luigiTiltPosition.Contains(b.position))
            {
                // Check 3 tilting position
                /*
                 * If Luigi is on the right state (next to a tilting box)
                 *      Move box to next position
                 * Else (if Luigi is not next to the tilting box)
                 *      Set box in a "confirmed" tilting state
                */
                #region Check Luigi position
                if(b.position == luigiTiltPosition[0])
                {
                    if(luigiManager.state == LuigiState.AT_FLOOR_1_ARMS_DOWN)
                    {
                        b.MoveToNextPosition();
                        luigiManager.UpdateState();
                    }
                    else
                    {
                        b.Tilt();
                    }
                }
                else if (b.position == luigiTiltPosition[1])
                {
                    if (luigiManager.state == LuigiState.AT_FLOOR_2_ARMS_DOWN)
                    {
                        b.MoveToNextPosition();
                        luigiManager.UpdateState();
                    }
                    else
                    {
                        b.Tilt();
                    }
                }
                else if (b.position == luigiTiltPosition[2])
                {
                    if (luigiManager.state == LuigiState.AT_FLOOR_3)
                    {
                        b.MoveToNextPosition();
                        luigiManager.UpdateState();
                    }
                    else
                    {
                        b.Tilt();
                    }
                }
                #endregion
            }
            else if (marioTiltPosition.Contains(b.position))
            {
                // Check 3 tilting position (same thing as Luigi but for Mario side)
                /*
                 * If Mario is on the right state (next to a tilting box)
                 *      Move box to next position
                 * Else (if Mario is not next to the tilting box)
                 *      Set box in a "confirmed" tilting state
                */
                #region Check Mario position
                if (b.position == marioTiltPosition[0])
                {
                    if (marioManager.state == MarioState.RECEIVING)
                    {
                        b.MoveToNextPosition();
                        marioManager.UpdateState();
                    }
                    else
                    {
                        b.Tilt();
                    }
                }
                else if (b.position == marioTiltPosition[1])
                {
                    if (marioManager.state == MarioState.AT_FLOOR_2_ARMS_DOWN)
                    {
                        b.MoveToNextPosition();
                        marioManager.UpdateState();
                    }
                    else
                    {
                        b.Tilt();
                    }
                }
                else if (b.position == marioTiltPosition[2])
                {
                    if (marioManager.state == MarioState.AT_FLOOR_3_ARMS_DOWN)
                    {
                        b.MoveToNextPosition();
                        marioManager.UpdateState();
                    }
                    else
                    {
                        b.Tilt();
                    }
                }
                #endregion
            }
            else
            {
                // ALL OTHER STATES
                b.MoveToNextPosition();
            }
        }
    }
}
