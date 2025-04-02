using System.Collections.Generic;
using Eu4ng.System.Item;
using UnityEngine;

namespace Eu4ng.System.Inventory.Sample
{
    public class SampleItemDataTableRow : ItemDataTableRow
    {
        public int MaxStack;
    }

#if USE_E4_INVENTORY_SYSTEM
    [CreateAssetMenu(fileName = "SampleItemDatabase", menuName = "Scriptable Objects/Item/Database")]
#endif
    public class SampleItemDatabase : ItemDatabase<SampleItemDataTableRow>
    {
#if UNITY_EDITOR
        [ContextMenu("HardUpdate")]
        protected override void HardUpdate() => base.HardUpdate();

        [ContextMenu("SoftUpdate")]
        protected override void SoftUpdate() => base.SoftUpdate();

        protected override void OnItemDefinitionCreated(int id, SampleItemDataTableRow dataTableRow, ItemDefinition itemDefinition,
            List<ItemConfig> itemConfigs)
        {
            var itemConfig = CreateInstance<ItemConfig_Inventory>();
            itemConfig.name = nameof(ItemConfig_Inventory) + "_" + id;
            InventoryItemConfigData data = new InventoryItemConfigData
            {
                MaxStack = dataTableRow.MaxStack
            };
            itemConfig.Initialize(data);
            itemConfigs.Add(itemConfig);
        }
#endif
    }
}
