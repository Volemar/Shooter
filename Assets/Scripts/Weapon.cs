using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 10;
    [SerializeField] ParticleSystem shootVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 1f;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;

    private void OnEnable()
    {
        StartCoroutine(CoolWeapon());
    }

    private IEnumerator CoolWeapon()
    {
        if (canShoot)
        {
            yield break;
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        ammoText.text = ammoSlot.GetCurrentAmmo(ammoType).ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ProccessEffects();
            ProccessRaycasting();
            ammoSlot.ReduceAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void ProccessEffects()
    {
        shootVFX.Play();
    }

    private void ProccessRaycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        float time = effect.GetComponentInChildren<ParticleSystem>().main.duration;
        Destroy(effect, time);
    }
}
