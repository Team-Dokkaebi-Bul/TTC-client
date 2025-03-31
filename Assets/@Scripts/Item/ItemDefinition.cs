using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "Scriptable Objects/ItemDefinition")]
public class ItemDefinition : ScriptableObject
{
    public IStat GetStat() => null;

    [SerializeField] int m_ID;
    [SerializeField] string m_DisplayName;
    [SerializeField] List<ItemConfig> m_ItemConfigs = new List<ItemConfig>();
    
    protected List<ItemConfig> ItemConfigs => m_ItemConfigs;
    
    public int ID => m_ID;
    public string DisplayName => m_DisplayName;

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
