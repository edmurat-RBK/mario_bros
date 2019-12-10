using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SevenDigitDisplay : MonoBehaviour
{
    private DigitSegment segmentA;
    private DigitSegment segmentB;
    private DigitSegment segmentC;
    private DigitSegment segmentD;
    private DigitSegment segmentE;
    private DigitSegment segmentF;
    private DigitSegment segmentG;

    public int digitToPrint = 0;

    private void Start()
    {
        DigitSegment[] children = GetComponentsInChildren<DigitSegment>();
        foreach(DigitSegment ds in children)
        {
            switch(ds.segment)
            {
                case DigitSegment.SegmentType.A:
                    segmentA = ds;
                    break;

                case DigitSegment.SegmentType.B:
                    segmentB = ds;
                    break;

                case DigitSegment.SegmentType.C:
                    segmentC = ds;
                    break;

                case DigitSegment.SegmentType.D:
                    segmentD = ds;
                    break;

                case DigitSegment.SegmentType.E:
                    segmentE = ds;
                    break;

                case DigitSegment.SegmentType.F:
                    segmentF = ds;
                    break;

                case DigitSegment.SegmentType.G:
                    segmentG = ds;
                    break;
            }
        }
    }

    private void Update()
    {
        switch(digitToPrint)
        {
            case 0:
                segmentA.active = true;
                segmentB.active = true;
                segmentC.active = true;
                segmentD.active = true;
                segmentE.active = true;
                segmentF.active = true;
                segmentG.active = false;
                break;

            case 1:
                segmentA.active = false;
                segmentB.active = true;
                segmentC.active = true;
                segmentD.active = false;
                segmentE.active = false;
                segmentF.active = false;
                segmentG.active = false;
                break;

            case 2:
                segmentA.active = true;
                segmentB.active = true;
                segmentC.active = false;
                segmentD.active = true;
                segmentE.active = true;
                segmentF.active = false;
                segmentG.active = true;
                break;

            case 3:
                segmentA.active = true;
                segmentB.active = true;
                segmentC.active = true;
                segmentD.active = true;
                segmentE.active = false;
                segmentF.active = false;
                segmentG.active = true;
                break;

            case 4:
                segmentA.active = false;
                segmentB.active = true;
                segmentC.active = true;
                segmentD.active = false;
                segmentE.active = false;
                segmentF.active = true;
                segmentG.active = true;
                break;

            case 5:
                segmentA.active = true;
                segmentB.active = false;
                segmentC.active = true;
                segmentD.active = true;
                segmentE.active = false;
                segmentF.active = true;
                segmentG.active = true;
                break;

            case 6:
                segmentA.active = true;
                segmentB.active = false;
                segmentC.active = true;
                segmentD.active = true;
                segmentE.active = true;
                segmentF.active = true;
                segmentG.active = true;
                break;

            case 7:
                segmentA.active = true;
                segmentB.active = true;
                segmentC.active = true;
                segmentD.active = false;
                segmentE.active = false;
                segmentF.active = false;
                segmentG.active = false;
                break;

            case 8:
                segmentA.active = true;
                segmentB.active = true;
                segmentC.active = true;
                segmentD.active = true;
                segmentE.active = true;
                segmentF.active = true;
                segmentG.active = true;
                break;

            case 9:
                segmentA.active = true;
                segmentB.active = true;
                segmentC.active = true;
                segmentD.active = true;
                segmentE.active = false;
                segmentF.active = true;
                segmentG.active = true;
                break;

        }
    }
}
