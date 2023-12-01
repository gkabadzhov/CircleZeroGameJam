using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 inputMovement;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = 0.5f, dashCooldown = 1.0f;

    private float dashCounter;
    private float dashCoolCounter;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
