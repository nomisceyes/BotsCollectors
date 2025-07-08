using UnityEngine;
using UnityEngine.Pool;

public class Spawner<TObject> : MonoBehaviour where TObject : MonoBehaviour, IPoolable<TObject>
{
    [SerializeField] protected int PoolCapacity;
    [SerializeField] protected int MaxPoolSize;
    [SerializeField] private TObject _prefab;

    protected ObjectPool<TObject> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<TObject>(
              createFunc: () => Instantiate(_prefab),
              actionOnGet: (@object) => SetUpObject(@object),
              actionOnRelease: (@object) => ResetObject(@object),
              defaultCapacity: PoolCapacity,
              maxSize: MaxPoolSize);
    }

    public virtual void Release(TObject @object) =>
        Pool.Release(@object);

    protected virtual void ResetObject(TObject @object)
    {
        @object.Destroyed -= Release;
        @object.gameObject.SetActive(false);
    }

    protected virtual void SetUpObject(TObject @object)
    {
        @object.Destroyed += Release;
        @object.gameObject.SetActive(true);
    }
}