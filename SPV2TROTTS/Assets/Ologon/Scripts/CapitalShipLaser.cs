using UnityEngine;
using System.Collections;

public class CapitalShipLaser : MonoBehaviour {

    LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.enabled = true;
    }

    public void init(Color start, Color end, Vector3 origin, Vector3 target, float delay)
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.SetColors(start, end);
        lr.SetPosition(0, origin);
        lr.SetPosition(1, target);
        StartCoroutine(Fire(delay));
        lr.enabled = true;

    }
	
    IEnumerator Fire(float delay)
    {
        lr.enabled = true;
        yield return new WaitForSeconds(delay);
        lr.enabled = false;
        Destroy(gameObject);
        yield return null;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
