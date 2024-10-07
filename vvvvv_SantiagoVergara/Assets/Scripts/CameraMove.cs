using UnityEngine;

public class CameraMove : MonoBehaviour
{
    static public CameraMove instance;
    public Camera_SO cameraData;
    public Player playerToFollow;
    public float smoothTime = 0.3f;
    public float offsetX = 5f; // Offset horizontal para anticipar el movimiento del jugador

    private Camera mainCamera;
    private Vector2 sizeViewCamera;
    private Vector3 velocity = Vector3.zero;
    public Vector3 targetPosition;
    public Vector3 staticPosition;

    private void Awake()
    {
        if (instance == null)
        {
            mainCamera = Camera.main;
            instance = this;
        }
        else
            Destroy(instance);
    }

    void Start()
    {
        InitPosition();
        CalculateCameraSize();
    }

    void LateUpdate()
    {
        if (playerToFollow == null) return;

        targetPosition = CalculateTargetPosition();
        FollowPlayer(targetPosition);
  
    }
    public void FollowPlayer(Vector3 target)
    {
        // Suaviza el movimiento de la c�mara
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 playerPosition = playerToFollow.transform.position;
        float playerVelocityX = playerToFollow.rg2d.velocity.x;

        // Calcula la posici�n objetivo basada en la posici�n del jugador y su velocidad
        float targetX = playerPosition.x + (playerVelocityX > 0 ? offsetX : -offsetX);

        // Mant�n la posici�n Y y Z de la c�mara
        return new Vector3(targetX, transform.position.y, transform.position.z);
    }

    public void InitPosition()
    {
        if (cameraData != null)
        {
            transform.position = cameraData.initPosition;
            staticPosition = transform.position;
        }
    }

    private void CalculateCameraSize()
    {
        sizeViewCamera.x = mainCamera.orthographicSize * mainCamera.aspect;
        sizeViewCamera.y = mainCamera.orthographicSize;
    }

    // M�todo para recalcular la posici�n de la c�mara si el jugador se teletransporta
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
