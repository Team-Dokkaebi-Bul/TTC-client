using UnityEngine;

namespace Weapon
{
    public enum ESlotIndex
    {
        FirstSlot = 0, SecondSlot = 2
    };

    public class WeaponSlot : MonoBehaviour
    {
        private Weapon[] _slot = new Weapon[4];
        private ESlotIndex _currentIndex = ESlotIndex.FirstSlot;

        public Weapon EquipMainWeapon(in Weapon weapon)
        {
            Weapon heldWeapon = GetCurrentMainWeapon();

            if (weapon.GetWeaponType() == EWeaponType.Main || weapon.GetWeaponType() == EWeaponType.Both)
            {
                _slot[(int)_currentIndex] = weapon;
            }
            return heldWeapon;
        }

        public Weapon EquipSubWeapon(in Weapon weapon)
        {
            Weapon heldWeapon = GetCurrentSubWeapon();

            if (weapon.GetWeaponType() == EWeaponType.Sub || weapon.GetWeaponType() == EWeaponType.Both)
            {
                _slot[(int)_currentIndex + 1] = weapon;
            }
            return heldWeapon;
        }

        public Weapon UnequipMainWeapon()
        {
            Weapon heldWeapon = _slot[(int)_currentIndex];

            _slot[(int)_currentIndex] = null;
            return heldWeapon;
        }

        public Weapon UnequipSubWeapon()
        {
            Weapon heldWeapon = _slot[(int)_currentIndex + 1];

            _slot[(int)_currentIndex + 1] = null;
            return heldWeapon;
        }

        public void SwapSlot()
        {
            _currentIndex = _currentIndex == ESlotIndex.FirstSlot ? ESlotIndex.SecondSlot : ESlotIndex.FirstSlot;
        }

        public Weapon GetCurrentMainWeapon()
        {
            return _slot[(int)_currentIndex];
        }

        public Weapon GetCurrentSubWeapon()
        {
            return _slot[(int)_currentIndex + 1];
        }

        public int GetCurrentSlotIndex()
        {
            return _currentIndex == ESlotIndex.FirstSlot ? 1 : 2;
        }
    }
}
