using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] float speed;
    [SerializeField] float despawnTime = 3f;

    public Vector3 direction;

	// Use this for initialization
	void Start () {
		if (myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
        myRigidbody.AddForce(direction * speed);
        StartCoroutine(TimerForDespawn());
    }

    IEnumerator TimerForDespawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        } 
    }

    // Update is called once per frame
    void Update () {
		
	}
}
