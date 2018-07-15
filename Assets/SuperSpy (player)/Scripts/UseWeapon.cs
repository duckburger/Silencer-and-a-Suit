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


    private void Start()
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

    public void AdjustAnimationsForNewWeapons()
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
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            myMovementController.TurnLegsInLineWithBody();
            RaycastHit2D immediateFrontCheck = Physics2D.Raycast(projectileStartPos.position, dir, 0.2f, immediateFrontMask);
            if (immediateFrontCheck.collider != null)
            {
                Debug.Log("You are standing right in front of an environment object! I am not even spawning that bullet!");
                StartCoroutine(Weapon1Cooldone());
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
                    Bullet newBullet = Instantiate(equippedWeapon1.projectile, bulletPos, newRotation).GetComponent<Bullet>();
                    newBullet.direction = dir;
                    break;
                default:
                    break;
            }

            myAnimator.SetTrigger(equippedWeapon1.attackAnimTrigger);
            StartCoroutine(Weapon1Cooldone());
            
        }
    }


    void UseCurrentWeapon2()
    {
        if (!weap2InUse && equippedWeapon2 != null)
        {
            weap2InUse = true;
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            myMovementController.TurnLegsInLineWithBody();
            RaycastHit2D immediateFrontCheck = Physics2D.Raycast(projectileStartPos.position, dir, 0.2f, immediateFrontMask);
            if (immediateFrontCheck.collider != null)
            {
                Debug.Log("You are standing right in front of an environment object! I am not even spawning that bullet!");
                StartCoroutine(Weapon2Cooldone());
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
                        Bullet newBullet = Instantiate(equippedWeapon1.projectile, bulletPos, towardsMouseRotation).GetComponent<Bullet>();
                        newBullet.direction = dir;

                        break;
                    default:
                        break;
                }

            myAnimator.SetTrigger(equippedWeapon2.attackAnimTrigger);
            StartCoroutine(Weapon2Cooldone());
            
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


    IEnumerator Weapon1Cooldone()
    {
        yield return new WaitForSeconds(equippedWeapon1.usageCooldown);
        weap1InUse = false;
    }

    IEnumerator Weapon2Cooldone()
    {
        yield return new WaitForSeconds(equippedWeapon2.usageCooldown);
        weap2InUse = false;
    }

    void WeaponEffects(WeaponData weaponToUse)
    {
        Instantiate(weaponToUse.projectileTrail, projectileStartPos.position, projectileStartPos.rotation);
    }
}
