using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera_SO cameraData;

    private Camera mainCamera;
    private Vector2 sizeViewCamara;

    public Player player;
    private float verticalView;
    private float horizontaView;
    public Vector3 cameraPosition;
    public float durationSmooth;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitPosition();
        sizeViewCamara.x = mainCamera.orthographicSize * mainCamera.aspect;
        sizeViewCamara.y = mainCamera.orthographicSize;
    }

    
    void LateUpdate()
    {
        if (player.rg2d.position.x > cameraPosition.x + sizeViewCamara.x)
        {

        }
    }

    public void CheckPlayerPosition()
    {
        if (player != null)
        {

        }
    }
    public void InitPosition()
    {
        cameraPosition = cameraData.initPosition;
        StartCoroutine(SmoothCameraTransition(cameraPosition));
    }
    public void ChangeScenarioHorizontal(Collider2D collider, float direction)
    {
        cameraPosition = new Vector3
            (cameraPosition.x + Mathf.Abs(collider.transform.position.x - cameraPosition.x) * 2 * direction,
            cameraPosition.y, 
            cameraPosition.z);
        StartCoroutine(SmoothCameraTransition(cameraPosition));
    }

    public void ChangeScenarioVertical(Collider2D collider, float direction)
    {
        cameraPosition = new Vector3
            (cameraPosition.x,
            cameraPosition.y + Mathf.Abs(collider.transform.position.y - cameraPosition.y) * 2 * direction,
            cameraPosition.z);
        StartCoroutine(SmoothCameraTransition(cameraPosition));
    }
    private IEnumerator SmoothCameraTransition(Vector3 targetPosition)
    {
        float elapsed = 0f;

        Vector3 startPosition = mainCamera.transform.position;
        while (elapsed < durationSmooth)
        {
            elapsed += Time.deltaTime;

            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / durationSmooth);

            yield return null;
        }

        mainCamera.transform.position = targetPosition;
    }
}
