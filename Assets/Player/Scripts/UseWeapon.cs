﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour {

    [SerializeField] WeaponData equippedWeapon1;
    [SerializeField] WeaponData equippedWeapon2;
    [SerializeField] Transform projectileStartPos;
    [SerializeField] AudioSource weaponAudioSource;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] MeleeInRangeDetector meleeRangeDetector;
    [SerializeField] LayerMask immediateFrontMask;

    bool weap1InUse;
    bool weap2InUse;


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetMouseButton(0))
        {
            UseCurrentWeapon1();
        }

        if (Input.GetMouseButton(1))
        {
            UseCurrentWeapon2();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myRigidbody.velocity = Vector2.zero;
    }

    void UseCurrentWeapon1()
    {
        if (!weap1InUse)
        {
            weap1InUse = true;
            weaponAudioSource.clip = equippedWeapon1.fireSounds[Random.Range(0, equippedWeapon1.fireSounds.Count)];
            weaponAudioSource.Play();
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;

            RaycastHit2D immediateFrontCheck = Physics2D.Raycast(projectileStartPos.position, dir, 0.2f, immediateFrontMask);
            if (immediateFrontCheck.collider != null)
            {
                Debug.Log("You are standing right in front of an environment object! I am not even spawning that bullet!");
                StartCoroutine(Weapon1Cooldon());
                return;
            }
            // Actually do the shooting/hitting
            switch (equippedWeapon1.weapongType)
            {
                case weaponTypes.Melee:

                    break;
                case weaponTypes.Ranged:

                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    Vector2 bulletPos = new Vector2(projectileStartPos.position.x, projectileStartPos.position.y);
                    //WeaponEffects(equippedWeapon1);
                    Bullet newBullet = Instantiate(equippedWeapon1.projectile, bulletPos, newRotation).GetComponent<Bullet>();
                    newBullet.direction = dir;
                    break;
                default:
                    break;
            }

            StartCoroutine(Weapon1Cooldon());
            
        }
    }

    void UseCurrentWeapon2()
    {
        if (!weap2InUse)
        {
            weap2InUse = true;
            weaponAudioSource.clip = equippedWeapon2.fireSounds[Random.Range(0, equippedWeapon2.fireSounds.Count)];
            weaponAudioSource.Play();
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;

            RaycastHit2D immediateFrontCheck = Physics2D.Raycast(projectileStartPos.position, dir, 0.2f, immediateFrontMask);
            if (immediateFrontCheck.collider != null)
            {
                Debug.Log("You are standing right in front of an environment object! I am not even spawning that bullet!");
                StartCoroutine(Weapon2Cooldon());
                return;
            }
            // Actually do the shooting/hitting
            switch (equippedWeapon2.weapongType)
                {
                    case weaponTypes.Melee:
                        // Dash forward towards mouse, draw a sphere in fron and kill people in sphere

                        GetComponent<CharMovement>().body.GetComponent<Animator>().SetTrigger("swordAttack");
                        List<IDamageable> inRangeList = meleeRangeDetector.GetListOfObjInMeleeRange();
                        if (inRangeList.Count > 0)
                        {
                            foreach (IDamageable obj in inRangeList.ToArray())
                            {
                                obj.GetDamaged();
                            }
                        }

                        break;
                    case weaponTypes.Ranged:
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        Quaternion towardsMouseRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        Vector2 bulletPos = new Vector2(projectileStartPos.position.x, projectileStartPos.position.y);
                        //WeaponEffects(equippedWeapon2);
                        Bullet newBullet = Instantiate(equippedWeapon1.projectile, bulletPos, towardsMouseRotation).GetComponent<Bullet>();
                        newBullet.direction = dir;

                        break;
                    default:
                        break;
                }

            StartCoroutine(Weapon2Cooldon());
            
        }
    }


    IEnumerator Weapon1Cooldon()
    {
        yield return new WaitForSeconds(equippedWeapon1.usageCooldown);
        weap1InUse = false;
    }

    IEnumerator Weapon2Cooldon()
    {
        yield return new WaitForSeconds(equippedWeapon2.usageCooldown);
        weap2InUse = false;
    }

    void WeaponEffects(WeaponData weaponToUse)
    {
        Instantiate(weaponToUse.projectileTrail, projectileStartPos.position, projectileStartPos.rotation);
    }
}
