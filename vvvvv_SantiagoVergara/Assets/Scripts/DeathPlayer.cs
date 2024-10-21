using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
public class DeathPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.DeathPlayerHandle();
    }
}
