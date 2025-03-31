using Juhyeon.Weapon.System;

public interface IPlayer : IStatComponent, IInventoryComponent, IEquipmentComponent
{
    void Initialize(PlayerData NewPlayerData);
}
