using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDM_Mario
{
    public class MarioManager : MonoBehaviour
    {

        public MarioState state;
        private AudioSource audioSource;

        [Header("Audio")]
        public AudioClip movementAudio;
        public AudioClip passBoxAudio;


        void Start()
        {
            state = MarioState.RECEIVING;
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            switch (state)
            {
                case MarioState.RECEIVING:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        state = MarioState.AT_FLOOR_2_ARMS_DOWN;
                        audioSource.PlayOneShot(movementAudio);
                    }
                    break;

                case MarioState.AT_FLOOR_2_ARMS_DOWN:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        state = MarioState.AT_FLOOR_3_ARMS_DOWN;
                        audioSource.PlayOneShot(movementAudio);
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        state = MarioState.RECEIVING;
                        audioSource.PlayOneShot(movementAudio);
                    }
                    break;

                case MarioState.AT_FLOOR_3_ARMS_DOWN:
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        state = MarioState.AT_FLOOR_2_ARMS_DOWN;
                        audioSource.PlayOneShot(movementAudio);
                    }
                    break;
            }
        }

        public void UpdateState()
        {
            if (state == MarioState.RECEIVING)
            {
                state = MarioState.AT_FLOOR_1;
                audioSource.PlayOneShot(passBoxAudio);
            }
            else if (state == MarioState.AT_FLOOR_2_ARMS_DOWN)
            {
                state = MarioState.AT_FLOOR_2_ARMS_UP;
                audioSource.PlayOneShot(passBoxAudio);
            }
            else if (state == MarioState.AT_FLOOR_3_ARMS_DOWN)
            {
                state = MarioState.AT_FLOOR_3_ARMS_UP;
                audioSource.PlayOneShot(passBoxAudio);
            }
        }

        public void ResetState()
        {
            if (state == MarioState.AT_FLOOR_1)
            {
                state = MarioState.RECEIVING;
            }
            else if (state == MarioState.AT_FLOOR_2_ARMS_UP)
            {
                state = MarioState.AT_FLOOR_2_ARMS_DOWN;
            }
            else if (state == MarioState.AT_FLOOR_3_ARMS_UP)
            {
                state = MarioState.AT_FLOOR_3_ARMS_DOWN;
            }
        }
    }
}
