using UnityEngine;
using System.Collections;

public class BuildBarManager : MonoBehaviour {
    public float maxSize = 19.6f;
    public Sprite set;
    public Sprite build;
    public GameManager gm;
    public int type;
    public UnityEngine.UI.Image img;
    public ButtonImageManager setImage;
    public UnityEngine.UI.Mask myMask;

    private float maxTime;
    private GameObject newSat = null;

    public void testeClick()
    {
        Debug.Log("Clicou");
    }
    // Use this for initialization
    void Start () {
        myMask = gameObject.GetComponent<UnityEngine.UI.Mask>();

        switch (type)
        {
            case 0:
                maxTime = 2f;
                break;
            case 1:
                maxTime = 3f;
                break;
            case 2:
                maxTime = 2f;
                break;
            case 3:
                maxTime = 30f;
                break;
            case 4:
                maxTime = 60f;
                break;
        }
	}

    // Update is called once per frame
    void Update()
    {

        if (gm.SelectedPlanet != null)
        {
            float satTime = -1f;
            if(gm.SelectedPlanet.satControl!=null) satTime = gm.SelectedPlanet.satControl.GetBuildingTime();
            if ((gm.SelectedPlanet.currentFaction == 0) && (gm.SelectedPlanet.CurrentBuilding == type))
            {
                img.sprite = build;
                if (type == 3)
                {

                }
                else
                {
                    if (type == 4)
                    {

                    }
                    else
                    {
                        setImage.DefineState(true);
                        myMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, gm.SelectedPlanet.constructionTimer * maxSize / maxTime);
                    }
                }
            }
            else
            {
                setImage.DefineState(false);
                if ((gm.SelectedPlanet.currentFaction == 0) && (satTime >= 0f) && (type==3)){
                    
                        img.sprite = build;
                        myMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, satTime * maxSize / maxTime);
                    

                }
                else {
                    myMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxSize);
                    img.sprite = set;
                }
            }
        } else
        {
            img.sprite = set;
        }
    }
    
    public void ChangeBuilding()
    {
        if(gm.SelectedPlanet.CurrentBuilding != type)
        {
            if(type == 3)
            {
                newSat = gm.SelectedPlanet.BuildSatellite();
            } else
            {
                
                {
                    myMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 19.6f);
                    gm.SelectedPlanet.ChangeConstruction(type);
                }
            }
        }
    }
}
