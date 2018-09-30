using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public float spawnInterval;
    public float intervalTimeIncrement;
    private float intervalTime = 0.0f;
    public Timer timer;

    public GameObject prescriptionsPrefab;
    public GameObject taxReturnPrefab;
    public GameObject beerPrefab;

    private void InstantiateRandomBlock(Vector2 spawnPos)
    {
        int blockTypeNum = Random.Range(0, 4);
        if (blockTypeNum == 0)
        {
            GameObject block = Instantiate(
                beerPrefab,
                spawnPos,
                new Quaternion()
            );
            BlockScript blockScript = block.GetComponent<BlockScript>();
            blockScript.blockType = BlockScript.BlockType.Beer;
        }
        else if (blockTypeNum == 1)
        {
            GameObject block = Instantiate(
                taxReturnPrefab,
                spawnPos,
                new Quaternion()
            );
            BlockScript blockScript = block.GetComponent<BlockScript>();
            blockScript.blockType = BlockScript.BlockType.TaxReturn;
        }
        else if (blockTypeNum == 2)
        {
            GameObject block = Instantiate(
                prescriptionsPrefab,
                spawnPos,
                new Quaternion()
            );
            BlockScript blockScript = block.GetComponent<BlockScript>();
            blockScript.blockType = BlockScript.BlockType.DoctorAppointment;
        }
        else if (blockTypeNum == 3)
        {
            GameObject block = Instantiate(
                prescriptionsPrefab,
                spawnPos,
                new Quaternion()
            );
            BlockScript blockScript = block.GetComponent<BlockScript>();
            int badType = Random.Range(0, 2);
            if (badType == 1)
            {
                block.transform.localScale = new Vector3(3, 3, 1);
            }
            block.GetComponent<SpriteRenderer>().color = Color.black;
            blockScript.blockType = BlockScript.BlockType.IllicitDrug;
            int fallIncrease = (int)(timer.time / 10);
            if (fallIncrease <= 17.5f)
            {
                blockScript.fallSpeed += fallIncrease;
            }
            else
            {
                blockScript.fallSpeed += 17.5f;
            }
        }
    }

    private void Update()
    {
        if (Time.timeScale > 0)
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
}
