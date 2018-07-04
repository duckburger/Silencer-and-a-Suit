using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour {

    [SerializeField] GameObject bloodSplatterPrefab;

    private void Start()
    {
        SplatterBlood();
    }

    void SplatterBlood()
    {
        float randX = Random.Range(-0.15f, 0.15f);
        float randY = Random.Range(-0.15f, 0.15f);
        Vector2 randomBloodPos = new Vector2(transform.position.x + randX, transform.position.y + randY);
        if (bloodSplatterPrefab != null)
        {
            Instantiate(bloodSplatterPrefab, randomBloodPos, Quaternion.identity);
        }
       
    }

}
