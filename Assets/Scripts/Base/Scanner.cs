using System;
using System.Collections;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private LayerMask _scanLayerMask = ~0;
    [SerializeField] private float _maxRadius = 20f;
    [SerializeField] private float _scanInterval = 0.4f;
    [SerializeField] private float _scanSpeed = 5f;

    private Collider[] _scanResults = new Collider[20];
    private Coroutine _scanningCoroutine;
    private WaitForSeconds _scanDelay;
    private float _currentRadius;
    //private bool IsScanning;

    public event Action<Resource> ResourceFound;

    public bool IsScanning { get; private set; }

    private void Start()
    {
        _scanDelay = new(_scanInterval);
    }

    private IEnumerator ScanningProcess()
    {
        while (IsScanning)
        {
            UpdateScanRadius();
            PerformScan();
            VisualizeScan();

            yield return _scanDelay;

            if (_currentRadius >= _maxRadius)
                StopScan();
        }       
    }

    public float TimeToScannComplete() =>
        _maxRadius / _scanSpeed;
      
    public void StartScan()
    {
        _currentRadius = 0f;
        IsScanning = true;

        if (_scanningCoroutine != null)
            StopCoroutine(_scanningCoroutine);

        _scanningCoroutine = StartCoroutine(ScanningProcess());
    }

    private void PerformScan()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(
            transform.position,
            _currentRadius,
            _scanResults,
            _scanLayerMask);

        for (int i = 0; i < hitCount; i++)
        {
            if (_scanResults[i].TryGetComponent(out Resource resource) && resource.IsReserved == false)          
                ResourceFound?.Invoke(resource);          
        }
    }

    private void VisualizeScan()
    {
        Debug.DrawRay(transform.position, Vector3.right * _currentRadius, Color.green);
        Debug.DrawRay(transform.position, Vector3.forward * _currentRadius, Color.green);
    }

    private void UpdateScanRadius()
    {
        _currentRadius += _scanSpeed * Time.deltaTime;
        Debug.Log(_currentRadius);

        if (_currentRadius >= _maxRadius)
            StopScan();
    }

    private void StopScan()
    {
        if (IsScanning == false)
            return;

        IsScanning = false;

        if (_scanningCoroutine != null)
        {
            StopCoroutine(_scanningCoroutine);
            _scanningCoroutine = null;
        }
    }
}