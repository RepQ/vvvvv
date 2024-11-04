using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool initGame = false;
    public static GameManager gameManager;
    public Stack<GameObject> stack;

    public bool isChangingScene = false;

    public int lastPlayableScene;
    public List<GameObject> cameraLimits;

    [Header(" Global References")]
    public Spawner playerSpawner;

    public CameraMove mainCamara;
    public LifeBar playerLifeBar;
    public Player player;

    [Header("PLayer Stats")]
    public bool playerIsDeath = false;
    public int playerLifes;
    public Vector2 playerVelocityinit;
    public float playerGravity;

    public GameObject playerRePosition;

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //private static void InitializeGameManager()
    //{
    //    if (gameManager == null)
    //    {
    //        GameObject gameManagerObject = new GameObject("GameManager");
    //        gameManager = gameManagerObject.AddComponent<GameManager>();
    //        DontDestroyOnLoad(gameManagerObject);
    //    }
    //}
    private void Awake()
    {
        if (gameManager == null && gameManager != this)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(playerRePosition);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stack = new Stack<GameObject>();
        cameraLimits = new List<GameObject>();
    }

    public void DeathPlayerHandle()
    {
        player.ResetPlayer();
        playerSpawner.ReSpawn(player.gameObject);
        if (--playerLifes <= 0)
        {
            playerSpawner.Push(player.gameObject);
        }
    }

    public void DestroyGameObject(GameObject obj)
    {
        Destroy(obj);
    }

    public void ChangeScene()
    {
        isChangingScene = true;
        playerSpawner.Push(player.gameObject);
        cameraLimits.Clear();
    }

    public void NewScene(GameObject obj)
    {
        isChangingScene = false;
        playerRePosition.transform.position = new Vector3 (obj.transform.position.x + obj.transform.localScale.x * player.horizontalDirection,
            player.transform.position.y, obj.transform.position.z);
        StartCoroutine(playerSpawner.SpawnGameObject(playerRePosition));
    }
}
