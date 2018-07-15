using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour {

    [SerializeField] int speed = 230;

	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        Destroy(gameObject, 0.3f);
	}
    
}
