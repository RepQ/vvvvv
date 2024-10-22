using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int Spawns;

    private Stack<GameObject> spawnStack;
    private Coroutine coroutine;
    private Vector3 SpawnXY;
    private int CounterSpawns;

    // Start is called before the first frame update
    void Start()
    {
        spawnStack = GameManager.gameManager.stack;
        CounterSpawns = 0;
        SpawnXY = transform.position;
        coroutine = StartCoroutine(SpawnGameObject(objectToSpawn));
    }

    public void Push(GameObject obj)
    {
        spawnStack.Push(obj);
        obj.SetActive(false);
    }

    public GameObject Pop()
    {
        GameObject obj = spawnStack.Pop();
        obj.SetActive(true);
        obj.transform.position = SpawnXY;
        return obj;
    }

    public void ReSpawn(GameObject obj)
    {
        Push(obj);
        Pop();
    }
    private int Count()
    {
        return spawnStack.Count;
    }

    private IEnumerator SpawnGameObject(GameObject obj)
    {
        while (CounterSpawns < Spawns) 
        {
            if (Count() == 0)
            {
                Instantiate(objectToSpawn, SpawnXY, Quaternion.identity);
            }
            else
            {
                Pop();
            }
            CounterSpawns++;
        }
        yield return null;
    }
}
