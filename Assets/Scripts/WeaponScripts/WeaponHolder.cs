using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    private GameObject weaponToSpawn;
    [SerializeField]
    private Sprite crossHairImage;
    [SerializeField]
    private Transform weaponSocketLocation;
    [SerializeField]
    private Transform gripIKSocketLocation;

    private PlayerController playerController;
    public PlayerController PlayerController { get { return playerController; } }
    private Animator playerAnimator;
    private WeaponComponent equippedWeapon;

    private bool firingPressed;

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingingHash = Animator.StringToHash("isReloading");

    // Start is called before the first frame update
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation);

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialize(this);
        gripIKSocketLocation = equippedWeapon.GripLocation;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!playerController.isReloading)
        {
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.position);
        }
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        playerAnimator.SetTrigger(isReloadingingHash);
    }

    public void OnAim(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    }

    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;
        //playerController.isFiring = value.isPressed;
        //playerAnimator.SetBool(isFiringHash, value.isPressed);
        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            Stopfiring();
        }
    }

    public void StartReloading()
    {

    }

    private void StartFiring()
    {
        if (equippedWeapon.WeaponStats.bulletsInClip <= 0) return;

        playerAnimator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    private void Stopfiring()
    {
        equippedWeapon.StopFiringWeapon();
        playerAnimator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
    }
}
