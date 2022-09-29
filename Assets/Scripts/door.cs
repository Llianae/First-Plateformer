using UnityEngine;

public class door : MonoBehaviour
{
    BoxCollider2D doorCollider;
    public bool isLocked = false;

    public string sceneToLoad;

    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.tag == "Player" && !isLocked)
        {
            GameManager.instance.LoadScene(sceneToLoad);
            PlayerBehaviour.instance.Respawn();
        }
    }
}
