using System;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [SerializeField] private bool _isStackable;
    public bool IsStackable { get { return _isStackable; } set { _isStackable = value; } }

    public int ID => GetInstanceID();

    [SerializeField] private int _maxStackSize = 1;
    public int MaxStackSize { get { return _maxStackSize; } set { _maxStackSize = value; } }

    [SerializeField] private Sprite _itemImage;
    public Sprite ItemImage { get { return _itemImage; } set { _itemImage = value; } }

    [SerializeField] private string _name;
    public string Name { get { return _name; } set { _name = value; } }
}
