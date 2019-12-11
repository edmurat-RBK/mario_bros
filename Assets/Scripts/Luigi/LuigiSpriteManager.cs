using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDM_Luigi
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LuigiSpriteManager : MonoBehaviour
    {
        private SpriteRenderer spriteComponant;
        private LuigiManager luigiManager;

        public List<LuigiState> activeAtState;


        private void Start()
        {
            spriteComponant = GetComponent<SpriteRenderer>();
            luigiManager = GameObject.FindGameObjectWithTag("LuigiManager").GetComponent<LuigiManager>();
            spriteComponant.enabled = true;
        }

        private void Update()
        {
            if(activeAtState.Contains(luigiManager.state))
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

