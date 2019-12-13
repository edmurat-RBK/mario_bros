using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    private GameManager gameManager;

    public TruckState truckState;
    public int subState = -1;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(gameManager.boxLoaded < 6)
        {
            truckState = TruckState.WAITING;
        }
        else if((6 <= gameManager.boxLoaded && gameManager.boxLoaded < 8) && truckState == TruckState.WAITING)
        {
            truckState = TruckState.ALMOST_LOAD;
            if(subState == -1)
            {
                subState = 0;
                StartCoroutine(AlmostLoad());
            }
        }
        else if((gameManager.boxLoaded == 8) && truckState == TruckState.ALMOST_LOAD)
        {
            truckState = TruckState.READY_TO_GO;
        }
    }

    IEnumerator AlmostLoad()
    {
        while (truckState == TruckState.ALMOST_LOAD)
        {
            subState = (subState + 1) % 3;
            yield return new WaitForSeconds(gameManager.gameLoopSpeed / 4);
        }
    }
}
