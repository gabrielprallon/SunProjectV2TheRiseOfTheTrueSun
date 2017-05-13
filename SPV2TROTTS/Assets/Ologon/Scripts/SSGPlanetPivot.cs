using UnityEngine;
using System.Collections;

public class SSGPlanetPivot : MonoBehaviour {
    // Use this for initialization
    public GameObject reference;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeToPlanet()
    {
        reference.SendMessage("ChangeToPlanet", SendMessageOptions.DontRequireReceiver);
    }
}
