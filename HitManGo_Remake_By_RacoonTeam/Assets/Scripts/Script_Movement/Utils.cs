using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector2 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = 1f;
        //return camera.ScreenToWorldPoint(position);
        return camera.ViewportToWorldPoint(new Vector3(position.x / Screen.width, position.y / Screen.height, 3f));

    }
}
