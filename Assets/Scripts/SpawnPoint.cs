using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    static public SpawnPoint instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public Vector2 getSpawnPoint()
    {
        return transform.position;
    }
}
