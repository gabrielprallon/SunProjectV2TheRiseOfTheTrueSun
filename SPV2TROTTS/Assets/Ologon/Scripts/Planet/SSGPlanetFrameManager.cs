using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SSGPlanetFrameManager : MonoBehaviour {
    UnityEngine.UI.Image frame;
    public Sprite rebelFrame;
    public Sprite empireFrame;
    public Sprite neutralFrame;
    public Sprite selectedFrame;
    public Text number;

    public SSGPlanet planet;
	// Use this for initialization
	void Start () {
        frame = gameObject.GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(planet != null)
        {
            switch (planet.currentFaction)
            {
                case 0:
                    frame.sprite = rebelFrame; 
                    number.color = new Color(0f, 0.513f,0.698f);
                    break;
                case 1:
                    frame.sprite = empireFrame;
                    number.color = new Color(0.5647f, 0.0078f, 0.07058f);
                    break;
                default:
                    frame.sprite = neutralFrame;
                    number.color = Color.gray;
                    break;
            }
            if (planet.gm.Selected == gameObject) { frame.sprite = selectedFrame; number.color = Color.white; }
            }
	}
}
