using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera_SO cameraData;
    public Player playerToFollow;
    public float smoothTime = 0.3f;
    public float offsetX = 5f; // Offset horizontal para anticipar el movimiento del jugador
    public float transitionThreshold = 0.1f; // Umbral para iniciar la transición

    private Camera mainCamera;
    private Vector2 sizeViewCamera;
    private Vector3 velocity = Vector3.zero;
    private float lastTransitionTime;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        InitPosition();
        CalculateCameraSize();
    }

    void LateUpdate()
    {
        if (playerToFollow == null) return;

        Vector3 targetPosition = CalculateTargetPosition();

        // Suaviza el movimiento de la cámara

        if (playerToFollow.isDashing)
            UpdateCameraPosition();
        else
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 playerPosition = playerToFollow.transform.position;
        float playerVelocityX = playerToFollow.rg2d.velocity.x;

        // Calcula la posición objetivo basada en la posición del jugador y su velocidad
        float targetX = playerPosition.x + (playerVelocityX > 0 ? offsetX : -offsetX);

        // Mantén la posición Y y Z de la cámara
        return new Vector3(targetX, transform.position.y, transform.position.z);
    }

    public void InitPosition()
    {
        if (cameraData != null)
        {
            transform.position = cameraData.initPosition;
        }
    }

    private void CalculateCameraSize()
    {
        sizeViewCamera.x = mainCamera.orthographicSize * mainCamera.aspect;
        sizeViewCamera.y = mainCamera.orthographicSize;
    }

    // Método para recalcular la posición de la cámara si el jugador se teletransporta
    public void UpdateCameraPosition()
    {
        if (playerToFollow != null)
        {
            Vector3 newPosition = CalculateTargetPosition();
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime * 5);
            velocity = Vector3.zero; // Resetea la velocidad para evitar deslizamientos no deseados
        }
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraMove : MonoBehaviour
//{
//    public Camera_SO cameraData;

//    private Camera mainCamera;
//    private Vector2 sizeViewCamara;

//    public Player playerToFollow;
//    public Vector3 cameraPosition;
//    public float durationSmooth;
//    private void Awake()
//    {
//        mainCamera = Camera.main;
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        InitPosition();
//        sizeViewCamara.x = mainCamera.orthographicSize * mainCamera.aspect;
//        sizeViewCamara.y = mainCamera.orthographicSize;
//    }


//    void LateUpdate()
//    {
//        if (playerToFollow.rg2d.position.x > cameraPosition.x + sizeViewCamara.x)
//        {
//            StartCoroutine(SmoothCameraTransition(new Vector3((cameraPosition.x + sizeViewCamara.x) * 2, cameraPosition.y, cameraPosition.z)));
//        }
//        else if (playerToFollow.rg2d.position.x < cameraPosition.x - sizeViewCamara.x)
//        {
//            StartCoroutine(SmoothCameraTransition(new Vector3((cameraPosition.x - sizeViewCamara.x) * -2, cameraPosition.y, cameraPosition.z)));
//        }
//    }

//    public void CheckPlayerPosition()
//    {
//        if (playerToFollow != null)
//        {

//        }
//    }
//    public void InitPosition()
//    {
//        cameraPosition = cameraData.initPosition;
//        StartCoroutine(SmoothCameraTransition(cameraPosition));
//    }


//    private IEnumerator SmoothCameraTransition(Vector3 targetPosition)
//    {
//        float elapsed = 0f;

//        Vector3 startPosition = mainCamera.transform.position;
//        while (elapsed < durationSmooth)
//        {
//            elapsed += Time.deltaTime;

//            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / durationSmooth);

//            yield return null;
//        }

//        mainCamera.transform.position = targetPosition;
//        cameraPosition = mainCamera.transform.position;
//    }
//}
