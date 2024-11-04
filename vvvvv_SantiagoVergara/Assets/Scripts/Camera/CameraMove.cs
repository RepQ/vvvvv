using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class CameraMove : MonoBehaviour
{

    public delegate void PlayerCreation();
    public event PlayerCreation OnPlayerCreation;

    private GameManager gameManagerEvent;

    [Header("PLayer References")]
    public Player playerToFollow;

    [Header("Camera Stats")]
    private float smoothTime;
    public float offsetX;
    public GameObject CameraLimitLeft;
    public GameObject CameraLimitRight;

    private Vector3 cameraPosition;
    private float cameraHalfHeight;
    private float cameraHalfWidth;

    [Header("Main Camara Reference")]
    public Camera mainCamera;

    private Vector3 velocityX = Vector3.zero;
    private Vector3 velocityY = Vector3.zero;

    [Header("Position of Target")]
    public Vector3 targetPosition;

    private void Awake()
    {
        GameManager.gameManager.mainCamara = this;
    }
    void Start()
    {
        smoothTime = 0.5f;
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;
    }

    private void OnEnable()
    {
        cameraPosition = transform.position;
    }
    void LateUpdate()
    {
        if (playerToFollow == null) return;
        if (CameraLimitLeft != null && CameraLimitRight != null)
            targetPosition = CalculateTargetPosition();
        else
            targetPosition = cameraPosition;
        FollowTo(targetPosition);
    }

    private void Update()
    {
        if ((CameraLimitLeft == null || CameraLimitRight == null) && GameManager.gameManager.cameraLimits.Count > 0)
        {
            GameObject firstLimit = GameManager.gameManager.cameraLimits[0];
            GameObject secondLimit = GameManager.gameManager.cameraLimits[1];

            CameraLimitLeft = firstLimit.GetComponent<CameraLimits>().isLeft ? firstLimit : secondLimit;
            CameraLimitRight = CameraLimitLeft.Equals(firstLimit) ? secondLimit : firstLimit;
        }
        if (playerToFollow == null)
        {
            OnPlayerCreation?.Invoke();
        }
    }
    
    public void FollowTo(Vector3 target)
    {
        cameraPosition = Vector3.SmoothDamp(cameraPosition, new Vector3(cameraPosition.x, target.y, cameraPosition.z), ref velocityY, 0);
        cameraPosition = Vector3.SmoothDamp(cameraPosition, new Vector3 (target.x, cameraPosition.y, cameraPosition.z), ref velocityX, smoothTime);

        mainCamera.transform.position = cameraPosition;
    }
    public Vector3 CalculateTargetPosition()
    {
        Vector3 playerPosition = playerToFollow.playerPosition;
        Vector3 LimitLeft = CameraLimitLeft ? CameraLimitLeft.transform.position : cameraPosition;
        Vector3 LimitRight = CameraLimitRight ? CameraLimitRight.transform.position : cameraPosition;
        Vector3 LimitUp = new (cameraPosition.x, cameraHalfHeight);
        Vector3 LimitDown = new(cameraPosition.x, -cameraHalfHeight);
        float targetX = cameraPosition.x;
        float targetY = cameraPosition.y;

        if (Mathf.Abs(cameraPosition.x - playerPosition.x) >= cameraHalfWidth - offsetX)
            targetX = playerPosition.x;
        targetX = Mathf.Clamp(targetX, LimitLeft.x, LimitRight.x);

        if (Mathf.Abs(cameraPosition.y - playerPosition.y) >= cameraHalfHeight)
            targetY = cameraPosition.y + cameraHalfHeight * playerToFollow.verticalDirection * 2;
        return new Vector3(targetX, targetY, cameraPosition.z);
    }
}
