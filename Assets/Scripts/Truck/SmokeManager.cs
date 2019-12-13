using UnityEngine;
using System.Collections;

public class SmokeManager : MonoBehaviour
{
    private GameManager gameManager;
    private TruckManager truckManager;

    public SmokeState smokeState;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        truckManager = GameObject.FindGameObjectWithTag("TruckManager").GetComponent<TruckManager>();
    }

    IEnumerator SmokeLoop()
    {
        while(truckManager.truckState == TruckState.ALMOST_LOAD)
        {
            switch(smokeState)
            {
                case SmokeState.NONE:
                    smokeState = SmokeState.SMALL;
                    break;

                case SmokeState.SMALL:
                    smokeState = SmokeState.LARGE;
                    break;

                case SmokeState.LARGE:
                    smokeState = SmokeState.NONE;
                    break;
            }
            yield return new WaitForSeconds(gameManager.gameLoopSpeed / 4);
        }

        smokeState = SmokeState.NONE;
    }
}
