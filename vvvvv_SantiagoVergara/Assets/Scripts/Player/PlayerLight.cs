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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    public void FollowTo(Vector3 target)
    {
       transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, durationSmooth);
    }
    
    public Vector3 CalculateTargetPosition()
    {
        Vector3 playerPosition = playerToFollow.playerPosition;
        float playerVelocityX = playerToFollow.rg2d.velocity.x;
        float playerVelocityY = playerToFollow.rg2d.velocity.y;
        float targetX;
        float targetY;

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
        targetY = playerPosition.y;

        return new Vector3(targetX, targetY, transform.position.z);
    }
}
