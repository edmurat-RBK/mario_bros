using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDM_Mario
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MarioSpriteManager : MonoBehaviour
    {
        private SpriteRenderer spriteComponant;
        private MarioManager marioManager;

        public List<MarioState> activeAtState;


        private void Start()
        {
            spriteComponant = GetComponent<SpriteRenderer>();
            marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
            spriteComponant.enabled = true;
        }

        private void Update()
        {
            if (activeAtState.Contains(marioManager.state))
            {
                spriteComponant.enabled = true;
            }
            else
            {
                spriteComponant.enabled = false;
            }

        }
    }
}
