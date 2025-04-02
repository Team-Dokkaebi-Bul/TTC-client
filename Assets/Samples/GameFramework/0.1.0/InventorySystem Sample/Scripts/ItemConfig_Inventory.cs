using System;
using Eu4ng.System.Item;
using UnityEngine;

namespace Eu4ng.System.Inventory.Sample
{
    public interface IInventoryItemConfig : Eu4ng.System.Inventory.IInventoryItemConfig
    {
        public int TestValue { get; }
    }

    [Serializable]
    public struct InventoryItemConfigData : IInventoryItemConfig
    {
        [field: SerializeField, Min(0)]
        public int MaxStack { get; set; }

        [field: SerializeField]
        public int TestValue { get; set; }
    }

#if USE_E4_INVENTORY_SYSTEM
    [CreateAssetMenu(fileName = "ItemConfig_Inventory", menuName = "Scriptable Objects/ItemConfig/InventorySample")]
#endif
    public class ItemConfig_Inventory : ItemConfig<InventoryItemConfigData>
    {

    }
}
