using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class Player_SO : ScriptableObject
{
    public Vector2 playerPosition;
    public Vector2 playerVelocity;
}
