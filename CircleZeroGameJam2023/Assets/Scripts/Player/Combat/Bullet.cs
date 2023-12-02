using OTBG.Gameplay.Player.Combat.Data;
using OTBG.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float minimumSpeed = 15f;

    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(this.gameObject,5);
    }

    public void BulletInit(Vector3 direction, float force)
    {
        rb.velocity = direction.normalized * Mathf.Max(force, minimumSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out IDamageable damageable))
        {
            Destroy(this.gameObject);
            return;
        }

        damageable.TakeDamage(new DamageData() { 
            damage = 1
        });

        Destroy(this.gameObject);
    }


}
