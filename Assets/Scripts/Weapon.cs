using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Weapon : MonoBehaviour
{
    public GunDataSO gunData;
    public LayerMask damageableLayer;
    public Transform muzzlePosition;

    public int bulletInMagazine;
    public int totalBulletsLeft = 100;

    private float timer = 0;
    private bool isReloading = false;
    public bool shootingDisabled = false;

    private Camera cam;
    private Vector2 screenCenter;

    private Animator playerAnimator;
    private AudioSource audioSource;

    public Rig rig;


    private void Start()
    {
        bulletInMagazine = gunData.magazineSize;
        cam = Camera.main;
        playerAnimator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void Shoot()
    {
        if(CanShoot())
        {
            timer = 0;
            bulletInMagazine--;
            Instantiate(gunData.muzzleFlash, muzzlePosition);
            audioSource.PlayOneShot(gunData.shootSound);

            Ray ray = cam.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, 40, damageableLayer))
            {
                IDamageable enemyHealth = hit.transform.GetComponent<IDamageable>();
                enemyHealth.TakeDamage(gunData.damage);
            }
        }
    }

    bool CanShoot()
    {
        if(timer < gunData.fireRate) return false;
        if (isReloading) return false;
        if (shootingDisabled) return false;

        if (bulletInMagazine < 1)
        {
            StartCoroutine(ReloadGun());
            return false;
        }

        return true;
    }

    public IEnumerator ReloadGun()
    {
        if (isReloading) yield break;
        if (bulletInMagazine == gunData.magazineSize) yield break;

        isReloading = true;
        rig.weight = 0f;
        playerAnimator.SetTrigger("Reloaded");
        yield return new WaitForSeconds(2.6f);
        rig.weight = 1f;
        isReloading = false;

        if (totalBulletsLeft <= gunData.magazineSize)
        {
            bulletInMagazine = totalBulletsLeft;
            totalBulletsLeft = 0;
        }
        else
        {
            totalBulletsLeft -= gunData.magazineSize - bulletInMagazine;
            bulletInMagazine = gunData.magazineSize;
        }
    }
}
