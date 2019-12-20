using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    private GameManager gameManager;
    private SmokeManager smokeManager;

    public TruckState truckState;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        smokeManager = GameObject.FindGameObjectWithTag("SmokeManager").GetComponent<SmokeManager>();
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
            smokeManager.StartCoroutine("SmokeLoop");
        }
        else if((gameManager.boxLoaded == 8) && truckState == TruckState.ALMOST_LOAD)
        {
            truckState = TruckState.READY_TO_GO;
        }
    }
}
