using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn")]
    [SerializeField]
    public GameObject weaponToSpawn;

    PlayerController playerController;
    Animator animator;

    public Sprite crossHairImage;

    [SerializeField]
    public GameObject weaponSocketLocation;
    [SerializeField]
    public Transform gripIKSocketLocation;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        animator.SetBool("IsReloading", playerController.isReloading);

    }

    public void OnFire(InputValue value)
    {
        playerController.isFiring = value.isPressed;
        animator.SetBool("IsFiring", playerController.isFiring);
        //set up firing animation
    }
}
