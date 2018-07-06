using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] float movementSpeed;

    [Header("BODY PARTS")]
    public Transform legs;
    public Transform body;


    Quaternion lastBodyRot;
    Quaternion lastLegsRot;

    void Start()
    {
        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update() {

        float verticalMovement = Input.GetAxis("Vertical");
        float horizMovement = Input.GetAxis("Horizontal");
        float bodyAngle = 0;
        float legsAngle = 0;
        Vector3 movementVector = new Vector3(horizMovement, verticalMovement, 0);
        //Debug.Log("Movement vector is " + movementVector);
        myRigidBody.AddForce(movementVector * movementSpeed);

        if (body != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            bodyAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //Debug.Log("body angle is " + bodyAngle);
            body.rotation = Quaternion.AngleAxis(bodyAngle, Vector3.forward);
        }

        if (legs != null)
        {   if (verticalMovement > 0 || horizMovement > 0)
            {
                legsAngle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
                //Debug.Log("legs angle is " + legsAngle);
                legs.rotation = Quaternion.AngleAxis(legsAngle, Vector3.forward);
            }
            
        }

        //float diff = bodyAngle - legsAngle;
        //if (Mathf.Abs(diff) > 75)
        //{
        //    // Rotate legs with body if the angle is too steep
        //    legs.rotation = body.rotation;
        //}

        lastBodyRot = body.rotation;
        lastLegsRot = legs.rotation;
    }
  

    private void LateUpdate()
    {
        body.rotation = lastBodyRot;
        legs.rotation = lastLegsRot;
    }


}
