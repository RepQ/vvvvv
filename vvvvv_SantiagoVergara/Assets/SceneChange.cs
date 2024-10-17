using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneChange : MonoBehaviour
{
    [Header("Scene To Load")]
    public string SceneNameToLoad;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.gameManager.ChangeScene(SceneNameToLoad);
    }
}
