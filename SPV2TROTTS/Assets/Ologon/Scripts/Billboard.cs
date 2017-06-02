using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    //public TextMesh tm;
    public SSGPlanet ssgp;

	// Use this for initialization
	void Start () {
        //tm = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(Camera.main.transform);
        //tm.color = ssgp.faction.FactionColor;
        //tm.text = ssgp.DogFighterAmount.ToString();
	}
}
