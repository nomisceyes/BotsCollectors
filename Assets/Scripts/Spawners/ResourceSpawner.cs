using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnDelay = 1f;

    private void Start() =>    
        StartCoroutine(SpawnResources());
    
    private IEnumerator SpawnResources()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (enabled)
        {
            foreach (Transform spawnPoint in _spawnPoints)
            {
                Pool.Get().transform.position = spawnPoint.position;

                yield return wait;
            }
        }
    }
}