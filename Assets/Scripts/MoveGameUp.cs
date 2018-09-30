using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameUp : MonoBehaviour
{
    public GameObject cameraObj;
    public float cameraMoveSpeed;
    private Vector3 stageDimensions;

    public GameObject spawnLocation;
    public GameObject player;
    private GameObject head;

    private void Awake()
    {
        head = player.transform.GetChild(0).gameObject;
        stageDimensions = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0)
        );
    }

    private void FixedUpdate()
    {
        if (head.transform.position.y > cameraObj.transform.position.y)
        {
            Vector3 newCameraPos = new Vector3(
                cameraObj.transform.position.x,
                head.transform.position.y,
                cameraObj.transform.position.z
            );
            cameraObj.transform.position = Vector3.Lerp(
                cameraObj.transform.position,
                newCameraPos,
                Time.deltaTime * cameraMoveSpeed
            );
            Debug.Log("Stage Dimenstions: [" +
                (stageDimensions.x + cameraObj.transform.position.x) +
                ", " +
                (stageDimensions.y + cameraObj.transform.position.y)
                + "]");
        }
    }
}
