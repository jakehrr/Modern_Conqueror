using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    [Header("Weapon Settings")]
    public float fireRate = 0.1f; // SCAR full-auto (~600 RPM)
    public int maxAmmo = 25;
    public float reloadDuration = 2.4f;

    [Header("References")]
    public Transform firePoint;
    public GameObject bulletImpactPrefab;
    public Animator weaponAnimation;

    private float lastFireTime;
    private int currentAmmo;
    private bool isReloading;

    private RecoilHandler recoil;

    void Start()
    {
        recoil = Camera.main.transform.parent.GetComponent<RecoilHandler>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading) return;

        // Reload manually
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        // Auto fire when holding LMB
        if (Input.GetButton("Fire1") && Time.time > lastFireTime + fireRate)
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
    }

    void Shoot()
    {
        lastFireTime = Time.time;
        currentAmmo--;

        // Raycast to simulate hit
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, 100f))
        {
            if (bulletImpactPrefab != null)
                Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }

        // Recoil
        recoil?.ApplyRecoil();

        // TODO: Play shoot animation, SFX, muzzle flash
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        weaponAnimation.SetTrigger("Reload");

        // TODO: Play reload animation, SFX
        yield return new WaitForSeconds(reloadDuration);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
