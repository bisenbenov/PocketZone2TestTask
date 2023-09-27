using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private InventorySO _inventoryData;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.transform.TryGetComponent<Item>(out var item))
        {
            var reminder = _inventoryData.AddItem(item.InventoryItem, item.Quantity);

            if (reminder == 0)
            {
                item.DestroyItem();
            }
            else
            {
                Debug.Log(reminder);
                item.Quantity = reminder;
            }
        }
    }
}
