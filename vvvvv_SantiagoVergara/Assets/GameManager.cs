using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Stack<GameObject> stack;
    [SerializeField] Spawner playerSpawner;

    private Camera mainCamara;

    [Header("References")]
    public Player player;

    [Header("PLayer Stats")]
    public int playerLifes;
    public Vector2 playerVelocityinit;
    public Vector2 playerPositionInit;
    public float playerGravity;


    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
            stack = new Stack<GameObject>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamara = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathPlayerHandle()
    {
        //Camera.InitPosition();
        player.ResetPlayer();
        if (--playerLifes <= 0)
        {
            DestroyGameObject(player.gameObject);
        }
    }

    public void DestroyGameObject(GameObject obj)
    {
        Destroy(obj);
    }

    public void ChangeScene(string sceneName)
    {
        playerSpawner.Push(player.gameObject);
        SceneManager.LoadScene(sceneName);
    }
}
