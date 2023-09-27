using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public List<InventoryItemData> inventoryItems = new List<InventoryItemData>();
}

[Serializable]
public class InventoryItemData
{
    public int itemIndex;
    public int quantity;
    public ItemSOData itemData;
}

[Serializable]
public class ItemSOData
{
    public int instanceID;
    public int maxStackSize;
    public string name;
}
