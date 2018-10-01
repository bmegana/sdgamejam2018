using UnityEngine;

public class MoveGameUp : MonoBehaviour
{
    public GameObject cameraObj;
    public float cameraMoveSpeed;

    private float moveDownTime = 0.0f;
    private Vector3 initCamPos;

    public GameObject spawnLocation;
    public GameObject player;
    private GameObject head;

    private void Awake()
    {
        head = player.transform.GetChild(0).gameObject;
        initCamPos = cameraObj.transform.position;
    }

    private void FixedUpdate()
    {
        if (head.transform.position.y + 2.0f > cameraObj.transform.position.y)
        {
            Vector3 newCameraPos = new Vector3(
                cameraObj.transform.position.x,
                head.transform.position.y + 6.0f,
                cameraObj.transform.position.z
            );
            cameraObj.transform.position = Vector3.Lerp(
                cameraObj.transform.position,
                newCameraPos,
                Time.deltaTime * cameraMoveSpeed
            );
        }
    }

    private void Update()
    {
        /*
         * Code below should be within this function because Update is not
         * dependent on time, unlike FixedUpdate.
         */
        if (GameControl.instance.gameIsOver &&
            cameraObj.transform.position.y != initCamPos.y)
        {
            float moveDownDec = 0.0001f;
            Vector3 gameOverCamPos = new Vector3(
                player.transform.position.x,
                initCamPos.y,
                initCamPos.z
            );
            moveDownDec /= Vector3.Distance(
                cameraObj.transform.position, gameOverCamPos
            );
            moveDownTime += 0.0001f;
            cameraObj.transform.position = Vector3.Lerp(
                cameraObj.transform.position,
                gameOverCamPos,
                moveDownTime * (cameraMoveSpeed / 2.0f)
            );
        }
    }
}
