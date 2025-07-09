using UnityEngine;

public class ResourcePicker : MonoBehaviour
{
    [SerializeField] private Transform _handPosition;

    [field: SerializeField] public bool HasResource { get; private set; } = false;

    private Resource _currentResource;

    public void PickResource(Resource resource)
    {
        HasResource = true;   

        _currentResource = resource;
        _currentResource.IsPickUp();
        _currentResource.transform.SetParent(_handPosition);
        _currentResource.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void DropResource()
    {
        HasResource = false;
        _currentResource.transform.SetParent(null);
        _currentResource = null;
    }
}