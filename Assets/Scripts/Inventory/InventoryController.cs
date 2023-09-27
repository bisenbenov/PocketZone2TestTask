using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryUI _inventoryUI;

    [SerializeField] private InventorySO _inventoryData;

    [SerializeField] private List<InventoryItem> _initialItems = new List<InventoryItem>();

    private void Start()
    {
        _inventoryUI.InitializeInventory(_inventoryData.Size);
        _inventoryData.Initialize();
        
        _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        _inventoryUI.OnItemIconClick += RemoveItemFromInventory;

        foreach(var item in _initialItems)
        {
            if (item.IsEmpty) continue;
            _inventoryData.AddItem(item);
        }
    }

    private void RemoveItemFromInventory(object sender, int itemIndex)
    {
        var inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        _inventoryData.RemoveItem(itemIndex, 1);
    }

    private void Update()
    {    
        if (_inventoryUI.isActiveAndEnabled == false) 
        {
            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
    }

    private void UpdateInventoryUI(object sender, Dictionary<int, InventoryItem> inventoryState)
    {
        _inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage,
                item.Value.quantity);
        }
    }
}
