using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractsDesk : MonoBehaviour {

    [SerializeField] Transform player;
    [SerializeField] Animator myAnimator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        if (Vector2.Distance(this.transform.position, player.position) < 2f)
        {
            if (!myAnimator.GetBool("playerNear"))
            {
                myAnimator.SetBool("playerNear", true);
            }           
        }
        else
        {
            if (myAnimator.GetBool("playerNear"))
            {
                myAnimator.SetBool("playerNear", false);
            }
            
        }
	}



}
