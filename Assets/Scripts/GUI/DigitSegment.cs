using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitSegment : MonoBehaviour
{
    public enum SegmentType
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G
    }

    public SegmentType segment;
    public bool active;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        active = true;
    }

    private void Update()
    {
        spriteRenderer.enabled = active;
    }
}
