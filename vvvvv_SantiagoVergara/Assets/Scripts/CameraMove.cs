using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public delegate void PlayerCreation();
    public event PlayerCreation OnPlayerCreation;

    static public CameraMove instance;

    [Header("PLayer References")]
    public Player playerToFollow;
    [Header("Camera Stats")]
    public float smoothTime = 0.3f;
    public float offsetX = 5f;
    public bool CameraON = true;

    [Header("Main Camara Reference")]
    [SerializeField] Camera mainCamera;

    private Vector2 velocity = Vector2.zero;

    [Header("Position of Target")]
    public Vector2 targetPosition;

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
        if (CameraON)
        {
            targetPosition = CalculateTargetPosition();
        }
            FollowTo(targetPosition);
    }

    private void Update()
    {
        if (playerToFollow == null)
        {
            OnPlayerCreation?.Invoke();
        }
    }
    
    public void FollowTo(Vector2 target)
    {
        // Suaviza el movimiento de la c�mara
        mainCamera.transform.position = Vector2.SmoothDamp(mainCamera.transform.position, target, ref velocity, smoothTime);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
    }

    public Vector3 CalculateTargetPosition()
    {
        Vector2 playerPosition = playerToFollow.playerPosition;
        float playerVelocityX = playerToFollow.playerVelocity.x;

        // Calcula la posici�n objetivo basada en la posici�n del jugador y su velocidad
        float targetX = playerPosition.x + (playerVelocityX > 0 ? offsetX : -offsetX);

        // Mant�n la posici�n Y y Z de la c�mara
        return new Vector2(targetX, mainCamera.transform.position.y);
    }
}
