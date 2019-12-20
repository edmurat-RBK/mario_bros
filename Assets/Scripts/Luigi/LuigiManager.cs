using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDM_Luigi
{
    public class LuigiManager : MonoBehaviour
    {

        public LuigiState state;
        private AudioSource audioSource;

        [Header("Audio")]
        public AudioClip movementAudio;
        public AudioClip passBoxAudio;

        void Start()
        {
            state = LuigiState.AT_FLOOR_1_ARMS_DOWN;
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            switch(state)
            {
                case LuigiState.AT_FLOOR_1_ARMS_DOWN:
                    if(Input.GetKeyDown(KeyCode.Z))
                    {
                        state = LuigiState.AT_FLOOR_2_ARMS_DOWN;
                        audioSource.PlayOneShot(movementAudio);
                    }
                    break;

                case LuigiState.AT_FLOOR_2_ARMS_DOWN:
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        state = LuigiState.AT_FLOOR_3;
                        audioSource.PlayOneShot(movementAudio);
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        state = LuigiState.AT_FLOOR_1_ARMS_DOWN;
                        audioSource.PlayOneShot(movementAudio);
                    }
                    break;

                case LuigiState.AT_FLOOR_3:
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        state = LuigiState.AT_FLOOR_2_ARMS_DOWN;
                        audioSource.PlayOneShot(movementAudio);
                    }
                    break;
            }
        }

        public void UpdateState()
        {
            if(state == LuigiState.AT_FLOOR_1_ARMS_DOWN)
            {
                state = LuigiState.AT_FLOOR_1_ARMS_UP;
                audioSource.PlayOneShot(passBoxAudio);
            }
            else if (state == LuigiState.AT_FLOOR_2_ARMS_DOWN)
            {
                state = LuigiState.AT_FLOOR_2_ARMS_UP;
                audioSource.PlayOneShot(passBoxAudio);
            }
            else if (state == LuigiState.AT_FLOOR_3)
            {
                state = LuigiState.DROPPING;
                audioSource.PlayOneShot(passBoxAudio);
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
