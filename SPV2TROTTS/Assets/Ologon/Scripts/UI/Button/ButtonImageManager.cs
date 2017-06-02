using UnityEngine;
using System.Collections;

public class ButtonImageManager : MonoBehaviour {
    public Sprite UnSelected;
    public Sprite Selected;
    public UnityEngine.UI.Image img;
    public bool currentState = false;
	// Use this for initialization
	void Start () {
        img = gameObject.GetComponent<UnityEngine.UI.Image>();
        if (currentState)
            Select();
        else
            UnSelect();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void Select()
    {
        img.sprite = Selected;
    }

    public void DefineState(bool state)
    {
        currentState = state;
        if (currentState)
            Select();
        else
            UnSelect();
    }

    public void UnSelect()
    {
        img.sprite = UnSelected;

    }

    public void ChangeState()
    {
        currentState = !currentState;
        if (currentState)
            Select();
        else
            UnSelect();
    }
}
