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

    // Update is called once per frame
    void Update()
    {
        ResolveLookDirection();
    }

    private void ResolveLookDirection()
    {
        transform.localScale = new Vector3(Mathf.Sign(rb2d.velocity.x), 1, 1);
    }
}
