using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundTrigger : MonoBehaviour {

    [SerializeField] UseWeapon playerWeaponController;


    private void Start()
    {
        if (playerWeaponController == null)
        {
            playerWeaponController = GetComponentInParent<UseWeapon>();
        }
    }

    public void PlayPrimaryWeaponSound()
    {
        playerWeaponController.PlayRandomWeaponNoise(playerWeaponController.equippedWeapon1);
    }

    public void PlaySecondaryWeaponSound()
    {
        playerWeaponController.PlayRandomWeaponNoise(playerWeaponController.equippedWeapon2);
    }

}
