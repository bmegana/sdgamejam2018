using UnityEngine;

public class HeadControl : MonoBehaviour {

	public static HeadControl instance;

    public GameObject head;
    public float highestBlockHeight = -8.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    public void UpdateHeadPosition(Vector2 newPos)
    {
        head.transform.position = newPos;
        highestBlockHeight = newPos.y;
    }
}
