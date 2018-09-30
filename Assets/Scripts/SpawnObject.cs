using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public float spawnInterval;
    public float intervalTimeIncrement;
    private float intervalTime = 0.0f;
	public Timer timer;

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
            block.GetComponent<SpriteRenderer>().color = Color.yellow;
            blockScript.blockType = BlockScript.BlockType.Beer;
        }
        else if (blockTypeNum == 1)
        {
            block.GetComponent<SpriteRenderer>().color = Color.green;
            blockScript.blockType = BlockScript.BlockType.TaxReturn;
        }
        else if (blockTypeNum == 2)
        {
            block.GetComponent<SpriteRenderer>().color = Color.red;
            blockScript.blockType = BlockScript.BlockType.DoctorAppointment;
        }
        else if (blockTypeNum == 3)
        {
            block.GetComponent<SpriteRenderer>().color = Color.black;
            blockScript.blockType = BlockScript.BlockType.IllicitDrug;
			blockScript.fallSpeed = 5 + (int)(timer.time / 5);
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
