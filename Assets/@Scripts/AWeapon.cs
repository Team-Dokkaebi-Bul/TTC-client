using UnityEngine;

public enum EWeaponType
{
    Main, Sub
};

public enum EWeaponProp
{
    Normal
};

public class AWeapon
{
    private int _id;
    private EWeaponType _type;
    private EWeaponProp _prop;

    public AWeapon(in int id, in EWeaponType type, in EWeaponProp prop)
    {
        _id = id;
        _type = type;
        _prop = prop;
    }

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
