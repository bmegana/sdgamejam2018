using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public float fallSpeed;

    private Rigidbody2D rb2d;
    private bool collided = false;

    private Transform parentObjTransform;
    private Rigidbody2D parentObjRb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private GameObject GetRootParent(GameObject)
    {
        if (transform.parent != null)
        {
            return GetRootParent();
        }
    }

    private void FixedUpdate()
    {
        if (!collided)
        {
            rb2d.velocity = new Vector2(0.0f, -fallSpeed);
        }
        else
        {
            rb2d.velocity = new Vector2(parentObjRb2d.velocity.x, rb2d.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject entity = collision.collider.gameObject;
        if (entity.CompareTag("Player"))
        {
            collided = true;
            transform.parent = entity.transform;

            parentObjTransform = entity.transform;
            parentObjRb2d = entity.GetComponent<Rigidbody2D>();
            gameObject.tag = "Player";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collided = false;
        transform.parent = null;

        parentObjTransform = null;
        parentObjRb2d = null;
    }
}
