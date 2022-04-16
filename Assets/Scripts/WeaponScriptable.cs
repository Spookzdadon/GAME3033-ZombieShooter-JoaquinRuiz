using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "weapon", menuName = "Items/Weapon", order = 2)]
public class WeaponScriptable : EquippableScriptable
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController playerController)
    {
        if (equipped)
        {
            playerController.weaponHolder.UnEquipWeapon(this);
            //unequip from inventory here
            //remove from controller here too
        }
        else
        {
            //invoke OnWeaponEquipped from player here for inventory
            //equip weapon from weapon holder on playercontroller
            playerController.weaponHolder.EquipWeapon(this);
            //PlayerEvents.InvokeOnWeaponEquipped(itemPrefab.GetComponent<WeaponComponent>());
        }

        base.UseItem(playerController);
    }
}
