using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponDB", menuName = "Scriptable Objects/WeaponDB")]
    public class WeaponDB : ScriptableObject
    {
        public List<WeaponData> weapons;
        private Dictionary<int, WeaponData> _db;

        private void OnEnable()
        {
            _db = new Dictionary<int, WeaponData>();
            foreach(var weapon in weapons)
            {
                if (_db.ContainsKey(weapon.weaponID))
                {
                    Debug.Log($"이미 존재하는 ID입니다 : {weapon.weaponID} - {weapon.weaponName}");
                    return;
                }
                _db.Add(weapon.weaponID, weapon);
            }
        }

        public WeaponData GetWeaponData(in int id)
        {
            if (_db.TryGetValue(id, out var weapon))
            {
                return weapon;
            }
            Debug.Log($"무기 {id} 는 등록되어 있지 않습니다.");
            return null;
        }
    }
}
