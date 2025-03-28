using UnityEngine;

public class DropItem : MonoBehaviour, IInteractable
{
    [SerializeField] Item m_Item;
    
    protected Item GetItem() => m_Item;
    
    /* IInteractable */

    public void Interact(MonoBehaviour target)
    {
        IInventoryComponent inventoryInterface = target.GetComponent<IInventoryComponent>();
        if (inventoryInterface == null) return;
        
        inventoryInterface.AddItem(GetItem());
        
        Destroy(gameObject);
    }
}
