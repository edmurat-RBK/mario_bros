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

    private void Start()
    {
        luigiManager = GameObject.FindGameObjectWithTag("LuigiManager").GetComponent<LuigiManager>();
        marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        while(loopActive)
        {
            // What I need to do in the loop ?
            Debug.Log("New Tick : " + Time.time);
            yield return new WaitForSeconds(tickSpeed);
        }
    }
}
