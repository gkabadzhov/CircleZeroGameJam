using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    [SerializeField]
    private EdgeBase[] edges;
    protected TaskSystem taskSystem;
    protected EnemyVision vision;

    void Start()
    {
        taskSystem  = GetComponentInParent<TaskSystem>();
        vision      = transform.parent.GetComponentInParent<EnemyVision>();
    }

    public StateBase OnEvaluateEdges()
    {
        foreach (EdgeBase edge in edges)
        {
            if (edge != null && edge.Evaluate())
            {
                return edge.ResultState();
            }
        }

        return this;
    }

    public virtual void OnStartState()
    {

    }

    public virtual void OnUpdateState()
    {

    }

    public virtual void OnLeaveState()
    {

    }
}
