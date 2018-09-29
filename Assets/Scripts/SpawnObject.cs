using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public float spawnInterval;
    public float intervalTimeIncrement;
    private float intervalTime = 0.0f;

    public GameObject objectPrefab;

    private void Update()
    {
        intervalTime += intervalTimeIncrement;
        if (intervalTime >= spawnInterval)
        {
            float spawnRangeScale = gameObject.transform.localScale.x / 2.0f;
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnRangeScale, spawnRangeScale),
                gameObject.transform.position.y
            );
            Instantiate(objectPrefab, spawnPosition, new Quaternion());
            intervalTime = 0.0f;
        }        
    }
}
