using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector2 ScreenToWorld(Camera camera, Vector2 position)
    {
        //return camera.ScreenToWorldPoint(position);
        //return camera.ScreenToWorldPoint(new Vector3(position.x / Screen.width, position.y / Screen.height, Camera.main.transform.position.z));

        return position;

    }
}
