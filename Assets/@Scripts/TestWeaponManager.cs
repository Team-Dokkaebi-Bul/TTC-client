using UnityEngine;
using UnityEngine.UI;

public class TestWeaponManager : MonoBehaviour
{
    public Button mainEquipButton;
    public Button subEquipButton;
    public Button mainUnequipButton;
    public Button subUnequipButton;

    public AWeapon weapon;
    public WeaponSlot slot;

    private void Start()
    {
        mainEquipButton.onClick.AddListener(EquipMainWeaponFromButton);
        subEquipButton.onClick.AddListener(EquipSubWeaponFromButton);
        mainUnequipButton.onClick.AddListener(UnequipMainWeponFromButton);
        subUnequipButton.onClick.AddListener(UnequipSubWeaponFromButton);
    }

    private void EquipMainWeaponFromButton()
    {
        Debug.Log($"Try Equip main weapon : {weapon.GetWeaponID()}");
        slot.EquipMainWeapon(weapon);
        Debug.Log($"Current main weapon : {slot?.GetCurrentMainWeapon().GetWeaponID()}");
    }

    private void EquipSubWeaponFromButton()
    {
        Debug.Log($"Try Equip sub weapon : {weapon.GetWeaponID()}");
        slot.EquipSubWeapon(weapon);
        Debug.Log($"Current sub weapon : {slot?.GetCurrentSubWeapon().GetWeaponID()}");
    }

    private void UnequipMainWeponFromButton()
    {
        Debug.Log($"Try unequip main weapon");
        var heldWeapon = slot.UnequipMainWeapon();
        if (heldWeapon)
        {
            Debug.Log($"Held main weapon : {heldWeapon.GetWeaponID()}");
        }
        else
        {
            Debug.Log("No held");
        }
        Debug.Log($"Current main weapon : {slot.GetCurrentMainWeapon().GetWeaponID()}");
    }

    private void UnequipSubWeaponFromButton()
    {
        Debug.Log($"Try unequip sub weapon");
        var heldWeapon = slot.UnequipSubWeapon();
        if (heldWeapon)
        {
            Debug.Log($"Held sub weapon : {heldWeapon.GetWeaponID()}");
        }
        else
        {
            Debug.Log("No held");
        }
        Debug.Log($"Current main weapon : {slot.GetCurrentSubWeapon().GetWeaponID()}");
    }
}
