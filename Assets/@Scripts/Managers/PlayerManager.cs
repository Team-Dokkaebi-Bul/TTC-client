using System;
using Eu4ng.Framework.Platformer;
using Eu4ng.Manager.Data;

[Serializable]
public class PlayerData : ISavable
{
    // TODO 로비에서 장착된 무기, 스킬, 버프 등
}

public class PlayerManager : DataManagerClient<PlayerManager, PlayerData>
{
    /* Fields */
    
    IPlayer m_PlayerInterface;
    
    /* Properties */
    
    IPlayer PlayerInterface => m_PlayerInterface;
    
    protected override void OnInitialize()
    {
        base.OnInitialize();
        
        PlayerSpawnManager.Instance.PlayerSpawned += OnPlayerSpawned;
    }

    void OnPlayerSpawned()
    {
        m_PlayerInterface = PlayerSpawnManager.Instance.Player?.GetComponent<IPlayer>();
        PlayerInterface.Initialize(Data);
    }
}
