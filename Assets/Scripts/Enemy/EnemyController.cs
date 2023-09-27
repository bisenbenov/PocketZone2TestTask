using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _awareRadius;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Transform _player;

    private float _distance;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_enemy && _player)
        {
            _distance = Vector2.Distance(_enemy.position, _player.position);
        }
        
        Move();
        Attack();
    }

    private void Move()
    {
        if (_distance < _awareRadius)
        {
            if (_enemy && _player)
            {
                _enemy.position = Vector2.MoveTowards(_enemy.position, _player.localPosition, _speed * Time.deltaTime);
            }
            _animator.SetBool("IsChasing", true);
        }
        else
        {
            _animator.SetBool("IsChasing", false);
        }
    }

    private void Attack()
    {
        if (_distance < _attackDistance)
        {
            _animator.SetBool("IsNear", true);
        }
        else
        {
            _animator.SetBool("IsNear", false);
        }
    }
}
