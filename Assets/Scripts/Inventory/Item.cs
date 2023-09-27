using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO _inventoryItem;
    public ItemSO InventoryItem { get { return _inventoryItem; } set { _inventoryItem = value; } }

    [SerializeField] private int _quantity = 1;
    public int Quantity { get { return _quantity; } set {  _quantity = value;  } }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _inventoryItem.ItemImage;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }
}
