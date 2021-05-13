using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] GameObject pipePrefab = null;
    [SerializeField] float spawnDelay = 0f;
    [SerializeField] Vector2 heightRange = Vector2.zero;
    [SerializeField] int poolSize;
    [SerializeField] bool objectPoolIsExpandable;
    private List<GameObject> objectPool;

    void Awake()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pipe = Instantiate(pipePrefab);
            pipe.SetActive(false);
            objectPool.Add(pipe);
        }
    }

    void OnEnable()
    {
        StartCoroutine("SpawnPipe");
    }

    void OnDisable()
    {
        StopCoroutine("SpawnPipe");
        foreach ( GameObject pooledObject in objectPool )
        {
            pooledObject.SetActive(false);
        }
    }

    void Update()
    {

    }

    IEnumerator SpawnPipe()
    {
        while(true)
        {
            GameObject pipe = GetAvailableObject();
                if ( pipe ) {
                    pipe.transform.position = new Vector3(transform.position.x, 
                                                    Random.Range(heightRange.x, heightRange.y),
                                                    transform.position.z);
                    pipe.SetActive(true);
                }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    GameObject GetAvailableObject()
    {
        foreach ( GameObject pooledObject in objectPool )
        {
            if (!pooledObject.activeInHierarchy)
            {
                return pooledObject;
            }
        }
        
        if ( objectPoolIsExpandable )
        {
            GameObject pooledObject = Instantiate(pipePrefab);
            pooledObject.SetActive(false);
            objectPool.Add(pooledObject);
            return pooledObject;
        }
        else
        return null;
    }
}
