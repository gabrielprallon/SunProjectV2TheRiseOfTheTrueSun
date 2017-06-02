using UnityEngine;
using System.Collections;

public class SSGAttackButtonUI : MonoBehaviour {
    public GameManager gm;
    public int type;
    public ButtonImageManager img;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gm.gameMode == 0)
            img.DefineState(false);
        else if (gm.SelectedShipAttack == type)
        {
            img.DefineState(true);
        } else
        {
            img.DefineState(false);
        }
	}
}
