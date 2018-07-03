using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour {

    [SerializeField] WeaponData equippedWeapon;
    [SerializeField] Transform projectileStartPos;
    [SerializeField] AudioSource weaponAudioSource;

    bool isInUse;

	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetMouseButton(0))
        {
            UseCurrentWeapon();
        }
	}

    void UseCurrentWeapon()
    {
        if (!isInUse)
        {
            isInUse = true;
            weaponAudioSource.clip = equippedWeapon.fireSounds[Random.Range(0, equippedWeapon.fireSounds.Count - 1)];
            weaponAudioSource.Play();
            // Actually do the shooting/hitting
            switch (equippedWeapon.weapongType)
            {
                case weaponTypes.Melee:

                    break;
                case weaponTypes.Ranged:

                    Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 dir = Input.mousePosition - pos;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    Vector2 bulletPos = new Vector2(projectileStartPos.position.x, projectileStartPos.position.y);
                    Bullet newBullet = Instantiate(equippedWeapon.projectile, bulletPos, newRotation).GetComponent<Bullet>();
                    newBullet.direction = dir;
                    break;
                default:
                    break;
            }

            StartCoroutine(WeaponCooldown());
        }
    }


    IEnumerator WeaponCooldown()
    {
        yield return new WaitForSeconds(equippedWeapon.usageCooldown);
        isInUse = false;
    }
}
