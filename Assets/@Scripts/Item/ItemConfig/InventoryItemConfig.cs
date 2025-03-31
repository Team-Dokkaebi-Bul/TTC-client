using System;
using UnityEngine;

[Serializable]
public struct InventoryItemData
{
    int MaxStack;
}

public class InventoryItemConfig : ItemConfig<InventoryItemData>
{
    
}
