using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    static LevelButtons instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            DestroyImmediate(this.gameObject);
        }

    }
}
