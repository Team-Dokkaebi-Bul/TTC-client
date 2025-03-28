using UnityEngine;

public interface IInventoryComponent
{
    void HasItem(Item item);
    
    void AddItem(Item item);
    
    void RemoveItem(Item item);
}
