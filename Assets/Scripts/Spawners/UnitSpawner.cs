using System.Collections;
using UnityEngine;

public class UnitSpawner : Spawner<Unit>
{ 
    [SerializeField] private Base _base;
    [SerializeField] private int _startAmount = 3;
    [SerializeField] private float _spawnDelay = 0.5f;

    private void Start() =>    
        StartCoroutine(SpawnUnits());    

    private IEnumerator SpawnUnits()
    {
        WaitForSeconds wait = new(_spawnDelay);

        for (int i = 0; i < _startAmount; i++)
        {
            Spawn();

            yield return wait;
        }        
    }
   
    protected override void SetUpObject(Unit unit)
    {
        base.SetUpObject(unit);
        unit.SetStartPosition(GetSpawnPosition());
        unit.SetBase(_base);
        _base.AddUnit(unit);
    }

    private void Spawn() =>   
        Pool.Get();
}