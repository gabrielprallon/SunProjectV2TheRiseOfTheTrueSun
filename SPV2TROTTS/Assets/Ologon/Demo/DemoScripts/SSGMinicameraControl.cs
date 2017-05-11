using UnityEngine;
using System.Collections;

public class SSGMinicameraControl : MonoBehaviour {
    public GameObject currentActive = null;
    public GameManager gm;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeCamera(GameObject newCamera)
    {
        //if (gm.gameMode == 0) { 
            if (currentActive != null)
                currentActive.SetActive(false);
            newCamera.SetActive(true);
            currentActive = newCamera;
        //}
    }

    public void ClearCamera()
    {

        if (currentActive != null)
            currentActive.SetActive(false);
        currentActive = null;
    }

    public void moveTo(GameObject planet)   
    {
        gameObject.SendMessage("Lock");
        Vector3 newCameraPos = new Vector3(planet.transform.position.x, transform.position.y, planet.transform.position.z - 34f);
        transform.position = newCameraPos;
        gameObject.SendMessage("ResetPosition");
        gameObject.SendMessage("UnLock");
        if(gm.gameMode == 0)
            planet.SendMessage("ChangeToPlanet", SendMessageOptions.DontRequireReceiver);
    }
}
