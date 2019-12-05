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
        position++;
        Debug.Log("[ID:" + id + "] Position : " + position);
    }
}
