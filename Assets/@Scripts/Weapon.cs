using UnityEngine;

namespace Weapon
{

    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private EWeaponType _type;
        [SerializeField] private EWeaponProp _prop;

        public void Initialize(WeaponData data)
        {
            _id = data.weaponID;
            _type = data.weaponType;
            _prop = data.weaponProp;
        }

        public int GetWeaponID() => _id;
        public EWeaponType GetWeaponType() => _type;
        public EWeaponProp GetWeaponProp() => _prop;
    }
}
