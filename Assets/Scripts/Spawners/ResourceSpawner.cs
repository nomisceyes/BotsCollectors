using System.Collections;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private float _spawnDelay = 1f;

    private void Start() =>
        StartCoroutine(SpawnResources());

    private IEnumerator SpawnResources()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (enabled)
        {
            Pool.Get().transform.position = GetSpawnPosition();

            yield return wait;
        }
    }
}