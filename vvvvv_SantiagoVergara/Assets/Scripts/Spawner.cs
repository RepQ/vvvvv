using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int Spawns;

    private Stack<GameObject> stack;
    private Coroutine coroutine;
    private Vector3 SpawnXY;
    private int CounterSpawns;

    // Start is called before the first frame update
    void Start()
    {
        CounterSpawns = 0;
        SpawnXY = transform.position;
        stack = new Stack<GameObject>();
        coroutine = StartCoroutine(SpawnGameObject(objectToSpawn));
    }

    private void Push(GameObject obj)
    {
        stack.Push(obj);
        obj.SetActive(false);
    }

    private GameObject Pop()
    {
        GameObject obj = stack.Pop();
        obj.SetActive(true);
        return obj;
    }

    private int Count()
    {
        return stack.Count;
    }
    // Update is called once per frame
    void Update()
    {

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
