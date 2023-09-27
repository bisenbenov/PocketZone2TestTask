using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _startSpeed = 15f;
    [SerializeField] private int _damage;
    
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }
    
    public int Damage
    {
        set { _damage = value; }
        get { return _damage; }
    }

    public void SetForce(Transform force)
    {
        _rigidBody.velocity = force.right * _startSpeed;
    }
}
