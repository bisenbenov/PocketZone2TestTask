using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;

    public int Damage
    {
        get { return _damage; }
    }

    private IEnumerator Aim()
    {
        _animator.SetBool("IsAiming", true);
        
        var bullet = Instantiate(_bullet, _firePoint.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.SetForce(_firePoint);

        yield return new WaitForEndOfFrame();
        _animator.SetBool("IsAiming", false);
    }

    public void Shoot()
    {
        StartCoroutine(Aim());
    }
}
