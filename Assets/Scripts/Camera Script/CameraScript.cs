using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float offsetX;
    void Update()
    {
        if (BirdScript.instance != null)
            if (BirdScript.instance.isAlive)
            {
                MoveTheCamera();
            }
    }

    void MoveTheCamera()
    {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetPositionX() + offsetX;
        transform.position = temp;
    }
}
