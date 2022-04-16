using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn")]
    [SerializeField]
    public GameObject weaponToSpawn;

    public PlayerController playerController;
    Animator animator;

    GameObject spawnedWeapon;
    public WeaponAmmoUI weaponAmmoUI;

    public Sprite crossHairImage;
    public WeaponComponent equippedWeapon;

    [SerializeField]
    public GameObject weaponSocketLocation;
    [SerializeField]
    public Transform gripIKSocketLocation;

    bool firingPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        //spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);

        //equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        //equippedWeapon.Initialize(this);
        //PlayerEvents.InvokeOnWEaponEquipped(equippedWeapon);
        //gripIKSocketLocation = equippedWeapon.gripLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (equippedWeapon)
        {
            if (!playerController.isReloading)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
            }
        }
    }



    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;
        if (!equippedWeapon) return;
        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        }
        animator.SetBool("IsFiring", true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    void StopFiring()
    {
        animator.SetBool("IsFiring", false);
        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        if (!equippedWeapon) return;
        StartReloading();
    }

    public void StartReloading()
    {
        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0)
        {
            return;
        }

        // Refill ammo here
        equippedWeapon.StartReloading();


        playerController.isReloading = true;
        animator.SetBool("IsReloading", true);
        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (animator.GetBool("IsReloading"))
        {
            return;
        }

        playerController.isReloading = false;
        animator.SetBool("IsReloading", false);
        equippedWeapon.StopReloading();
        CancelInvoke(nameof(StopReloading));

        if(firingPressed)
        {
            StartFiring();
        }

    }

    public void EquipWeapon(WeaponScriptable weaponScriptable)
    {
        if (!weaponScriptable) return;

        spawnedWeapon = Instantiate(weaponScriptable.itemPrefab, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
        if (!spawnedWeapon) return;

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        //if (!equippedWeapon) return;
        equippedWeapon.Initialize(this, weaponScriptable);
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);

        gripIKSocketLocation = equippedWeapon.gripLocation;

        weaponAmmoUI.OnWeaponEquipped(equippedWeapon);
    }

    public void UnEquipWeapon(WeaponScriptable weapon)
    {
        if (!equippedWeapon) return;

        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
    }
}
