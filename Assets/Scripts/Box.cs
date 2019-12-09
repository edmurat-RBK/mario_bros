using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box
{
    public static int instanceCount = 0;
    public int id;
    public int position;

    public bool tilt = false;
    public bool fallLeft = false;
    public bool fallRight = false;
    public bool fallReceiving = false;

    public Box()
    {
        id = ++instanceCount;
        position = 0;
    }

    public void MoveToNextPosition()
    {
        // Move to next position
        position++;

        // Reset tilting state
        tilt = false;
    }

    public void Tilt()
    {
        // Call this function with "tilt" at false, set it to true
        // Call this function with "tilt" at true, makes the box falling
        if(tilt)
        {
            Fall();
        }
        else
        {
            tilt = true;
        }
    }

    public void Fall()
    {
        Debug.Log("Oups !");
    }
}
