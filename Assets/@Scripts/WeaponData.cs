using UnityEngine;

namespace Weapon
{
    public enum EWeaponType
    {
        Both, Main, Sub
    };

    public enum EWeaponProp
    {
        Normal
    };

    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public int weaponID;
        public string weaponName;
        public EWeaponType weaponType;
        public EWeaponProp weaponProp;
    }
}
