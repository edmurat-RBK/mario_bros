using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissDisplay : MonoBehaviour
{
    [Range(1, 3)]
    public int minimalMissToDisplay;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spriteRenderer.enabled = true;
    }

    private void Update()
    {
        if(gameManager.miss >= minimalMissToDisplay)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

}
