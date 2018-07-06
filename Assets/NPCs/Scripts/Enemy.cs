using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IKillable {

    [SerializeField] GameObject deadBodyPrefab;
    [SerializeField] AudioClip myDeathSound;


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Debug.Log("got hit by a bullet!");
            Die(collider);
        }
    }

    private void Die(Collider2D collider = null)
    {
        SpawnDeadBody(collider);
        Destroy(gameObject);
    }

    public void GetDamaged()
    {
        Die();
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
