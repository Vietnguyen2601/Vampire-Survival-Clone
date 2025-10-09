using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public List<GameObject> propSpawnsPoint;
    public List<GameObject> propPrefabs;
    void Start()
    {
        SpawnsProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnsProps()
    {
        foreach (var spawnPoint in propSpawnsPoint)
        {
            int randomIndex = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[randomIndex], spawnPoint.transform.position, Quaternion.identity);
            prop.transform.parent = spawnPoint.transform;
        }
    }
}
