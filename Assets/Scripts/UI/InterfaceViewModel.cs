using System;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceViewModel : MonoBehaviour
{
    [SerializeField] private Button _buttonShot;
    [SerializeField] private Button _buttonBackpack;

    public event EventHandler onShoot;
    public event EventHandler onBackpack;

    private void OnEnable()
    {
        _buttonShot.onClick.AddListener(OnShoot);
        _buttonBackpack.onClick.AddListener(OnBackpack);
    }

    private void OnShoot()
    {
        onShoot?.Invoke(this, EventArgs.Empty);
    }

    private void OnBackpack()
    {
        onBackpack?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        _buttonShot.onClick.RemoveListener(OnShoot);
        _buttonBackpack.onClick.RemoveListener(OnBackpack);
    }
}
