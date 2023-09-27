using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItem> _inventoryItems;
    [SerializeField] private int _size = 10;

    public int Size { get { return _size; } private set { } }
    public event EventHandler<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void Initialize()
    {
       _inventoryItems = new List<InventoryItem>();
       for (int i = 0; i < _size; i++)
       {
           _inventoryItems.Add(InventoryItem.GetEmptyItem());
       }
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue =
            new Dictionary<int, InventoryItem>();

        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = _inventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return _inventoryItems[itemIndex];
    }

    public void AddItem(InventoryItem item)
    {
        AddItem(item.item, item.quantity);
    }

    public int AddItem(ItemSO item, int quantity)
    {
        if (item.IsStackable == false)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                while (quantity > 0 && IsInventoryFull() == false)
                {
                    quantity -= AddItemToFirstFreeSlot(item, 1);
                }
                InformAboutChange();
                return quantity;
            }
        }
        quantity = AddStackableItem(item, quantity);
        
        InformAboutChange();
        return quantity;
    }

    private int AddStackableItem(ItemSO item, int quantity)
    {
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].IsEmpty)
                continue;
            if (_inventoryItems[i].item.ID == item.ID)
            {
                int amountPossibleToTake =
                    _inventoryItems[i].item.MaxStackSize - _inventoryItems[i].quantity;
                
                if (quantity > amountPossibleToTake)
                {
                    _inventoryItems[i] = _inventoryItems[i]
                        .ChangeQuantity(_inventoryItems[i].item.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else
                {
                  _inventoryItems[i] = _inventoryItems[i]
                      .ChangeQuantity(_inventoryItems[i].quantity + quantity);
                  InformAboutChange();
                  return 0;
                }
            }
        }
        while (quantity > 0 && IsInventoryFull() == false)
        {
            int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity);
        }

        return quantity;
    }

    public void RemoveItem(int itemIndex, int amount)
    {
        if (_inventoryItems.Count > itemIndex)
        {
            if (_inventoryItems[itemIndex].IsEmpty)
                return;
            int reminder = _inventoryItems[itemIndex].quantity - amount;
            if (reminder <= 0)
                _inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
            else
                _inventoryItems[itemIndex] = _inventoryItems[itemIndex]
                    .ChangeQuantity(reminder);

            InformAboutChange();
        }
    }

    private bool IsInventoryFull()
            => _inventoryItems.Where(item => item.IsEmpty).Any() == false;

    private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            quantity = quantity
        };

        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].IsEmpty)
            {
                _inventoryItems[i] = newItem;
                return quantity;
            }
        }

        return 0;
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(this, GetCurrentInventoryState());
    }
}

[Serializable]
public struct InventoryItem
{
    public int quantity;
    public ItemSO item;
    public bool IsEmpty => item == null;

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuantity 
        };
    }

    public static InventoryItem GetEmptyItem() => new InventoryItem
    {
        item = null,
        quantity = 0
    };
}