using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public float spawnInterval;
    public float intervalTimeIncrement;
    private float intervalTime = 0.0f;

    public GameObject objectPrefab;

    private void InstantiateRandomBlock(Vector2 spawnPos)
    {
        int blockTypeNum = Random.Range(0, 4);
        GameObject block = Instantiate(
                objectPrefab,
                spawnPos,
                new Quaternion()
        );
        BlockScript blockScript = block.GetComponent<BlockScript>();
        if (blockTypeNum == 0)
        {
            block.transform.localScale = new Vector3(3, 1, 1);
            block.GetComponent<SpriteRenderer>().color = Color.yellow;
            blockScript.blockType = BlockScript.BlockType.Beer;
        }
        else if (blockTypeNum == 1)
        {
            block.transform.localScale = new Vector3(1, 3, 1);
            block.GetComponent<SpriteRenderer>().color = Color.green;
            blockScript.blockType = BlockScript.BlockType.TaxReturn;
        }
        else if (blockTypeNum == 2)
        {
            block.transform.localScale = new Vector3(2, 2, 1);
            block.GetComponent<SpriteRenderer>().color = Color.red;
            blockScript.blockType = BlockScript.BlockType.DoctorAppointment;
        }
        else if (blockTypeNum == 3)
        {
            int badType = Random.Range(0, 2);
            if (badType == 1)
            {
                block.transform.localScale = new Vector3(3, 3, 1);
            }
            block.GetComponent<SpriteRenderer>().color = Color.black;
            blockScript.blockType = BlockScript.BlockType.IllicitDrug;
        }
    }

    private void Update()
    {
        intervalTime += intervalTimeIncrement;
        if (intervalTime >= spawnInterval)
        {
            float spawnRangeScale = gameObject.transform.localScale.x / 2.0f;
            Vector2 spawnPos = new Vector2(
                Random.Range(-spawnRangeScale, spawnRangeScale),
                gameObject.transform.position.y
            );
            InstantiateRandomBlock(spawnPos);
            intervalTime = 0.0f;
        }        
    }
}
