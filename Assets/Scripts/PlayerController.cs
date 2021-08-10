using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool _isMove;
    float _axisX, _axisY;

    Plane _plane;
    Camera _cam;
    Rigidbody _rb;
    Vector3 _currentPosition, _startPosition;

    [SerializeField] float speed = 10f;

    void Start()
    {
        _plane = FindObjectOfType<Plane>();
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (GameManager.Instance.GameStatus == GameState.Play)
        {
            GetInputs();
            Move();
            CheckPosition();
        }

        if (GameManager.Instance.GameStatus == GameState.Pause && Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.StartGame();
        }
    }
    void GetInputs()
    {
        _axisX = Input.GetAxis(GameConstants.HorizontalAxisName);
        _axisY = Input.GetAxis(GameConstants.VerticalAxisName);

        if (Input.GetMouseButtonDown(0))
        {
            _isMove = true;
            _startPosition = GetMousePosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMove = false;

        }

        if (_isMove)
        {
            _currentPosition = GetMousePosition();
            _axisX = _currentPosition.x - _startPosition.x;
            _axisY = _currentPosition.y - _startPosition.y;

        }
    }

    void CheckPosition()
    {
        if (_rb.position.x > _plane.MaxWidth || _rb.position.x < -_plane.MaxWidth)
        {
            _rb.position = new Vector3(Mathf.Sign(_rb.position.x) * _plane.MaxWidth, _rb.position.y, _rb.position.z);
        }

        if (_rb.position.z > _plane.MaxHeight || _rb.position.z < -_plane.MaxHeight)
        {
            _rb.position = new Vector3(_rb.position.x, _rb.position.y, Mathf.Sign(_rb.position.z) * _plane.MaxHeight);
        }
    }

    void Move() => _rb.MovePosition(_rb.position + new Vector3(_axisX, 0, _axisY) * speed * Time.deltaTime);
    Vector3 GetMousePosition() => _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

}
