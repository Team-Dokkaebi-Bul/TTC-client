using UnityEngine;
using UnityEngine.UI;

public class TestWeaponManager : MonoBehaviour
{
    public Button mainEquipButton;
    public Button subEquipButton;
    public Button mainUnequipButton;
    public Button subUnequipButton;
    public Text text;

    public AWeapon weapon;
    public WeaponSlot slot;

    private void Start()
    {
        mainEquipButton.onClick.AddListener(EquipMainWeaponFromButton);
        subEquipButton.onClick.AddListener(EquipSubWeaponFromButton);
        mainUnequipButton.onClick.AddListener(UnequipMainWeponFromButton);
        subUnequipButton.onClick.AddListener(UnequipSubWeaponFromButton);
    }

    private void Update()
    {
        var mainWeapon = slot.GetCurrentMainWeapon();
        var subWeapon = slot.GetCurrentSubWeapon();
        int mainId = 0, subId = 0;

        if (mainWeapon)
        {
            mainId = mainWeapon.GetWeaponID();
        }
        if (subWeapon)
        {
            subId = subWeapon.GetWeaponID();
        }
        string message = $"Main : {mainId}\nSub : {subId}";
        text.text = message;
    }

    private void EquipMainWeaponFromButton()
    {
        Debug.Log($"Try Equip main weapon : {weapon.GetWeaponID()}");
        slot.EquipMainWeapon(weapon);

    }

    private void EquipSubWeaponFromButton()
    {
        Debug.Log($"Try Equip sub weapon : {weapon.GetWeaponID()}");
        slot.EquipSubWeapon(weapon);
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
            Debug.Log("Unequiped");
        }
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
            Debug.Log("Unequiped");
        }
    }
}
