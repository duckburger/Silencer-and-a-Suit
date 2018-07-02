using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {

    [SerializeField] Camera currentCamera;
    [SerializeField] Transform currentTarget;
    [SerializeField] float smoothing;


    private void Update()
    {
        Vector3 adjustedCharPos = new Vector3(currentTarget.transform.position.x, currentTarget.transform.position.y, -10f);
        Vector3 newCamPos = Vector2.Lerp(currentCamera.transform.position, adjustedCharPos, Time.time * smoothing);
        currentCamera.transform.position = new Vector3(newCamPos.x, newCamPos.y, -10f);
    }


}
