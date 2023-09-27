using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;

    private InputSystem _inputSystem;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
    }

    private void Update()
    {
        var direction = _inputSystem.Player.Move.ReadValue<Vector2>();
        Move(direction);

        if (!_isFacingRight && direction.x > 0f)
        {
            Flip();
        }
        else if (_isFacingRight && direction.x < 0f)
        {
            Flip();
        }
    }

    private void Move(Vector2 direction)
    {
        var moveDirection = new Vector3(direction.x, direction.y);
        _player.position += moveDirection * _moveSpeed * Time.fixedDeltaTime;
        
        if (moveDirection != Vector3.zero)
        {
            _animator.SetBool("IsRun", true);
        }
        else
        {
            _animator.SetBool("IsRun", false);
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        _player.Rotate(0f, 180f, 0f);
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
    }
}
