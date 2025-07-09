using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources;
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void Start()
    {
        _scanner.ResourceFound += AddResource;

        StartCoroutine(WorkCoroutine());
    }

    private IEnumerator WorkCoroutine()
    {
        while (enabled)
        {
            if (_scanner.IsScanning == false)
            {
                _scanner.StartScan();
            }
          
            AssignResourceToUnit();

            yield return null;
        }
    }

    public void AddUnit(Unit unit) =>
        _units.Add(unit);

    public void CollectResource(Resource resource)
    {
        resource.MarkAsTaken();
        _resources.Remove(resource);
        _scoreCounter.AddScore();
    }

    private void AddResource(Resource resource)
    {
        if (_resources.Contains(resource) == false && resource.IsReserved == false)
            _resources.Add(resource);
    }

    private void AssignResourceToUnit()
    {
        foreach (Unit unit in _units.Where(unit => unit.IsBusy == false))
        {
            Resource resource = FindResource(unit.transform.position);

            if (resource != null && resource.TryReserve())
            {
                unit.AssignResource(resource);
                _resources.Remove(resource);
            }
        }
    }

    private Resource FindResource(Vector3 fromPosition)
    {
        return _resources
            .Where(resource => resource.IsReserved == false)
            .OrderBy(resource => (resource.transform.position - fromPosition).sqrMagnitude)
            .FirstOrDefault();
    }

    private void OnDestroy() =>
        _scanner.ResourceFound -= AddResource;
}