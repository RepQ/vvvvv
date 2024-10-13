using UnityEngine;

public class CameraMove : MonoBehaviour, Followers
{
    static public CameraMove instance;
    public Player playerToFollow;

    public float smoothTime = 0.3f;
    public float offsetX = 5f; // Offset horizontal para anticipar el movimiento del jugador

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    public Vector3 targetPosition;


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

    }

    void LateUpdate()
    {
        if (playerToFollow == null) return;

        targetPosition = CalculateTargetPosition();
        FollowTo(targetPosition);
  
    }
    public void FollowTo(Vector3 target)
    {
        // Suaviza el movimiento de la c�mara
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, target, ref velocity, smoothTime);
    }

    public Vector3 CalculateTargetPosition()
    {
        Vector3 playerPosition = playerToFollow.playerPosition;
        float playerVelocityX = playerToFollow.playerVelocity.x;

        // Calcula la posici�n objetivo basada en la posici�n del jugador y su velocidad
        float targetX = playerPosition.x + (playerVelocityX > 0 ? offsetX : -offsetX);

        // Mant�n la posici�n Y y Z de la c�mara
        return new Vector3(targetX, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
}
