using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    static public PlayerBehaviour instance;
    private GameObject player;
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

        player = this.gameObject;
    }

    public void Death()
    {
        player.SetActive(false);
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.rb.velocity = Vector3.zero;
    }

    public void Respawn()
    {
        PlayerMovement.instance.ResetPlayer(SpawnPoint.instance.getSpawnPoint() + new Vector2(0, 1));
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        player.SetActive(true);
    }


}
