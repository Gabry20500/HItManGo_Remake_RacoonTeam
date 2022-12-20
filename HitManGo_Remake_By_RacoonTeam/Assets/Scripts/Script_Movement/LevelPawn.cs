using UnityEngine;

public class LevelPawn : MonoBehaviour
{
    static LevelPawn instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)
        {
            DestroyImmediate(this.gameObject);
        }

    }
}
