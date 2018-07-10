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

[CreateAssetMenu(menuName = "SAAS/Weapon")]
public class WeaponData : ScriptableObject {

    public GameObject projectile;
    public GameObject projectileTrail;
    public List<AudioClip> fireSounds = new List<AudioClip>();
    public float usageCooldown;
    public weaponTypes weapongType;

    public float meleeSlideDistance;

    [Header("Melee params")]
    public int startDashTimer;
    public int dashTimer;
    public float dashDistance;
}
