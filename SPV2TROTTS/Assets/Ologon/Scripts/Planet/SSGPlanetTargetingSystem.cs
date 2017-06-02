using UnityEngine;
using System.Collections.Generic;

public class SSGPlanetTargetingSystem : MonoBehaviour {
    public List<GameObject> Attackers = new List<GameObject>();
    public SSGPlanet myplanet;

    // Use this for initialization
    void Start () {
        myplanet = transform.parent.gameObject.GetComponent<SSGPlanet>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Attackers.RemoveAll(item => item == null);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship" + ((myplanet.currentFaction + 1) % 2).ToString())
        {
            Attackers.Add(other.gameObject);
            if (!myplanet.ChangingFaction)
            {
                Debug.Log("-----" + myplanet.currentFaction.ToString());
                myplanet.gm.sp.SpawnDefenders(transform.parent.gameObject, 1);
                Debug.Log("-----" + myplanet.currentFaction.ToString());
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ship" + ((myplanet.currentFaction + 1) % 2).ToString())
        {
            Attackers.Remove(other.gameObject);
        }

    }
}
