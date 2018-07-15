﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour {

    [Header("Weapon types")]
    [SerializeField] WeaponData punch;
    [SerializeField] WeaponData kick;
    [SerializeField] WeaponData silencedPistol;

    [Space(10)]
    [Header("Object refs")]
    [SerializeField] UseWeapon useWeaponController;
    [SerializeField] GameEvent switchedWeaponsEvent;


    private void Start()
    {
        if (useWeaponController == null)
        {
            useWeaponController = GetComponent<UseWeapon>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(2);
        }
    }

    void SelectWeapon(int code)
    {
        if (useWeaponController == null)
        {
            useWeaponController = GetComponent<UseWeapon>();
        }

        switch (code)
        {
            case 1:
                useWeaponController.ChangeWeapon(punch, kick);
                break;
            case 2:
                useWeaponController.ChangeWeapon(silencedPistol, punch);
                break;
            default:
                break;
        }
        switchedWeaponsEvent.Invoke();
    }
}
