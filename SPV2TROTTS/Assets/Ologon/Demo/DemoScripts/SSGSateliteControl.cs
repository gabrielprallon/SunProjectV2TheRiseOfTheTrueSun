using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SSGSateliteControl : MonoBehaviour {
    public List<GameObject> DefSats = new List<GameObject>();
    Queue<GameObject> SatsUnbuilt = new Queue<GameObject>();
    public List<GameObject> SatsBuilt = new List<GameObject>();
    public int slots;
    private GameObject lastBuilt = null;
    private SSGSatelite lastBuiltSat = null;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < DefSats.Count; i++)
            SatsUnbuilt.Enqueue(DefSats[i]);
        slots = SatsUnbuilt.Count;
        //Build();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float GetBuildingTime()
    {
        if(lastBuiltSat != null)
        {
            if (lastBuiltSat.building)
            {
                return lastBuiltSat.timeSinceStart;
            }
        }
        return -1f;
    }

    public GameObject Build()
    {
        if (slots > 0)
        {
            if (lastBuilt == null || lastBuilt.GetComponent<SSGSatelite>().building == false)
            {
                lastBuilt = SatsUnbuilt.Dequeue();
                lastBuilt.SetActive(true);
                lastBuilt.GetComponent<SSGSatelite>().Build(gameObject.GetComponent<SSGPlanet>().currentFaction);
                SatsBuilt.Add(lastBuilt);
                lastBuiltSat = lastBuilt.GetComponent<SSGSatelite>();
                return lastBuilt;
            }
        }
        return null;
    }

    public void AddSlot(GameObject destroyed)
    {
        SatsBuilt.Remove(destroyed);
        SatsUnbuilt.Enqueue(destroyed);
        destroyed.SetActive(false);
        slots++;
    }
}
