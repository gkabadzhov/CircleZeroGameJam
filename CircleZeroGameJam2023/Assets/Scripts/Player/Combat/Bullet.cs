using OTBG.Gameplay.Player.Combat.Data;
using OTBG.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1000f;

    // Update is called once per frame
    private void Awake()
    {
        Destroy(this.gameObject,5);
    }

    public void BulletInit(Vector3 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 1000);
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
