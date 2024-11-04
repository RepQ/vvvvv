using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private Slider lifeBar;
    public Player player;
    public float currentLifePlayer;

    private void Awake()
    {
        lifeBar = GetComponent<Slider>();
        GameManager.gameManager.playerLifeBar = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            CheckLifePlayer();
        }
    }

    private void CheckLifePlayer()
    {
        float newLife = player.playerLife;
        currentLifePlayer = Mathf.Lerp(currentLifePlayer, newLife, Time.deltaTime * 2f);
        lifeBar.value = currentLifePlayer;
    }
}
