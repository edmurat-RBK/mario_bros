using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDM_Mario
{
    public class MarioManager : MonoBehaviour
    {

        public MarioState state;


        void Start()
        {
            state = MarioState.RECEIVING;
        }

        void Update()
        {
            switch (state)
            {
                case MarioState.RECEIVING:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        state = MarioState.AT_FLOOR_2_ARMS_DOWN;
                    }
                    break;

                case MarioState.AT_FLOOR_2_ARMS_DOWN:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        state = MarioState.AT_FLOOR_3_ARMS_DOWN;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        state = MarioState.RECEIVING;
                    }
                    break;

                case MarioState.AT_FLOOR_3_ARMS_DOWN:
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        state = MarioState.AT_FLOOR_2_ARMS_DOWN;
                    }
                    break;
            }
        }
    }
}
