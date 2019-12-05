using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDM_Luigi
{
    public class LuigiManager : MonoBehaviour
    {

        public LuigiState state;


        void Start()
        {
            Invoke("ChangeState", 1f);
        }

        void Update()
        {
            
        }

        void ChangeState()
        {
            state = (LuigiState)Random.Range(0, 9);
            Invoke("ChangeState", 1f);
        }
    }
}
