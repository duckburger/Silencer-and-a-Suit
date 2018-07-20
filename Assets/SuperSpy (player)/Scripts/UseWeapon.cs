using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour {

    public WeaponData equippedWeapon1;
    public WeaponData equippedWeapon2;
    [SerializeField] Transform projectileStartPos;
    [SerializeField] AudioSource weaponAudioSource;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] MeleeInRangeDetector meleeRangeDetector;
    [SerializeField] LayerMask immediateFrontMask;
    [SerializeField] Animator myAnimator;
    [SerializeField] CharMovement myMovementController;

    bool weap1InUse;
    bool weap2InUse;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myMovementController = GetComponent<CharMovement>();
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetMouseButtonDown(0))
        {
            UseCurrentWeapon1();
        }

        if (Input.GetMouseButtonDown(1))
        {
            UseCurrentWeapon2();
        }
	}

    public Sprite GetCurrentWeaponUISprite()
    {
        return equippedWeapon1.weaponIcon;
    }

    public void ChangeWeapon(WeaponData newEquippedWeapon1, WeaponData newEquippedWeapon2 = null)
    {
        equippedWeapon1 = newEquippedWeapon1;
        if (newEquippedWeapon2 != null)
        {
            equippedWeapon2 = newEquippedWeapon2;
            return;
        }
        equippedWeapon2 = null;
    }

    public void SetupIdleAnimationStateForNewWeapons()
    {
        // Grabs the idle state name from the primary weapon in the pack
        myAnimator.Play(equippedWeapon1.idleStateName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myRigidbody.velocity = Vector2.zero;
    }

    #region Using weapons

    void UseCurrentWeapon1()
    {
        if (!weap1InUse && equippedWeapon1 != null)
        {
            weap1InUse = true;
            myAnimator.SetTrigger(equippedWeapon1.attackAnimTrigger);
            StartCoroutine(Weapon1Cooldown());
        }
    }

    void UseCurrentWeapon2()
    {
        if (!weap2InUse && equippedWeapon2 != null)
        {
            weap2InUse = true;
            myAnimator.SetTrigger(equippedWeapon2.attackAnimTrigger);
            StartCoroutine(Weapon2Cooldown());
        }
    }



    // Called fromt he animator during the animation
    public void AnimWeapon1Activate()
    {
        if (equippedWeapon1.stopsMovement)
        {
            myMovementController.TurnOffMovement();
        }
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        myMovementController.TurnLegsInLineWithBody();
        RaycastHit2D immediateFrontCheck = Physics2D.Raycast(projectileStartPos.position, dir, 0.2f, immediateFrontMask);
        if (immediateFrontCheck.collider != null)
        {
            Debug.Log("You are standing right in front of an environment object! I am not even spawning that bullet!");
            StartCoroutine(Weapon1Cooldown());
            return;
        }
        // Actually do the shooting/hitting
        switch (equippedWeapon1.weapongType)
        {
            case weaponTypes.Melee:
                // Handled inside the animator
                break;
            case weaponTypes.Ranged:

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Vector2 bulletPos = new Vector2(projectileStartPos.position.x, projectileStartPos.position.y);
                //WeaponEffects(equippedWeapon1);
                if (equippedWeapon1.inaccuracyRadius > 0)
                {
                    newRotation = GenerateRotWithInaccuracy(equippedWeapon1, newRotation);
                }
                Bullet newBullet = Instantiate(equippedWeapon1.projectile, bulletPos, newRotation).GetComponent<Bullet>();
                newBullet.direction = dir;
                break;
            default:
                break;
        }
    }

    
    // Called fromt he animator during the animation
    public void AnimWeapon2Activate()
    {
        if (equippedWeapon2.stopsMovement)
        {
            myMovementController.TurnOffMovement();
        }
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        myMovementController.TurnLegsInLineWithBody();
        RaycastHit2D immediateFrontCheck = Physics2D.Raycast(projectileStartPos.position, dir, 0.2f, immediateFrontMask);
        if (immediateFrontCheck.collider != null)
        {
            Debug.Log("You are standing right in front of an environment object! I am not even spawning that bullet!");
            StartCoroutine(Weapon2Cooldown());
            return;
        }
        // Actually do the shooting/hitting
        switch (equippedWeapon2.weapongType)
        {
            case weaponTypes.Melee:
                // Handled inside the animator
                break;
            case weaponTypes.Ranged:
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion towardsMouseRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Vector2 bulletPos = new Vector2(projectileStartPos.position.x, projectileStartPos.position.y);
                //WeaponEffects(equippedWeapon2);
                if (equippedWeapon2.inaccuracyRadius > 0)
                {
                    towardsMouseRotation = GenerateRotWithInaccuracy(equippedWeapon2, towardsMouseRotation);
                }
                Bullet newBullet = Instantiate(equippedWeapon2.projectile, bulletPos, towardsMouseRotation).GetComponent<Bullet>();
                newBullet.direction = dir;

                break;
            default:
                break;
        }

    }
    #endregion



    public void DamageEnemiesInMeleeRange()
    {
        List<IDamageable> inRangeList = meleeRangeDetector.GetListOfObjInMeleeRange();
        if (inRangeList.Count > 0)
        {
            foreach (IDamageable obj in inRangeList.ToArray())
            {
                obj.GetDamaged();
            }
        }
    }


    public void PlayRandomWeaponNoise(WeaponData weaponWithSounds)
    {
        weaponAudioSource.clip = weaponWithSounds.usageSounds[Random.Range(0, weaponWithSounds.usageSounds.Count)];
        weaponAudioSource.Play();
    }


    IEnumerator Weapon1Cooldown()
    {
        yield return new WaitForSeconds(equippedWeapon1.usageCooldown);
        if (equippedWeapon1.stopsMovement)
        {
            myMovementController.TurnOnMovement();
        }
        weap1InUse = false;
        Debug.Log("Setting the weapon 1 in use to " + weap1InUse);

    }

    IEnumerator Weapon2Cooldown()
    {
        yield return new WaitForSeconds(equippedWeapon2.usageCooldown);
        if (equippedWeapon2.stopsMovement)
        {
            myMovementController.TurnOnMovement();
        }
        weap2InUse = false;
    }

    void WeaponEffects(WeaponData weaponToUse)
    {
        Instantiate(weaponToUse.projectileTrail, projectileStartPos.position, projectileStartPos.rotation);
    }

    Quaternion GenerateRotWithInaccuracy(WeaponData weapon, Quaternion initialBulletRot)
    {
        float randomizedBulletAnlge = Random.Range(-weapon.inaccuracyRadius, weapon.inaccuracyRadius);
        Quaternion angleAdjustmentForInnacuracy = Quaternion.Euler(0, 0, randomizedBulletAnlge);
        return angleAdjustmentForInnacuracy * initialBulletRot;
    }
}
