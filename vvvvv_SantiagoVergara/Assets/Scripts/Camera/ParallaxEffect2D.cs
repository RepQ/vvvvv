using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect2D : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 lastFramePosition;
    public float SmoothParallax;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        mainCamera = Camera.main;
        lastFramePosition = mainCamera.transform.position;
        //transform.position = new Vector3(lastFramePosition.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = mainCamera.transform.position.x - lastFramePosition.x;
        transform.Translate(new Vector3(deltaX * SmoothParallax, 0, 0));
        lastFramePosition = mainCamera.transform.position;
    }
}
