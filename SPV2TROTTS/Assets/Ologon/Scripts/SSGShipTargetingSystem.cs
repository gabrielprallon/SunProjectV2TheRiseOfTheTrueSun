using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SSGShipTargetingSystem : MonoBehaviour {
    public SSGShipIA myship;
    List<GameObject> Targets = new List<GameObject>();
    System.Random rand = new System.Random();
	// Use this for initialization
	void Start () {
        myship = transform.parent.gameObject.GetComponent<SSGShipIA>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       // StartCoroutine(RevalidateTargets());
    }

    IEnumerator RevalidateTargets()
    {
        yield return new WaitForSeconds(0.06f);
        if (myship.EnemyTarget == null)
        {
            if (Targets.Count > 0)
            {
                Targets.RemoveAll(item => item == null);
                int index = rand.Next(Targets.Count);
                myship.EnemyTarget = Targets[index % Targets.Count];
                Targets.RemoveAt(index);
            }
        }
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ship" + ((myship.faction + 1) % 2).ToString())
        {
            Targets.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (Targets.Contains(other.gameObject))
        {
            Targets.Remove(other.gameObject);
        }
    }*/
    /*
    void OnTriggerStay(Collider other)
    {
        if (myship.EnemyTarget == null)
        {
            if (other.gameObject.tag == "Ship"+((myship.faction+1)%2).ToString())
            {
                myship.TargetEnemy(other.gameObject);
            }
        }
    }*/
}
