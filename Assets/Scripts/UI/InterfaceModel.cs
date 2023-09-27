using System;
using UnityEngine;

public class InterfaceModel : MonoBehaviour
{
    [SerializeField] private InterfaceViewModel _shootViewModel;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private InventoryUI _backPack;

    private void OnEnable()
    {
        _shootViewModel.onShoot += Shoot;
        _shootViewModel.onBackpack += OpenBackpack;
    }

    private void Shoot(object sender, EventArgs e)
    {
        if (_weapon)
        {
            _weapon.Shoot();
        }
    }

    private void OpenBackpack(object sender, EventArgs e)
    {
        if (_backPack.isActiveAndEnabled)
        {
            _backPack.Hide();
        }
        else
        {
            _backPack.Show();   
        }
    }

    private void OnDisable()
    {
        _shootViewModel.onShoot -= Shoot;
        _shootViewModel.onBackpack -= OpenBackpack;
    }
}
