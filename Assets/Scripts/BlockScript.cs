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
        DoctorAppointment,
        IllicitDrug
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
		StatManager stats = StatManager.instance;
		switch(blockType)
		{
			case BlockType.Beer:
				stats.IncreaseMorale(10.0f);
				stats.DecreaseHealth(10.0f);
				break;
			case BlockType.TaxReturn:
				stats.IncreaseMoney(10.0f);
				stats.DecreaseMorale(10.0f);
				break;
			case BlockType.DoctorAppointment:
				stats.IncreaseHealth(10.0f);
				stats.DecreaseMorale(10.0f);
				break;
			case BlockType.IllicitDrug:
				stats.DecreaseHealth(20.0f);
				stats.DecreaseMoney(20.0f);
				stats.DecreaseMorale(20.0f);
				break;
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		GameObject entity = collision.collider.gameObject;
		Vector2 normal = Vector2.zero;
        if (collision.contacts.Length > 0)
        {
            normal = collision.contacts[0].normal;
        }

        if (entity.CompareTag("BlockDestroy") && !collided)
        {
            Destroy(gameObject);
        }
		else if (normal.y >= 0.99f && !collided)
        {
			if (entity.CompareTag("Player"))
            {
                rootObj = entity;
                rootObjRb2d = rootObj.GetComponent<Rigidbody2D>();
            }
			else if (entity.CompareTag(BLOCK_TAG))
            {
                BlockScript block = entity.GetComponent<BlockScript>();
                rootObj = block.rootObj;
                rootObjRb2d = block.rootObjRb2d;
			}
			collided = true;
			CheckBlockType();
			AttachBlock(rootObjRb2d);
			UpdateHeadCheck();
		}
	}

	public void AttachBlock(Rigidbody2D attached)
	{
		FixedJoint2D fj2d = this.gameObject.AddComponent<FixedJoint2D>();
		fj2d.connectedBody = attached;
	}
	
	public bool UpdateHeadCheck()
	{
		if (gameObject.transform.position.y >= HeadControl.instance.highestBlockHeight)
		{
			HeadControl.instance.UpdateHeadPosition(gameObject.transform.position);
			return true;
		}
		return false;
	}
}
