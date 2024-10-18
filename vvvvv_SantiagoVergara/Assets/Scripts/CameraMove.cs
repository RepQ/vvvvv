using System.Collections;
using UnityEngine;
using static Player;

public class CameraMove : MonoBehaviour
{
    static public CameraMove instance;
    public delegate void PlayerCreation();
    public event PlayerCreation OnPlayerCreation;

    [Header("PLayer References")]
    public Player playerToFollow;
    [Header("Camera Stats")]
    public float smoothTime = 0.3f;
    public float offsetX = 5f;
    public bool CameraON = true;

    [Header("Main Camara Reference")]
    [SerializeField] Camera mainCamera;

    private Vector3 velocity = Vector3.zero;

    [Header("Position of Target")]
    public Vector3 targetPosition;

    private void Awake()
    {
        if (instance == null)
        {
            mainCamera = Camera.main;
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {

    }

    void LateUpdate()
    {
        if (playerToFollow == null) return;
        if (CameraON == true)
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
    
    public void FollowTo(Vector3 target)
    {
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, target, ref velocity, smoothTime);
    }
    public Vector3 CalculateTargetPosition()
    {
        Vector3 playerPosition = playerToFollow.playerPosition;
        float playerVelocityX = playerToFollow.playerVelocity.x;

        float targetX = playerPosition.x + (playerVelocityX > 0 ? offsetX : -offsetX);

        return new Vector3(targetX, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
}
