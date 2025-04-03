using Juhyeon.Weapon.System;
using Noong2.StatSystem;

public interface IPlayer : IStatComponent, IInventoryComponent, IEquipmentComponent
{
    void Initialize(PlayerData NewPlayerData);
}
