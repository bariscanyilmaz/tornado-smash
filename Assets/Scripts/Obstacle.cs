using UnityEngine;

public class Obstacle : BaseTransform
{

    bool _isInit;
    float _delta = 0, _localHeight;
    float _localScale = 1;
    float _angle;

    Rigidbody _rb;
    SpringJoint _spring;
    Transform _target;
    Tornado _tornado;
    Collider _coll;

    [SerializeField] bool _isRotate = false;
    [SerializeField] RotatePropertySO obstacleRotatePropertyData;

    public bool IsInit => _isInit;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _coll = GetComponent<Collider>();
    }

    void Update()
    {
        if (_isRotate)
        {
            _delta = Time.deltaTime;

            _localHeight += _delta * obstacleRotatePropertyData.RiseSpeed;
            _localScale -= _delta * obstacleRotatePropertyData.ScaleSpeed;

            transform.localPosition = Rotate(_localHeight, _angle, _tornado.Radius);
            transform.localScale = Scale(_localScale);

            _angle += _delta * obstacleRotatePropertyData.RotateSpeed;

            if (_localScale < 0.1f || _localHeight > 10)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Pull(Vector3 force)
    {
        _rb.AddForce(force);
    }
    public void Init(Tornado tornado, Rigidbody tornadoRb, float springForce, float maxDistance, float minDistance, float damper)
    {
        _isInit = true;
        enabled = true;
        //save tornado reference
        _tornado = tornado;

        //initialize the spring
        _spring = gameObject.AddComponent<SpringJoint>();
        _spring.spring = springForce;
        _spring.damper = damper;
        _spring.breakForce = 50f;
        _spring.maxDistance = maxDistance;
        _spring.minDistance = minDistance;
        _spring.connectedBody = tornadoRb;
        _spring.autoConfigureConnectedAnchor = false;

        //set initial position of the caught object relative to its position and the tornado
        Vector3 initialPosition = Vector3.zero;
        initialPosition.y = transform.position.y;
        _spring.connectedAnchor = initialPosition;
    }
    public void Attach(Transform target)
    {
        _rb.isKinematic = true;
        _target = target;
        transform.SetParent(target);
        _isRotate = true;
        _angle = GetAngle(target);
        _localHeight = transform.localPosition.y;
        _rb.freezeRotation = true;
        if (_spring)
        {
            Destroy(_spring);
        }
        if (_coll)
        {
            _coll.enabled = false;
        }
    }

    public void Release()
    {
        Destroy(_spring);
        _isInit = false;
        enabled = false;
    }

    float GetAngle(Transform target) => Mathf.Atan2(target.localPosition.z - transform.localPosition.z, target.localPosition.x - transform.localPosition.x) * Mathf.Rad2Deg;

}
