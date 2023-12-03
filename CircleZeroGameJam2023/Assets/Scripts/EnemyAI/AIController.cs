using OTBG.Gameplay.Player.Combat.Data;
using OTBG.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private StateMachineBase stateMachine;
    private TaskSystem taskSystem;
    void Start()
    {
        stateMachine = GetComponent<StateMachineBase>();
        taskSystem = GetComponent<TaskSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isTaskSystemEmpty = taskSystem.IsEmpty();

        taskSystem.SetActive(isTaskSystemEmpty == false);
        stateMachine.SetActive(isTaskSystemEmpty);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (taskSystem.GetTask().GetType() == typeof(AIDashTask))
        {
            Debug.Log("In task type check");
            if (!collision.transform.TryGetComponent(out IDamageable damageable))
            {
                return;
            }

            Debug.Log("Past finding damageable");
            damageable.TakeDamage(new DamageData() { damage = 1} );
        }

        //   Destroy(this.gameObject);
    }
}
