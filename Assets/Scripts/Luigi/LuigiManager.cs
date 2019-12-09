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
            state = LuigiState.AT_FLOOR_1_ARMS_DOWN;
        }

        void Update()
        {
            switch(state)
            {
                case LuigiState.AT_FLOOR_1_ARMS_DOWN:
                    if(Input.GetKeyDown(KeyCode.Z))
                    {
                        state = LuigiState.AT_FLOOR_2_ARMS_DOWN;
                    }
                    break;

                case LuigiState.AT_FLOOR_2_ARMS_DOWN:
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        state = LuigiState.AT_FLOOR_3;
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        state = LuigiState.AT_FLOOR_1_ARMS_DOWN;
                    }
                    break;

                case LuigiState.AT_FLOOR_3:
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        state = LuigiState.AT_FLOOR_2_ARMS_DOWN;
                    }
                    break;
            }
        }

        public void UpdateState()
        {
            if(state == LuigiState.AT_FLOOR_1_ARMS_DOWN)
            {
                state = LuigiState.AT_FLOOR_1_ARMS_UP;
            }
            else if (state == LuigiState.AT_FLOOR_2_ARMS_DOWN)
            {
                state = LuigiState.AT_FLOOR_2_ARMS_UP;
            }
            else if (state == LuigiState.AT_FLOOR_3)
            {
                state = LuigiState.DROPPING;
            }
        }

        public void ResetState()
        {
            if (state == LuigiState.AT_FLOOR_1_ARMS_UP)
            {
                state = LuigiState.AT_FLOOR_1_ARMS_DOWN;
            }
            else if (state == LuigiState.AT_FLOOR_2_ARMS_UP)
            {
                state = LuigiState.AT_FLOOR_2_ARMS_DOWN;
            }
            else if (state == LuigiState.DROPPING)
            {
                state = LuigiState.AT_FLOOR_3;
            }
        }
    }
}
