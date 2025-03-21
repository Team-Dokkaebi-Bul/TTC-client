using UnityEngine;
using Weapon;

namespace Weapon
{
    public class WeaponManager : MonoBehaviour
    {
        public WeaponDB DB;

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
