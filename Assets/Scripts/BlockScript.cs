using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private readonly string PLAYER_TAG = "Player";
    private readonly string BLOCK_TAG = "Block";
    private readonly string BLOCK_DESTROY_TAG = "BlockDestroy";

    public float fallSpeed;

    public enum BlockType
    {
        TaxReturn,
        Beer,
        Prescriptions,
        IllicitDrug,
        SuddenDeath
    }
    public BlockType blockType;

    private Rigidbody2D rb2d;
    private BoxCollider2D boxCol2d;
    private bool stacked = false;

    private GameObject rootObj;
    private Rigidbody2D rootObjRb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCol2d = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (!stacked)
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
        switch (blockType)
        {
            case BlockType.Beer:
                stats.IncreaseMorale(20.0f);
                stats.DecreaseHealth(10.0f);
                stats.UpdateCounts(BlockType.Beer);
                break;
            case BlockType.TaxReturn:
                stats.IncreaseMoney(20.0f);
                stats.DecreaseMorale(10.0f);
                stats.UpdateCounts(BlockType.TaxReturn);
                break;
            case BlockType.Prescriptions:
                stats.IncreaseHealth(20.0f);
                stats.DecreaseMoney(10.0f);
                stats.UpdateCounts(BlockType.Prescriptions);
                break;
            case BlockType.IllicitDrug:
                stats.DecreaseHealth(15.0f);
                stats.DecreaseMoney(15.0f);
                stats.DecreaseMorale(15.0f);
                break;
            case BlockType.SuddenDeath:
                stats.DecreaseHealth(15.0f);
                stats.DecreaseMoney(15.0f);
                stats.DecreaseMorale(15.0f);
                break;
        }
    }

    public void AttachBlock(Rigidbody2D attached)
    {
        FixedJoint2D fj2d = this.gameObject.AddComponent<FixedJoint2D>();
        fj2d.connectedBody = attached;
    }

    public bool UpdateHeadCheck()
    {
        if (gameObject.transform.position.y + 1 >= HeadControl.instance.highestBlockHeight)
        {
            Vector2 newHeadPosition = new Vector2(
                gameObject.transform.position.x,
                gameObject.transform.position.y +
                    ((boxCol2d.size.y / 2.0f) + 0.5f)
            );
            HeadControl.instance.UpdateHeadPosition(newHeadPosition);
            return true;
        }
        return false;
    }

    private void CheckStacking(Collision2D collision)
    {
        GameObject entity = collision.collider.gameObject;
        Vector2 normal = Vector2.zero;
        if (collision.contacts.Length > 0)
        {
            normal = collision.contacts[0].normal;
        }

        if (entity.CompareTag(BLOCK_DESTROY_TAG) && !stacked)
        {
            Destroy(gameObject);
        }
        else if (normal.y >= 0.9f && !stacked)
        {
            if (entity.CompareTag(PLAYER_TAG))
            {
                rootObj = entity;
                rootObjRb2d = rootObj.GetComponent<Rigidbody2D>();
                stacked = true;

                CheckBlockType();
                AttachBlock(rootObjRb2d);
                UpdateHeadCheck();
            }
            else if (entity.CompareTag(BLOCK_TAG))
            {
                BlockScript block = entity.GetComponent<BlockScript>();
                if (block.stacked)
                {
                    rootObj = block.rootObj;
                    rootObjRb2d = block.rootObjRb2d;
                    stacked = true;

                    CheckBlockType();
                    AttachBlock(rootObjRb2d);
                    UpdateHeadCheck();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckStacking(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckStacking(collision);
    }

    private void OnBecameInvisible()
    {
        if (!stacked)
        {
            Destroy(gameObject);
        }
        else
        {
            boxCol2d.enabled = false;
        }
    }
}
