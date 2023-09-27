using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private UIInventoryItem _itemPrefab;
    [SerializeField] private RectTransform _contentPanel;

    private List<UIInventoryItem> _items = new();

    public event EventHandler<int> OnItemIconClick;

    public void InitializeInventory(int inventorySize)
    {
       for (int i = 0; i < inventorySize; i++)
       {
           var item = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
           item.transform.SetParent(_contentPanel);
           _items.Add(item);
           item.OnItemClicked += OnItemClick;
       }
    }

    private void OnItemClick(object sender, UIInventoryItem inventoryItem)
    {
        int index = _items.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }
        OnItemIconClick?.Invoke(this, index);
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (_items.Count > itemIndex)
        {
            _items[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    public void ResetAllItems()
    {
        foreach (var item in _items)
        {
            item.ResetData();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
