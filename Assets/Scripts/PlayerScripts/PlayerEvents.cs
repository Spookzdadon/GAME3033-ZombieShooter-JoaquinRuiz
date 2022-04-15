using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public delegate void OnWeaponEquippedEvent(WeaponComponent weaponComponent);

    public static event OnWeaponEquippedEvent OnWeaponEquipeed;
    
    public static void InvokeOnWEaponEquipped(WeaponComponent weaponComponent)
    {
        OnWeaponEquipeed?.Invoke(weaponComponent);
    }

    public delegate void OnHealthInitializedEvent(HealthComponent healthComponent);

    public static event OnHealthInitializedEvent OnHealthInitialized;

    public static void InvokeOnHealthInitialized(HealthComponent healthComponent)
    {
        OnHealthInitialized?.Invoke(healthComponent);
    }

}
