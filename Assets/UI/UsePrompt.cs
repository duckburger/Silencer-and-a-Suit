using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UsePrompt : MonoBehaviour {

    [SerializeField] Transform player;
    [SerializeField] GameObject usePromptPrefab;
    [SerializeField] UnityEvent onActivateEvent;
    [SerializeField] UnityEvent onDeactivateEvent;

    GameObject myPrompt;
    bool isInRange;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () {
        if (Vector2.Distance(this.transform.position, player.position) < 2f)
        {
            if (usePromptPrefab != null && myPrompt == null)
            {
                myPrompt = Instantiate(usePromptPrefab, this.transform.position, Quaternion.identity, this.transform);
                isInRange = true;
            }
        }
        else
        {
            if (myPrompt != null)
            {
                Destroy(myPrompt);
                isInRange = false;
                onDeactivateEvent.Invoke();
            }
        }

        HandleUse();
    }


    void HandleUse()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            onActivateEvent.Invoke();
        }
    }
}
