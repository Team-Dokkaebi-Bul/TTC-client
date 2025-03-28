using UnityEngine;

public struct Item
{
    public ItemDefinition Definition;

    [Min(0)]
    public int Quantity;
}
