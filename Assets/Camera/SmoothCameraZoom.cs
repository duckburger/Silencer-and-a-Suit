using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraZoom : MonoBehaviour {

    [SerializeField] Camera cameraToZoom;
    [SerializeField] float scrollSpeed;


    private void Start()
    {
        if (cameraToZoom == null)
        {
            cameraToZoom = GetComponent<Camera>();
        }
    }


    private void Update()
    {
        float wheelScroll = Input.GetAxis("Mouse ScrollWheel");
        if (wheelScroll > 0 || wheelScroll < 0)
        {
            cameraToZoom.orthographicSize += -wheelScroll * scrollSpeed * Time.smoothDeltaTime;
        }

        cameraToZoom.orthographicSize = Mathf.Clamp(cameraToZoom.orthographicSize, 5, 11);
    }

}
