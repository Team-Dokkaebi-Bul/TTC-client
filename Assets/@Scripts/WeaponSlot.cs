using UnityEngine;

public enum ESlotIndex
{
    FirstSlot = 0, SecondSlot = 2
};

public class WeaponSlot : MonoBehaviour
{
    private AWeapon[] _slot = new AWeapon[4];
    private ESlotIndex _currentIndex = ESlotIndex.FirstSlot;

    public AWeapon EquipMainWeapon(in AWeapon weapon)
    {
        AWeapon heldWeapon = GetCurrentMainWeapon();

        if (weapon.GetWeaponType() == EWeaponType.Main || weapon.GetWeaponType() == EWeaponType.Both)
        {
            _slot[(int)_currentIndex] = weapon;
        }
        return heldWeapon;
    }

    public AWeapon EquipSubWeapon(in AWeapon weapon)
    {
        AWeapon heldWeapon = GetCurrentSubWeapon();

        if (weapon.GetWeaponType() == EWeaponType.Sub || weapon.GetWeaponType() == EWeaponType.Both)
        {
            _slot[(int)_currentIndex + 1] = weapon;
        }
        return heldWeapon;
    }

    public AWeapon UnequipMainWeapon()
    {
        AWeapon heldWeapon = _slot[(int)_currentIndex];

        _slot[(int)_currentIndex] = null;
        return heldWeapon;
    }

    public AWeapon UnequipSubWeapon()
    {
        AWeapon heldWeapon = _slot[(int)_currentIndex + 1];

        _slot[(int)_currentIndex + 1] = null;
        return heldWeapon;
    }

    public void SwapSlot()
    {
        _currentIndex = _currentIndex == ESlotIndex.FirstSlot ? ESlotIndex.SecondSlot : ESlotIndex.FirstSlot;
    }

    public AWeapon GetCurrentMainWeapon()
    {
        return _slot[(int)_currentIndex];
    }

    public AWeapon GetCurrentSubWeapon()
    {
        return _slot[(int)_currentIndex + 1];
    }
}
