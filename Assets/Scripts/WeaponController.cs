using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Animator weaponAnimations;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool ADS;
    [SerializeField] private bool reloading;

    public float currentPlayerSpeed;
    private float horizontal;
    private float vertical;

    void Update()
    {
        GatherPlayerInput();
        TriggeringReload();
        HandleBaseLocomotionAnimations();
        HandleSprintingAnimations();
        AimDownSightsAnimations();
    }

    private void GatherPlayerInput()
    {
        // Receive Player Input
        //
        horizontal = Input.GetAxis("Horizontal"); // A & D

        vertical = Input.GetAxis("Vertical"); // W & S

        isSprinting = Input.GetKey(KeyCode.LeftShift); // Left shift sprint check

        ADS = Input.GetKey(KeyCode.Mouse1); // Right Click ADS

        reloading = Input.GetKeyDown(KeyCode.R); // Has player pressed reload.
    }

    private void Recoil()
    {

    }

    #region Handle Weapon Animations

    private void TriggeringReload()
    {
        // Stop all animations other than reload if reloading.
        if (reloading)
        {
            StartCoroutine(Reload());
        }

        if (reloading) return;
    }

    private void HandleBaseLocomotionAnimations()
    {
        // Handle Locomotion Animation.
        //
        float targetSpeed = (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f) ? 1f : 0f;
        currentPlayerSpeed = Mathf.MoveTowards(currentPlayerSpeed, targetSpeed, 5f * Time.deltaTime);
        weaponAnimations.SetFloat("Speed", currentPlayerSpeed);
    }

    private void HandleSprintingAnimations()
    {
        // Handle Sprint Check Animations.
        //
        if (isSprinting && !ADS)
            weaponAnimations.SetBool("Sprinting", true);
        else
            weaponAnimations.SetBool("Sprinting", false);
    }

    private void AimDownSightsAnimations()
    {
        // Handle Aim Down Sights Animations.
        //
        if (ADS)
        {
            weaponAnimations.SetBool("Sprinting", false); weaponAnimations.SetBool("ADS", true);
        }
        else
        {
            weaponAnimations.SetBool("ADS", false);
        }
    }

    private IEnumerator Reload()
    {
        weaponAnimations.SetTrigger("Reload");
        reloading = true;

        yield return new WaitForSeconds(2.40f);

        reloading = false;
    }

    #endregion
}
