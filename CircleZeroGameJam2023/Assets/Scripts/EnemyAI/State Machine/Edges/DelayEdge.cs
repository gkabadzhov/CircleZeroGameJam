using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEdge : EdgeBase
{
    [SerializeField]
    private float delay = 1.0f;
    public override bool Evaluate()
    {
        return false;
    }
}

