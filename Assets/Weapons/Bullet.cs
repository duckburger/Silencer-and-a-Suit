using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] float speed;
    [SerializeField] float despawnTime = 3f;

    Vector3 posLastFrame = Vector3.zero;
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

    

    // Update is called once per frame
    void FixedUpdate () {

        if (posLastFrame != Vector3.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(posLastFrame, transform.position, (transform.position - posLastFrame).magnitude);
            if (hit != false)
            {
                Debug.Log("Bullet hit " + hit.collider.gameObject.name);
                IKillable killableObj = hit.collider.GetComponent<IKillable>();
                if (killableObj != null)
                {
                    Debug.Log("Hit a killable character");
                    killableObj.GetDamaged();
                }

                if (hit.collider.gameObject.layer != 9 && hit.collider.gameObject.layer != 8)
                {
                    Destroy(gameObject);
                }
            }
        }
        posLastFrame = transform.position;

    }
}
