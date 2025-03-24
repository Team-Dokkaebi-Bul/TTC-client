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
        [SerializeField] private int weaponID;
        [SerializeField] private string weaponName;
        [SerializeField] private EWeaponType weaponType;
        [SerializeField] private EWeaponProp weaponProp;
        [SerializeField] private GameObject weaponPrefab;

        public int GetWeaponID() => weaponID;
        public string GetWeaponName() => weaponName;
        public EWeaponType GetWeaponType() => weaponType;
        public EWeaponProp GetWeaponProp() => weaponProp;
        public GameObject GetWeaponPrefab() => weaponPrefab;
    }
}
