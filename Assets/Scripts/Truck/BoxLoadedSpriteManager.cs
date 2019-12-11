using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLoadedSpriteManager : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer spriteComponant;

    [Range(1, 8)]
    public int boxNumber;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spriteComponant = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (gameManager.boxLoaded >= boxNumber)
        {
            spriteComponant.enabled = true;
        }
        else
        {
            spriteComponant.enabled = false;
        }

    }
}
