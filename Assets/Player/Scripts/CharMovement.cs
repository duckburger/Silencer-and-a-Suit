using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] float movementSpeed;

    [Header("BODY PARTS")]
    [SerializeField] Transform legs;
    [SerializeField] Transform body;

	
    void Start()
    {
        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody2D>();
        }
    }

	// Update is called once per frame
	void Update () {

        float verticalMovement = Input.GetAxis("Vertical");
        float horizMovement = Input.GetAxis("Horizontal");
        Vector3 movementVector = new Vector3(horizMovement, verticalMovement, 0);
        myRigidBody.AddForce(movementVector * movementSpeed);

        if (body != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            body.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (legs != null)
        {
            float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
            legs.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
