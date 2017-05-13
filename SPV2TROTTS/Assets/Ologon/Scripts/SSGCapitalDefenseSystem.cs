using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SSGCapitalDefenseSystem : MonoBehaviour {
    public SSGCapitalShip myplanet;

    public List<GameObject> Attackers = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        myplanet = transform.parent.gameObject.GetComponent<SSGCapitalShip>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attackers.RemoveAll(item => item == null);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship" + ((myplanet.faction + 1) % 2).ToString())
        {
            Attackers.Add(other.gameObject);
            
                Debug.Log("-----" + myplanet.faction.ToString());
                myplanet.sp.SpawnDefenders(transform.parent.gameObject, 1);
                Debug.Log("-----" + myplanet.faction.ToString());
           
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ship" + ((myplanet.faction + 1) % 2).ToString())
        {
            Attackers.Remove(other.gameObject);
        }

    }
}
