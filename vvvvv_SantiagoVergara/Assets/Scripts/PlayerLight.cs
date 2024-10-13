using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Player playerToFollow;
    public float durationSmooth;
    public float offSetMovement;
    private Vector3 velocity = Vector2.zero;
    private Vector3 target;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerToFollow == null) return;
        target = CalculateTargetPosition();

        FollowTo(target);
    }

    public void FollowTo(Vector3 target)
    {
        // Suaviza el movimiento de la cámara
       transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, durationSmooth);
    }

    public Vector3 CalculateTargetPosition()
    {
        Vector3 playerPosition = playerToFollow.playerPosition;
        float playerVelocityX = playerToFollow.playerVelocity.x;
        float playerVelocityY = playerToFollow.playerVelocity.y;
        float targetX;

        // Calcula la posición objetivo basada en la posición del jugador y su velocidad
        if (playerVelocityX > 0)
        {
            targetX = playerPosition.x + offSetMovement;
        }
        else if (playerVelocityX < 0)
        {
            targetX = playerPosition.x - offSetMovement;
        }
        else
        {
            targetX = playerPosition.x;
        }
        float targetY = playerPosition.y + (playerVelocityY > 0 ? offSetMovement : -offSetMovement);

        // Mantén la posición Y y Z de la cámara
        return new Vector3(targetX, targetY, transform.position.z);
    }
}
