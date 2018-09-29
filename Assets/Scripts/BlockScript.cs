using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private readonly string BLOCK_TAG = "Block";

    public float fallSpeed;

    public enum BlockType
    {
        TaxReturn,
        Beer,
        DoctorAppointment
    }
    public BlockType blockType;

    private Rigidbody2D rb2d;
    private bool collided = false;

    private GameObject rootObj;
    private Rigidbody2D rootObjRb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!collided)
        {
            rb2d.velocity = new Vector2(0.0f, -fallSpeed);
        }
        else
        {
            if (rootObjRb2d != null)
            {
                rb2d.velocity = new Vector2(rootObjRb2d.velocity.x, rb2d.velocity.y);
            }
        }
    }

    private void CheckBlockType()
    {
        if (blockType == BlockType.Beer)
        {
            Debug.Log("Increasing Morale, Decreasing Health.");
        }
        if (blockType == BlockType.TaxReturn)
        {
            Debug.Log("Increasing Money, Decreasing Morale.");
        }
        if (blockType == BlockType.DoctorAppointment)
        {
            Debug.Log("Increasing Health, Decreasing Money.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = Vector2.zero;
        if (collision.contacts.Length > 0)
        {
            normal = collision.contacts[0].normal;
        }
        GameObject entity = collision.collider.gameObject;

        if (entity.CompareTag("BlockDestroy"))
        {
            Destroy(gameObject);
        }


        if (normal.y >= 0.99f)
        {
			if (entity.CompareTag("Player"))
            {
                collided = true;
                rootObj = entity;
                rootObjRb2d = rootObj.GetComponent<Rigidbody2D>();
				FixedJoint2D fj2d = this.gameObject.AddComponent<FixedJoint2D>();
				fj2d.connectedBody = rootObjRb2d;

			}
			else if (entity.CompareTag(BLOCK_TAG))
            {
                BlockScript block = entity.GetComponent<BlockScript>();
                collided = true;
                rootObj = block.rootObj;
                rootObjRb2d = block.rootObjRb2d;
				FixedJoint2D fj2d = this.gameObject.AddComponent<FixedJoint2D>();
				fj2d.connectedBody = rootObjRb2d;
				CheckBlockType();
			}
		}
	}
}
