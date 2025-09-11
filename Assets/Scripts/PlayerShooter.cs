using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private int currentWeapon = 0;
    public Weapon[] weapons;
    private InputReader playerInput;

    private Transform cam;

    public GameObject weaponInventory;
    bool isInventoryOpen = false;

    public Transform aimTarget;

    private void Awake()
    {
        cam = Camera.main.transform;
        weapons = GetComponentsInChildren<Weapon>(true);
        playerInput = GetComponent<InputReader>();
    }

    private void Update()
    {
        aimTarget.position = cam.position + cam.forward * 10;

        if(playerInput.shootAction.IsPressed())
        {
            weapons[currentWeapon].Shoot();
        }

        if (playerInput.reloadAction.triggered)
        {
            StartCoroutine(weapons[currentWeapon].ReloadGun());
        }

        if(playerInput.inventoryAction.triggered)
        {
            weaponInventory.SetActive(!isInventoryOpen);
            weapons[currentWeapon].shootingDisabled = !weapons[currentWeapon].shootingDisabled;
            isInventoryOpen = !isInventoryOpen;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SelectWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == index)
            {
                weapons[i].gameObject.SetActive(true);
                currentWeapon = i;
            }
            else weapons[i].gameObject.SetActive(false);
        }

        weaponInventory.SetActive(false);

        isInventoryOpen = false;
        weapons[currentWeapon].shootingDisabled = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

}
