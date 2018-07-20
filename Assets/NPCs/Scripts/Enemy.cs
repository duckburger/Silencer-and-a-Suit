using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IKnockable {
    [Header("Object refs")]
    [SerializeField] AudioSource myAudioSource;
    [SerializeField] Animator myAnimator;

    [Header("Death related")]
    [SerializeField] GameObject deadBodyPrefab;
    [SerializeField] AudioClip myDeathSound;
    [Space(10)]
    [Header("Knockout related")]
    [SerializeField] AudioClip myKnockOutSound;
    [SerializeField] GameObject myZzzzs;

    bool isKnockedOut;

    public void GetDamaged()
    {
        Die();
    }

    public void GetKnockedOut(float duration)
    {
        if (isKnockedOut)
        {
            return;
        }
        isKnockedOut = true;
        // Play the knock out animation & sound, turn off movement/AI/etc for the duration of the knockout, stay in the knocked out anim state for the duration of the knockout
        Debug.Log(this.gameObject.name + " got knocked out!");
        myAudioSource.clip = myKnockOutSound; // Make this pick from an array at random
        myAudioSource.Play();
        myAnimator.SetTrigger("npc_knockedOut");
        myZzzzs.SetActive(true);
        StartCoroutine(KnockoutTimer(duration));
    }

    IEnumerator KnockoutTimer(float durationOfKnockout)
    {
        yield return new WaitForSeconds(durationOfKnockout);
        myZzzzs.SetActive(false);
        myAnimator.SetTrigger("npc_Idle");
        isKnockedOut = false;
    }

    private void Die(Collider2D collider = null)
    {
        SpawnDeadBody(collider);
        Destroy(gameObject);
    }


    private void SpawnDeadBody(Collider2D collision = null)
    {
        GameObject newDeadBod;
        if (collision != null)
        {
            newDeadBod = Instantiate(deadBodyPrefab, this.transform.position, collision.transform.rotation);
        }
        else
        {
            newDeadBod = Instantiate(deadBodyPrefab, this.transform.position, this.transform.rotation);
        }
       
        AudioSource deadBodyAudioSource = newDeadBod.GetComponent<AudioSource>();
        deadBodyAudioSource.clip = myDeathSound;
        deadBodyAudioSource.Play();
    }


    
}
