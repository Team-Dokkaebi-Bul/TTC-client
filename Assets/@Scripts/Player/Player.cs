using Eu4ng.Utilities;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField, ReadOnly] PlayerData m_PlayerData;
    
    public virtual void Initialize(PlayerData NewPlayerData)
    {
        m_PlayerData = NewPlayerData;
        
        // TODO 스탯 및 버프 설정
        
        // TODO 사용 가능한 스킬 설정
        
        // TODO 무기 장착
    }
}
