using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] float movementSpeed;

	
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

        Vector2 movementVector = new Vector2(horizMovement, verticalMovement);
        myRigidBody.AddForce(movementVector * movementSpeed);
    }
}
