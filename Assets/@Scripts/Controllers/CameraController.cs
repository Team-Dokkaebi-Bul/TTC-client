using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 2f, -10f);

    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;
    private TestController _playerController;

    private void Start()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform; // 지향하기
        }

        _playerController = _target.GetComponent<TestController>();
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        _desiredPosition = new Vector3(
            _target.position.x + _offset.x,
            _target.position.y + _offset.y,
            _offset.z
        );

        _smoothedPosition = Vector3.Lerp(
            transform.position,
            _desiredPosition,
            _smoothSpeed * Time.deltaTime
        );

        transform.position = _smoothedPosition;
    }
}