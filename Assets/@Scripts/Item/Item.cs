using UnityEngine;

public struct Item
{
    public ItemDefinition Definition;

    [Min(0)]
    public int Quantity;

    public bool IsValid => Definition != null && Quantity > 0;
    public bool IsNotValid => !IsValid;
}
