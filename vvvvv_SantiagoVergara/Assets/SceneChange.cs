using System;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SceneChange : MonoBehaviour
{
    private GameObject playerRePosition;
    private bool isChangingScene;
    [Header("Scene To Load")]
    public int actualSceneIndex;
    public int nextSceneIndex;
    public bool next;
    public bool prev;

    private void Awake()
    {
        playerRePosition = GameManager.gameManager.playerRePosition;
        isChangingScene = GameManager.gameManager.isChangingScene;
    }
    private void Start()
    {
        if (isChangingScene)
        {
            GameManager.gameManager.NewScene(gameObject);
        }
        actualSceneIndex = GameManager.gameManager.lastPlayableScene;
        if (next)
        {
            nextSceneIndex = actualSceneIndex + 1;
        }
        else if (prev)
        {
            nextSceneIndex = actualSceneIndex - 1;
        }
        GameManager.gameManager.lastPlayableScene = nextSceneIndex;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //GameManager.gameManager.playerSpawner.transform.position = collider.transform.position;
        SceneManager.UnloadSceneAsync(actualSceneIndex);
        SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Additive);
        GameManager.gameManager.ChangeScene();
    }
}
