using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum weaponTypes
{
    Melee,
    Ranged,
}

[Serializable]
[CreateAssetMenu(menuName = "SAAS/Weapon")]
public class WeaponData : ScriptableObject {

    public Sprite weaponIcon;

    public GameObject projectile;
    public GameObject projectileTrail;
    public List<AudioClip> usageSounds = new List<AudioClip>();
    public float usageCooldown;
    public weaponTypes weapongType;
    public bool stopsMovement;
    public float meleeSlideDistance;

    [Header("Melee params")]
    public int startDashTimer;
    public int dashTimer;
    public float dashDistance;

    [Header("Animator param")]
    public string idleStateName;
    public string attackAnimTrigger;

}
