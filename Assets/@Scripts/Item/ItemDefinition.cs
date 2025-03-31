using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "Scriptable Objects/ItemDefinition")]
public class ItemDefinition : ScriptableObject
{
    public IStat GetStat() => null;
    
    [SerializeField] List<ItemConfig> m_ItemConfigs = new List<ItemConfig>();
    
    protected List<ItemConfig> ItemConfigs => m_ItemConfigs;

    public T GetItemConfig<T>() where T : ItemConfig
    {
        foreach (var itemConfig in ItemConfigs)
        {
            // 유효성 검사
            if (itemConfig == null) continue;
            
            if(itemConfig.GetType().IsSubclassOf(typeof(T))) return (T)itemConfig;
        }

        return null;
    }
}
