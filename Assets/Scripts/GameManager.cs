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
    private List<Box> conveyorBelt;

    private void Start()
    {
        luigiManager = GameObject.FindGameObjectWithTag("LuigiManager").GetComponent<LuigiManager>();
        marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
        conveyorBelt = new List<Box>();
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        while(loopActive)
        {
            SpawnBox();
            Debug.Log("New Tick : " + Time.time);
            yield return new WaitForSeconds(tickSpeed);
        }
    }

    void SpawnBox()
    {
        if(Random.Range(0f,1f) < boxSpawnChance)
        {
            Box nextBoxSpawned = new Box();
        }
    }
}
