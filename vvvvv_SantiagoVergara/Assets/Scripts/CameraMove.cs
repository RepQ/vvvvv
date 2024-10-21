using System.Collections;
using UnityEngine;
using static Player;

public class CameraMove : MonoBehaviour
{

    public delegate void PlayerCreation();
    public event PlayerCreation OnPlayerCreation;

    [Header("PLayer References")]
    public Player playerToFollow;

    [Header("Camera Stats")]
    public float smoothTime = 0.3f;
    public float offsetX = 100f;
    public bool CameraON = true;
    private Vector3 cameraPosition;
    private float cameraHalfHeight;
    private float cameraHalfWidth;

    [Header("Main Camara Reference")]
    [SerializeField] Camera mainCamera;

    private Vector3 velocity = Vector3.zero;

    [Header("Position of Target")]
    public Vector3 targetPosition;

    private void Awake()
    {
    }

    void Start()
    {
        cameraPosition = transform.position;
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;
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
        if (playerToFollow.isDeath == true)
        {
            
        }
    }
    
    public void FollowTo(Vector3 target)
    {
       cameraPosition = Vector3.SmoothDamp(cameraPosition, target, ref velocity, smoothTime);
       mainCamera.transform.position = cameraPosition;
    }
    public Vector3 CalculateTargetPosition()
    {
        Vector3 playerPosition = playerToFollow.playerPosition;
        bool outOfLimitsRight = playerPosition.x > cameraPosition.x + cameraHalfWidth - offsetX;
        bool outOfLimitsLeft = playerPosition.x < cameraPosition.x - cameraHalfWidth + offsetX;
        float targetX = cameraPosition.x;

        if (outOfLimitsRight || outOfLimitsLeft)
        {
            targetX = playerPosition.x;
        }

        return new Vector3(targetX, cameraPosition.y, cameraPosition.z);
    }
}
