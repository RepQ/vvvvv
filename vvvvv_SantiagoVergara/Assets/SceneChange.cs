using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;


public class SceneChange : MonoBehaviour
{
    [Header("Scene To Load")]
    [SerializeField] SceneAsset SceneName;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.gameManager.ChangeScene(SceneName.name);
    }
}
