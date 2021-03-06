using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 2)]
public class WeaponScriptable : EquippableScriptable
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController playerController)
    {
        if (Equipped)
        {
            // Unequip from inventory
            // Remove from controller
            playerController.weaponHolder.UnEquipWeapon();
        }
        else
        {
            // Invoke OnWeaponEquipped from PlayerEvents
            // Equip weapon from weapon holder on PlayerController
            playerController.weaponHolder.EquipWeapon(this);
        }

        base.UseItem(playerController);
    }
}
