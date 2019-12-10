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

    public bool Tilt()
    {
        // Call this function with "tilt" at false, set it to true
        // Call this function with "tilt" at true, makes the box falling
        // Return true if box has fallen, false if box has tilted

        bool hasFall;
        if(tilt)
        {
            Fall();
            hasFall = true;
        }
        else
        {
            tilt = true;
            hasFall = false;
        }
        return hasFall;
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
