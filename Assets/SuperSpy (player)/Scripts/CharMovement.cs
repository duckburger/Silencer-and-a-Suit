using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] float movementSpeed;
    [SerializeField] bool isMobile;

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
        if (isMobile)
        {
            HandleMovement();
        }

    }

    private void HandleMovement()
    {
        float verticalMovement = Input.GetAxis("Vertical");
        float horizMovement = Input.GetAxis("Horizontal");

        Vector3 movementVector = new Vector3(horizMovement, verticalMovement, 0);
        //Debug.Log("Movement vector is " + movementVector);
        myRigidBody.AddForce(movementVector * movementSpeed);


        if (body != null)
        {
            float bodyAngle = 0;
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            bodyAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //Debug.Log("body angle is " + bodyAngle);
            body.rotation = Quaternion.AngleAxis(bodyAngle, Vector3.forward);
        }

        if (legs != null)
        {
            TurnLegsInLineWithControls();
        }

        // Find angle between legs' dir and  body's dir
        float diff = Mathf.Abs(body.rotation.z * Mathf.Rad2Deg - legs.rotation.z * Mathf.Rad2Deg);

        if (diff >= 30)
        {
            legs.rotation = body.rotation;
        }

        lastBodyRot = body.rotation;
        lastLegsRot = legs.rotation;
    }

    void TurnLegsInLineWithControls()
    {
        float vertMovement = Input.GetAxis("Vertical");
        float horMovement = Input.GetAxis("Horizontal");
        Vector3 moveVector = new Vector3(horMovement, vertMovement, 0);
        if (vertMovement > 0 || horMovement > 0)
        {
            float legsAngle = 0;
            legsAngle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            //Debug.Log("legs angle is " + legsAngle);
            legs.rotation = Quaternion.AngleAxis(legsAngle, Vector3.forward);
        }
    }

    public void TurnLegsInLineWithBody()
    {
        Vector3 bodyEulerAngles = new Vector3(body.eulerAngles.x, body.eulerAngles.y, body.eulerAngles.z);
        legs.eulerAngles = bodyEulerAngles;
        lastLegsRot = legs.rotation;
    }

    public void TurnOffMovement()
    {
        isMobile = false;
    }

    public void TurnOnMovement()
    {
        isMobile = true;
    }

    private void LateUpdate()
    {
        body.rotation = lastBodyRot;
        legs.rotation = lastLegsRot;
    }


}
