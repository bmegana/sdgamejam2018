using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float maxSpeed;
    private float horizontalAxis;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontalAxis * maxSpeed, 0);
    }

    private void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
    }
}
