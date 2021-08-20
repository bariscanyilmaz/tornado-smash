using UnityEngine;
using Zenject;

public class Ground : MonoBehaviour
{
    bool _isRotate;
    float _delta = 0;
    float _angle, _localScale = 1, _radius;
    Transform _target;
    TransformService _transformService;
    [SerializeField] RotatePropertySO groundRotatePropertyData;

    [Inject]
    public void Contstruct(TransformService transformService)
    {
        _transformService = transformService;
    }

    void Start()
    {
        transform.SetParent(_target);
        GetComponent<Collider>().enabled = false;

        _angle = Mathf.Atan2(transform.position.z, transform.position.x) * Mathf.Rad2Deg;
        _radius = Mathf.Sqrt(transform.position.x * transform.position.x + transform.position.y * transform.position.y);
    }

    public void Init(Transform target)
    {
        _target = target;
        enabled = true;
    }

    void Update()
    {

        _delta = Time.deltaTime;
        _localScale -= _delta * groundRotatePropertyData.ScaleSpeed;

        transform.localPosition = _transformService.Rotate(_angle, 0, _radius);
        transform.localScale = _transformService.Scale(_localScale);

        _angle += _delta * groundRotatePropertyData.RotateSpeed;
        _radius -= _delta * groundRotatePropertyData.RadiusReduceSpeed;
        if (_localScale < 0.1f || _radius < .1f)
        {
            gameObject.SetActive(false);
        }

    }
    public void SetRotate(bool isRotate)
    {
        _isRotate = isRotate;
    }
}
