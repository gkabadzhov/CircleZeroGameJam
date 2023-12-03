using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        ResolveLookDirection();
    }

    private void ResolveLookDirection()
    {
        if (rb2d == null || Mathf.Abs(rb2d.velocity.x) <= 0.01f)
        {
            return;
        }

        transform.localScale = new Vector3(-Mathf.Sign(rb2d.velocity.x), 1, 1);
    }
}
