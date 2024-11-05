using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    public bool isLeft;
    public bool isRight;
    private void Awake()
    {
        GameManager.gameManager.cameraLimits.Add(gameObject);
    }
}
