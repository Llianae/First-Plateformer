using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D doorCollider;
    public bool isLocked = false;

    public string sceneToLoad;

    public static Door instance;

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

        doorCollider = GetComponent<BoxCollider2D>();
        if (isLocked) ColliderEnabling(false);
        else ColliderEnabling(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isLocked)
        {
            GameManager.instance.LoadScene(sceneToLoad);
            PlayerBehaviour.instance.Respawn();
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        ColliderEnabling(true);
    }

    public void LockDoor()
    {
        isLocked = true;
        ColliderEnabling(false);
    }

    private void ColliderEnabling(bool state)
    {
        doorCollider.enabled = state;
    }
}
