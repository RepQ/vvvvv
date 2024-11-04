using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // Objeto que se spawnea
    [SerializeField] private int maxSpawns; // Cantidad máxima de spawns

    public Player player;
    private Stack<GameObject> spawnStack; // Pila para manejar el pool de objetos
    private Vector3 spawnPosition;

    private void Awake()
    {
        GameManager.gameManager.playerSpawner = this; // Referencia en GameManager
        spawnStack = GameManager.gameManager.stack;
    }

    private void Start()
    {
        player = GameManager.gameManager.player;
        spawnPosition = transform.position;
        player = objectToSpawn.GetComponent<Player>();
        if (GameManager.gameManager.initGame == false)
        {
            StartCoroutine(SpawnGameObject(player.gameObject));
            GameManager.gameManager.initGame = true;
        }
    }

    public void Push(GameObject obj)
    {
        spawnStack.Push(obj);
        obj.SetActive(false); // Desactiva el objeto para guardarlo en el pool
    }

    public GameObject Pop(Vector3 position)
    {
        GameObject obj;

        obj = spawnStack.Pop();
        obj.SetActive(true); // Activa el objeto
        obj.transform.position = position; // Posiciona el objeto
        return obj;
    }

    public void ReSpawn(GameObject obj)
    {
        Push(obj); // Devuelve el objeto al pool
        Pop(spawnPosition); // Spawnea el objeto en la posición del Spawner
    }

    public IEnumerator SpawnGameObject(GameObject obj)
    {
        if (Count() == 0) // Si no hay objetos en la pila
        {
            GameObject newObj = Instantiate(obj, spawnPosition, Quaternion.identity); // Crea un nuevo objeto
        }
        else
            Pop(obj.transform.position);

        yield return null; // Ajusta el tiempo de espera según sea necesario
    }

    private int Count()
    {
        return spawnStack.Count;
    }
}
