using UnityEngine;

public class PoisonPalm : MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage
    {
        set { _damage = value; }
        get { return _damage; }
    }
}
