using UnityEngine;

namespace Weapon
{

    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private EWeaponType _type;
        [SerializeField] private EWeaponProp _prop;

        public int GetWeaponID()
        {
            return _id;
        }

        public EWeaponType GetWeaponType()
        {
            return _type;
        }

        public EWeaponProp GetWeaponProp()
        {
            return _prop;
        }
    }
}
