using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Carga "GlobalScene" si aún no está cargada.
        if (SceneManager.GetSceneByName("GlobalScene").isLoaded == false)
        {
            SceneManager.LoadScene("GlobalScene", LoadSceneMode.Additive);
            GameManager.gameManager.lastPlayableScene = SceneManager.GetActiveScene().buildIndex;
        }

        // Cargar la primera escena de juego
        SceneManager.LoadScene("Escena1", LoadSceneMode.Additive);
        GameManager.gameManager.lastPlayableScene += 1;
    }
}
