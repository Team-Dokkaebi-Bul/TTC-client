using System.Collections.Generic;
using Eu4ng.System.Item;
using UnityEngine;

/// <summary>
/// TTC 프로젝트 전용 데이터 테이블 데이터 클래스
/// </summary>
public class TTCItemDataTableRow : ItemDataTableRow
{
    
}

/// <summary>
/// TTC 프로젝트 전용 아이템 데이터베이스
/// </summary>
[CreateAssetMenu(fileName = "TTCItemDatabase", menuName = "Scriptable Objects/Item/Database")]
public class TTCItemDatabase : ItemDatabase<TTCItemDataTableRow>
{
#if UNITY_EDITOR
    [ContextMenu("HardUpdate")]
    protected override void HardUpdate() => base.HardUpdate();

    [ContextMenu("SoftUpdate")]
    protected override void SoftUpdate() => base.SoftUpdate();

    protected override void OnItemDefinitionCreated(int id, TTCItemDataTableRow dataTableRow, ItemDefinition itemDefinition,
        List<ItemConfig> itemConfigs)
    {
        // Assets/Samples/GameFramework/VERSION/InventorySystem Sample/Scripts/SampleItemDatabase.cs 참고
        
        RegisterInventoryItemConfig(id, dataTableRow, itemDefinition, itemConfigs);
        RegisterEquipmentItemConfig(id, dataTableRow, itemDefinition, itemConfigs);
        RegisterStatItemConfig(id, dataTableRow, itemDefinition, itemConfigs);
    }

    protected void RegisterInventoryItemConfig(int id, TTCItemDataTableRow dataTableRow, ItemDefinition itemDefinition,
        List<ItemConfig> itemConfigs)
    {
        // TODO
    }
    
    protected void RegisterEquipmentItemConfig(int id, TTCItemDataTableRow dataTableRow, ItemDefinition itemDefinition,
        List<ItemConfig> itemConfigs)
    {
        // TODO
    }
    
    protected void RegisterStatItemConfig(int id, TTCItemDataTableRow dataTableRow, ItemDefinition itemDefinition,
        List<ItemConfig> itemConfigs)
    {
        // TODO
    }
#endif
}
