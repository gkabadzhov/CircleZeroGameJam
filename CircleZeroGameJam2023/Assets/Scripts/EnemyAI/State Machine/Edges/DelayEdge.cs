using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEdge : EdgeBase
{
    [SerializeField]
    private float minimumDelay = 1.0f;
    [SerializeField]
    private float maximumDelay = 5.0f;

    private bool result = false;

    public override bool Evaluate()
    {
        StartCoroutine(Delay());

        return result;
    }

    IEnumerator Delay()
    {
        float delayTime = Random.Range(minimumDelay, maximumDelay);
        yield return new WaitForSeconds(delayTime);

        result = true;
    }
}

