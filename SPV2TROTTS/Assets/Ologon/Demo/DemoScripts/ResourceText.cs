using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceText : MonoBehaviour {
    public GameManager gm;
    public int ResourceType = 0;
    private Text t;
	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gm.Selected != null)
        {
            switch (ResourceType)
            {
                case 0:
                    t.text = gm.Selected.GetComponent<SSGPlanet>().OreAmount.ToString();
                    break;
                case 1:
                    t.text = gm.Selected.GetComponent<SSGPlanet>().GasAmount.ToString();
                    break;
                case 2:
                    t.text = gm.Selected.GetComponent<SSGPlanet>().CrystalAmount.ToString();
                    break;
            }
        }
	}
}
