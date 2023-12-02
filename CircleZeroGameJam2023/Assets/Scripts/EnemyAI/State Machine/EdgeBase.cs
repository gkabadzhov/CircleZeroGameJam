using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeBase : MonoBehaviour
{
    [SerializeField]
    private StateBase resultState;

    public StateBase ResultState()
    {
        return resultState;
    }

    public virtual bool Evaluate()
    {
        return false;
    }
}
