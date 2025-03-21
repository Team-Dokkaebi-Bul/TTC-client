using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace Weapon
{
    public class WeaponManager : MonoBehaviour
    {
        public WeaponDB DB;
        private WeaponSlot _slot;

        #region Test
        public Button mainEquip;
        public Button subEquip;
        public Button mainUnequip;
        public Button subUnequip;
        public Button swap;
        public Text text;

        public Weapon weapon;

        private void Start()
        {
            mainEquip.onClick.AddListener(OnEquipMain);
            subEquip.onClick.AddListener(OnEquipSub);
            mainUnequip.onClick.AddListener(OnUnequipMain);
            subUnequip.onClick.AddListener(OnUnequipSub);
            swap.onClick.AddListener(OnSwap);
            _slot = GetComponent<WeaponSlot>();
        }

        private void Update()
        {
            var main = _slot.GetCurrentMainWeapon();
            var sub = _slot.GetCurrentSubWeapon();
            int mainId = 0, subId = 0;

            if (main)
                mainId = main.GetWeaponID();
            if (sub)
                subId = sub.GetWeaponID();
            text.text = $"{_slot.GetCurrentSlotIndex()}\nSlot : Main : {mainId}\nSub : {subId}";
        }

        public void OnEquipMain()
        {
            Debug.Log($"Equip main weapon {weapon.GetWeaponID()}");
            _slot.EquipMainWeapon(weapon);
        }

        public void OnEquipSub()
        {
            Debug.Log($"Equip sub weapon {weapon.GetWeaponID()}");
            _slot.EquipSubWeapon(weapon);
        }

        public void OnUnequipMain()
        {
            var weapon = _slot.UnequipMainWeapon();
            if (weapon)
                Debug.Log($"Unequip main weapon {weapon.GetWeaponID()}");
        }

        public void OnUnequipSub()
        {
            var weapon = _slot.UnequipSubWeapon();
            if (weapon)
                Debug.Log($"Unequip main weapon {weapon.GetWeaponID()}");
        }
        public void OnSwap()
        {
            _slot.SwapSlot();
        }
        #endregion

        public Weapon CreateWeapon(int id, Transform parent = null)
        {
            WeaponData data = DB.GetWeaponData(id);

            if (data == null)
            {
                return null;
            }
            GameObject newWeapon = Instantiate(data.weaponPrefab, parent);
            Weapon weapon = newWeapon.GetComponent<Weapon>();
            if (weapon == null)
            {
                weapon = newWeapon.AddComponent<Weapon>();
            }
            weapon.Initialize(data);
            return weapon;
        }
    }
}
