using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box
{
    public static int instanceCount = 0;
    public int id;
    public int position;

    public bool tilt = false;
    public bool fall = false;

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
        if(position == 2)
        {
            fall = true;
            tilt = false;
            position = -1;
        }
        else if (position == 11 || position == 29 || position == 47)
        {
            fall = true;
            tilt = false;
            position = -3;
        }
        else if (position == 20 || position == 38)
        {
            fall = true;
            tilt = false;
            position = -2;
        }
    }
}
