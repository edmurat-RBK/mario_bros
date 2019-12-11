using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BoxSpriteManager : MonoBehaviour
{
    private SpriteRenderer spriteComponant;
    private GameManager gameManager;

    [Range(-3,57)]
    public int position;

    private void Start()
    {
        spriteComponant = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spriteComponant.enabled = false;
    }

    private void Update()
    {
        spriteComponant.enabled = false;
        foreach(Box b in gameManager.conveyorBelt)
        {
            if(b != null)
            {
                if (b.position == position)
                {
                    spriteComponant.enabled = true;
                }
            }
        }
    }
}
