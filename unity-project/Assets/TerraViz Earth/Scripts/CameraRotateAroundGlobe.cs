//Simple camera rotate script - trimmed down version of CameraRotateAroundGlobe in TerraViz
//Created by Julien Lynge @ Fragile Earth Studios

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraRotateAroundGlobe : MonoBehaviour
{
    private float altMiles;
    public float minAltitude = 100f;
    public float maxAltitude = 15000f;

    public float lat = 30f, lon = -70f;

    public float rotateSpeed = 100f;

    void Start()
    {
        altMiles = maxAltitude / 2f;
        applyPosInfoToTransform();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Input events

    void Update()
    {
            //Move camera
            if (Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0) //user is leftclick dragging - move camera along lat/lon
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    Vector2 posChange = new Vector2(-Input.GetAxis("Mouse X") * rotateSpeed * altMiles / maxAltitude, -Input.GetAxis("Mouse Y") * rotateSpeed * altMiles / maxAltitude);
                    lon += posChange.x;
                    lat += posChange.y;
                }
                
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    float smoothedTime = Mathf.Sqrt(Time.deltaTime / 0.02f);
                    altMiles *= 1f - Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * smoothedTime * 1f, -.8f, .4f);
                altMiles = Mathf.Clamp(altMiles, minAltitude, maxAltitude);
                }

                lat = Mathf.Clamp(lat, -90f, 90f);

                applyPosInfoToTransform();
            }
    }

    protected void applyPosInfoToTransform()
    {
        Quaternion rotation = Quaternion.Euler(lat, -lon, 0);
        Vector3 position = -(Quaternion.Euler(lat, -lon, 0) * Vector3.forward * (altMiles * 1000f / 3954.44494f + 1000f));

        transform.rotation = rotation;
        transform.position = position;
    }

    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
