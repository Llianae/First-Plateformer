using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    private int deathCount = 0;
    public Text deathCountText;
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

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayerBehaviour.instance.Respawn();
    }

    public void Death()
    {
        PlayerBehaviour.instance.Death();
        deathCount++;
        deathCountText.text = "Death Count: " + deathCount;
        StartCoroutine(RespawnPlayer());
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2f);
        PlayerBehaviour.instance.Respawn();
    }
}
