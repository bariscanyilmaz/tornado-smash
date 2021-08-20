using System.Collections;
using UnityEngine;
public class Tornado : MonoBehaviour
{
    Rigidbody _rb;
    Ground _ground;
    Obstacle _obstacle;
    CapsuleCollider _collider;

    [SerializeField] Transform _target;
    [SerializeField] TornadoPropertySO tornadoPropertyData;
    public float Radius => tornadoPropertyData.MinDistance;

    WaitForSeconds _delayTime=new WaitForSeconds(0.01f);
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstants.ObstacleTagName))
        {
            if (other.attachedRigidbody.isKinematic)
            {
                other.GetComponent<Rigidbody>().isKinematic = false;
            }

            if ((_target.position - other.transform.position).magnitude < tornadoPropertyData.MaxDistance + 2)
            {
                _obstacle = other.GetComponent<Obstacle>();
                _obstacle.Init(this, _rb, tornadoPropertyData.SpringForce, tornadoPropertyData.MaxDistance, tornadoPropertyData.MinDistance / 2, tornadoPropertyData.Damper);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GameConstants.ObstacleTagName))
        {
            _obstacle = other.GetComponent<Obstacle>();
            Vector3 dir = (_target.transform.position - other.transform.position);
            if (_obstacle.IsInit)
            {
                if (dir.magnitude < tornadoPropertyData.MinDistance)
                {
                    _obstacle.Attach(_target);
                    GameManager.Instance.IncreaseCollected();
                    UIManager.Instance.UpdateLevelBar(GameManager.Instance.Collected, GameManager.Instance.Total);
                }
                else
                {
                    _obstacle.Pull(dir.normalized * tornadoPropertyData.Force);
                }
            }
            else if ((_target.position - other.transform.position).magnitude < tornadoPropertyData.MaxDistance + 2)
            {
                _obstacle.Init(this, _rb, tornadoPropertyData.SpringForce, tornadoPropertyData.MaxDistance, tornadoPropertyData.MinDistance / 2, tornadoPropertyData.Damper);
            }
        }

        if (GameState.PreFinish == GameManager.Instance.GameStatus && other.CompareTag(GameConstants.GroundTagName))
        {
            _ground = other.GetComponent<Ground>();
            if (!_ground.enabled)
            {
                _ground.Init(_target);
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstants.ObstacleTagName))
        {
            _obstacle = other.GetComponent<Obstacle>();
            if (_obstacle != null)
            {
                _obstacle.Release();
            }
        }
    }

    public IEnumerator FinishAnimation()
    {
        yield return _delayTime;

        if (_collider.radius < tornadoPropertyData.MaxRadius)
        {
            _collider.radius += (Time.deltaTime * tornadoPropertyData.RadiusReduceSpeed);
            StartCoroutine(FinishAnimation());
        }else {
            GameManager.Instance.LoadNextLevel();      
            GameManager.Instance.StartGame();
        }
    }
}