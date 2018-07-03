using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deadBodyPrefab;
    [SerializeField] AudioClip myDeathSound;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            SpawnDeadBody(collision);
            Destroy(gameObject);
        }
    }

    private void SpawnDeadBody(Collision2D collision)
    {
        GameObject newDeadBod = Instantiate(deadBodyPrefab, this.transform.position, collision.transform.rotation);
        AudioSource deadBodyAudioSource = newDeadBod.GetComponent<AudioSource>();
        deadBodyAudioSource.clip = myDeathSound;
        deadBodyAudioSource.Play();
    }
}
