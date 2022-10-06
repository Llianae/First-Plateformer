using UnityEngine;

public class Door : MonoBehaviour
{
    public Door instance;
    BoxCollider2D doorCollider;
    private bool isLocked = false;

    public string sceneToLoad;

    public void Awake()
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

    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isLocked)
        {
            doorCollider.enabled = false;
        }
        else
        {
            doorCollider.enabled = true;
        }
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

    public void Unlock()
    {
        isLocked = false;
    }
}
