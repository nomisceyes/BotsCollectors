using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const KeyCode DownKey = KeyCode.Q;
    private const KeyCode UpKey = KeyCode.E;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const int RightMouseButton = 1;
    private const float RightAngle = 90;
    private const float ZeroRotation = 0f;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _moveDirection;
    private float _moveX;
    private float _moveZ;
    private float _rotateX;
    private float _rotateY;

    private void Update()
    {
        if (Input.GetMouseButton(RightMouseButton))
        {
            _rotateX += Input.GetAxis(MouseX) * _rotationSpeed;
            _rotateY -= Input.GetAxis(MouseY) * _rotationSpeed;
            _rotateY = Mathf.Clamp(_rotateY, -RightAngle, RightAngle);

            transform.localRotation = Quaternion.Euler(_rotateY, _rotateX, ZeroRotation);
        }

        if (Input.GetKey(DownKey))
        {
            transform.Translate(_movementSpeed * Time.deltaTime * Vector3.down, Space.World);
        }

        if (Input.GetKey(UpKey))
        {
            transform.Translate(_movementSpeed * Time.deltaTime * Vector3.up, Space.World);
        }

        _moveX = Input.GetAxis(Horizontal);
        _moveZ = Input.GetAxis(Vertical);

        _moveDirection = (_movementSpeed * _moveZ * transform.forward) + (_movementSpeed * _moveX * transform.right);

        transform.Translate(_moveDirection * Time.deltaTime, Space.World);
    }
}