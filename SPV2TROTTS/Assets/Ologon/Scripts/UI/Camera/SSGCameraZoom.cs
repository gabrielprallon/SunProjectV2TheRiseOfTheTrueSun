using UnityEngine;
using System.Collections;

public class SSGCameraZoom : MonoBehaviour {

    float cameraDistanceMax = 119f;
    float cameraDistanceMin = -3f;
    float scrollSpeed = 1000f;

    // Use this for initialization
    void Start () {
	
	}

    void Update()
    {

        // set camera position

        // Camera.main.fieldOfView = 45 - cameraDistance;
        float currentAxis = Input.GetAxis("Mouse ScrollWheel");
        /*if (currentAxis != 0) { 

            Debug.Log(currentAxis);
            Debug.Log(transform.position.y);
            Debug.Log(transform.localPosition.y);
        }*/
        
        if (((currentAxis > 0) && transform.localPosition.y > cameraDistanceMin)|| ((currentAxis < 0) && transform.localPosition.y < cameraDistanceMax))
            transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime);


    }
}
