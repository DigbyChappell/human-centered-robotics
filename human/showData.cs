using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR;

public class showData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {
        var gazeRay = TobiiXR.EyeTrackingData.GazeRay;

        // Check if gaze ray is valid
        if (TobiiXR.EyeTrackingData.GazeRay.IsValid)
        {
            // Eye Origin is in World Space
            var rayOrigin = TobiiXR.EyeTrackingData.GazeRay.Origin;
            Debug.Log("User gaze origin: "+rayOrigin);
            //Debug.Log(rayOrigin);
            // Eye Direction is a normalized direction vector in World Space
            var rayDirection = TobiiXR.EyeTrackingData.GazeRay.Direction;
            Debug.Log("User gaze direction: "+rayDirection);
            //Debug.Log(rayDirection);
        }

        // The EyeBlinking bool is true when the eye is closed
        var isLeftEyeBlinking = TobiiXR.EyeTrackingData.IsLeftEyeBlinking;
        var isRightEyeBlinking = TobiiXR.EyeTrackingData.IsRightEyeBlinking;

        if (isLeftEyeBlinking | isRightEyeBlinking)
        {
            Debug.Log("User is blinking!");
        }
    }
}
