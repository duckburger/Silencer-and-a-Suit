using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponController : MonoBehaviour {

    [SerializeField] UseWeapon playerWeaponController;
    [SerializeField] Image currentWeaponUIIcon;

    private void Awake()
    {
        if (playerWeaponController == null)
        {
            playerWeaponController = GameObject.FindGameObjectWithTag("Player").GetComponent<UseWeapon>();
        }
    }

 

    public void UpdateCurrentWeaponUI()
    {
        if (playerWeaponController != null)
        {
            currentWeaponUIIcon.sprite = playerWeaponController.GetCurrentWeaponUISprite();
        }
    }
}
