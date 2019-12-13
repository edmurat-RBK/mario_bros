using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmokeSpriteManager : MonoBehaviour
{
    private SmokeManager smokeManager;
    private SpriteRenderer spriteComponant;

    public List<SmokeState> activeAtState;

    private void Start()
    {
        smokeManager = GameObject.FindGameObjectWithTag("SmokeManager").GetComponent<SmokeManager>();
        spriteComponant = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (activeAtState.Contains(smokeManager.smokeState))
        {
            spriteComponant.enabled = true;
        }
        else
        {
            spriteComponant.enabled = false;
        }
    }
}
