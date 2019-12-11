using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TruckSpriteManager : MonoBehaviour
{
    private TruckManager truckManager;
    private SpriteRenderer spriteComponant;

    public List<TruckState> activeAtState;

    private void Start()
    {
        truckManager = GameObject.FindGameObjectWithTag("TruckManager").GetComponent<TruckManager>();
        spriteComponant = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (activeAtState.Contains(truckManager.truckState))
        {
            spriteComponant.enabled = true;
        }
        else
        {
            spriteComponant.enabled = false;
        }
    }
}
