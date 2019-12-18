using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box
{
    public int position;

    public bool tilt = false;
    public bool fall = false;
    public bool load = false;
    public bool fallInTruck = false;
    public int stopPosition = 0;

    public Box()
    {
        position = 0;
    }

    // Move box to next position
    public void MoveToNextPosition()
    {
        // Move to next position
        position++;

        // Reset tilting state
        tilt = false;
    }

    // Call this function with "tilt" at false, set it to true
    // Call this function with "tilt" at true, makes the box falling
    // Return true if box has fallen, false if box has tilted
    public bool Tilt()
    {
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

    // Set position of box to it falling position
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

    // Set box to a "on-truck state"
    public void Load(bool inFirstColumn, int boxCountInTruck)
    {
        load = true;
        fallInTruck = true;
        tilt = false;

        SetStopPosition(boxCountInTruck);

        if(inFirstColumn)
        {
            position = 49;
        }
        else
        {
            position = 48;
        }
    }

    // Used to shox gravity on box in truck
    public bool MoveInTruck()
    {
        bool hasLanded = false;
        if(fallInTruck)
        {
            position += 2;

            if(position >= stopPosition)
            {
                fallInTruck = false;
                hasLanded = true;
            }
        }
        return hasLanded;
    }

    // Get what should be the last position of the box in truck
    private void SetStopPosition(int boxCountInTruck)
    {
        switch(boxCountInTruck)
        {
            case 0:
                stopPosition = 57;
                break;
            case 1:
                stopPosition = 56;
                break;
            case 2:
                stopPosition = 55;
                break;
            case 3:
                stopPosition = 54;
                break;
            case 4:
                stopPosition = 53;
                break;
            case 5:
                stopPosition = 52;
                break;
            case 6:
                stopPosition = 51;
                break;
            case 7:
                stopPosition = 50;
                break;
        }
    }
}
