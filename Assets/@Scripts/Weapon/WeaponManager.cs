using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace Weapon
{
    public class WeaponManager : MonoBehaviour
    {
        public WeaponDB DB;

        #region Test
        public Button mainEquip;
        public Button subEquip;
        public Button mainUnequip;
        public Button subUnequip;
        public Button swap;
        public Button generate;
        public Text text;

        public WeaponSlot slot;
        public Weapon weapon;

        private void Start()
        {
            mainEquip.onClick.AddListener(OnEquipMain);
            subEquip.onClick.AddListener(OnEquipSub);
            mainUnequip.onClick.AddListener(OnUnequipMain);
            subUnequip.onClick.AddListener(OnUnequipSub);
            swap.onClick.AddListener(slot.SwapSlot);
            generate.onClick.AddListener(OnGenerate);
        }

        private void Update()
        {
            var main = slot.GetCurrentMainWeapon();
            var sub = slot.GetCurrentSubWeapon();
            int mainId = 0, subId = 0;

            if (main)
                mainId = main.GetWeaponID();
            if (sub)
                subId = sub.GetWeaponID();
            text.text = $"{slot.GetCurrentSlotIndex()}\nSlot : Main : {mainId}\nSub : {subId}";
        }

        public void OnEquipMain()
        {
            Debug.Log($"Equip main weapon {weapon.GetWeaponID()}");
            slot.EquipMainWeapon(weapon);
        }

        public void OnEquipSub()
        {
            Debug.Log($"Equip sub weapon {weapon.GetWeaponID()}");
            slot.EquipSubWeapon(weapon);
        }

        public void OnUnequipMain()
        {
            var weapon = slot.UnequipMainWeapon();
            if (weapon)
                Debug.Log($"Unequip main weapon {weapon.GetWeaponID()}");
        }

        public void OnUnequipSub()
        {
            var weapon = slot.UnequipSubWeapon();
            if (weapon)
                Debug.Log($"Unequip main weapon {weapon.GetWeaponID()}");
        }

        public void OnGenerate()
        {
            int id = Random.Range(1, 4);
            Weapon weapon = CreateWeapon(id);
            Debug.Log($"Generate weapon {weapon.GetWeaponID()}");
            
        }
        #endregion

        public Weapon CreateWeapon(int id, Transform parent = null)
        {
            WeaponData data = DB.GetWeaponData(id);

            if (data == null)
            {
                return null;
            }
            GameObject newWeapon = Instantiate(data.GetWeaponPrefab(), parent);
            Weapon weapon = newWeapon.GetComponent<Weapon>();
            if (weapon == null)
            {
                weapon = newWeapon.AddComponent<Weapon>();
            }
            weapon.SetData(data);
            return weapon;
        }
    }
}
