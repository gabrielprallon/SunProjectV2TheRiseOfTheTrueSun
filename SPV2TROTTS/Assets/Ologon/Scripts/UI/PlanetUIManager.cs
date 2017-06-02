using UnityEngine;
using System.Collections;
using Vectrosity;

public class PlanetUIManager : MonoBehaviour {

    [Header("Config")]
    public GameManager gm;
    public GameObject planet;
    [Header("Faction Related Objects")]
    public UnityEngine.UI.Image Grid;
    public Sprite GridNeutral;
    public Sprite GridEnemy;
    public Sprite GridFriendly;
    public GameObject TrianglePanel;
    public SgtRing orbit;
    [Header("Planet Info")]
    public UnityEngine.UI.Text Population;
    public UnityEngine.UI.Text PlanetName;
    public UnityEngine.UI.Text ShipsSlots;
    public UnityEngine.UI.Text Freighters;
    public UnityEngine.UI.Text Bombers;
    public UnityEngine.UI.Text Dogfighters;
    public UnityEngine.UI.Text SatSlots;
    public UnityEngine.UI.Text Gas;
    public UnityEngine.UI.Text Ore;
    public UnityEngine.UI.Text Crystal;
    public UnityEngine.UI.Text GasGrowth;
    public UnityEngine.UI.Text OreGrowth;
    public UnityEngine.UI.Text CrystalGrowth;
    [Header("Buttons configs")]
    public UnityEngine.UI.Image DogfighterImage;
    public Sprite DogfighterUnselected;
    public Sprite DogfighterSelected;
    public UnityEngine.UI.Image BomberImage;
    public Sprite BomberUnselected;
    public Sprite BomberSelected;
    public UnityEngine.UI.Image FreighterImage;
    public Sprite FreighterUnselected;
    public Sprite FreighterSelected;
    public UnityEngine.UI.Image CapitalImage;
    public Sprite CapitalUnselected;
    public Sprite CapitalSelected;
    public GameObject ButtonGroup;

    public bool drawingLine = false;

    private SSGPlanet planetManager;
    private VectorLine myLine;
    // Use this for initialization
    void Start () {
        planetManager = planet.GetComponent<SSGPlanet>();
	}

    public void ChangeCapital()
    {
        gm.ChangeSelected(planet, planetManager.myCamera);
        if ((gm.gameMode == 0) || (gm.gameMode == 1 && gm.SelectedShipAttack != 0))
        {
            gm.ChangeModeAttack(3);
            DogfighterImage.overrideSprite = DogfighterUnselected;
            BomberImage.overrideSprite = BomberUnselected;
            FreighterImage.overrideSprite = FreighterUnselected;
            CapitalImage.overrideSprite = CapitalSelected;
        }
        else
        {
            gm.ChangeModeSelect();
            DogfighterImage.overrideSprite = DogfighterUnselected;
            BomberImage.overrideSprite = BomberUnselected;
            FreighterImage.overrideSprite = FreighterUnselected;
            CapitalImage.overrideSprite = CapitalUnselected;
        }
    }

    public void ChangeDogFighter()
    {
        /*
        switch (planetManager.CurrentBuilding)
        {
            case 0:
                DogfighterImage.overrideSprite = DogfighterSelected;
                BomberImage.overrideSprite = BomberUnselected;
                FreighterImage.overrideSprite = FreighterUnselected;
                break;
            case 1:
                DogfighterImage.overrideSprite = DogfighterUnselected;
                BomberImage.overrideSprite = BomberSelected;
                FreighterImage.overrideSprite = FreighterUnselected;
                break;
            case 2:
                DogfighterImage.overrideSprite = DogfighterUnselected;
                BomberImage.overrideSprite = BomberUnselected;
                FreighterImage.overrideSprite = FreighterSelected;
                break;
            default:
                DogfighterImage.sprite = DogfighterUnselected;
                BomberImage.sprite = BomberUnselected;
                FreighterImage.sprite = FreighterUnselected;
                break;
        }*/
        //planetManager.CurrentBuilding = 0;
        gm.ChangeSelected(planet, planetManager.myCamera);
        if ((gm.gameMode == 0) || (gm.gameMode == 1 && gm.SelectedShipAttack != 0))
        {
            gm.ChangeModeAttack(0);
            DogfighterImage.overrideSprite = DogfighterSelected;
            BomberImage.overrideSprite = BomberUnselected;
            FreighterImage.overrideSprite = FreighterUnselected;
            CapitalImage.overrideSprite = CapitalUnselected;
        }
        else
        {
            gm.ChangeModeSelect();
            DogfighterImage.overrideSprite = DogfighterUnselected;
            BomberImage.overrideSprite = BomberUnselected;
            FreighterImage.overrideSprite = FreighterUnselected;
            CapitalImage.overrideSprite = CapitalUnselected;
        }
        /*
        drawingLine = true;
        Vector2 planetPos = Camera.main.WorldToScreenPoint(planet.transform.position);
        Vector2 mousePos = Input.mousePosition;
        myLine = VectorLine.SetLine(Color.cyan, planetPos, mousePos);*/
    }

    public void ChangeBomber()
    {
        gm.ChangeSelected(planet, planetManager.myCamera);
        if ((gm.gameMode == 0) || (gm.gameMode == 1 && gm.SelectedShipAttack != 1))
        {
            gm.ChangeModeAttack(1);
            DogfighterImage.overrideSprite = DogfighterUnselected;
            BomberImage.overrideSprite = BomberSelected;
            FreighterImage.overrideSprite = FreighterUnselected;
            CapitalImage.overrideSprite = CapitalUnselected;
        }
        else
        {
            gm.ChangeModeSelect();
            DogfighterImage.overrideSprite = DogfighterUnselected;
            BomberImage.overrideSprite = BomberUnselected;
            FreighterImage.overrideSprite = FreighterUnselected;
            CapitalImage.overrideSprite = CapitalUnselected;
        }
    }
	
    public void ChangeFreighter()
    {

        gm.ChangeSelected(planet, planetManager.myCamera);
        if ((gm.gameMode == 0) || (gm.gameMode == 1 && gm.SelectedShipAttack != 2))
        {
            gm.ChangeModeAttack(2);
            DogfighterImage.overrideSprite = DogfighterUnselected;
            BomberImage.overrideSprite = BomberUnselected;
            FreighterImage.overrideSprite = FreighterSelected;
            CapitalImage.overrideSprite = CapitalUnselected;
        }
        else
        {
            gm.ChangeModeSelect();
            DogfighterImage.overrideSprite = DogfighterUnselected;
            BomberImage.overrideSprite = BomberUnselected;
            FreighterImage.overrideSprite = FreighterUnselected;
            CapitalImage.overrideSprite = CapitalUnselected;
        }
    }

    public void BuildSatellite()
    {
        planetManager.BuildSatellite();
    }

	// Update is called once per frame
	void Update () {
        switch (planetManager.currentFaction)
        {
            case 0:
                Grid.overrideSprite = GridFriendly;
                orbit.Brightness = 50;
                orbit.Color = new Color(0f,0.015f,1f,0.6f);
                ButtonGroup.SetActive(true);
                break;
            case 1:
                Grid.overrideSprite = GridEnemy;
                orbit.Brightness = 50;
                orbit.Color = new Color(1f, 0f, 0f, 0.6f);
                ButtonGroup.SetActive(false);
                break;
            default:
                Grid.overrideSprite = GridNeutral;
                orbit.Brightness = 50;
                orbit.Color = Color.white;
                ButtonGroup.SetActive(false);
                break;
        }
        if (gm.Selected != planet)
        {
            TrianglePanel.SetActive(false);
            DogfighterImage.overrideSprite = DogfighterUnselected;
            BomberImage.overrideSprite = BomberUnselected;
            FreighterImage.overrideSprite = FreighterUnselected;
        }
        else
        {
            TrianglePanel.SetActive(true);
            /*
            if (drawingLine)
            {
                Vector2 planetPos = Camera.main.WorldToScreenPoint(planet.transform.position);
                Vector2 mousePos = Input.mousePosition;
                myLine.points2[0] = planetPos;
                myLine.points2[1] = mousePos;
                myLine.Draw();
            }*/
        }
        if (planetManager != null)
        {
            PlanetName.text = planetManager.PlanetName;
            ShipsSlots.text = planetManager.GetShipSlots().ToString(); // not done yet
            Freighters.text = planetManager.FreighterAmount.ToString();
            Bombers.text = planetManager.BomberAmount.ToString();
            Dogfighters.text = planetManager.DogFighterAmount.ToString();
            SatSlots.text = planetManager.SatsBuilt.ToString() + "/" + planetManager.SatSlotsAmount.ToString();
            Gas.text = planetManager.GasAmount.ToString();
            Ore.text = planetManager.OreAmount.ToString();
            Crystal.text = planetManager.CrystalAmount.ToString();
            Population.text = planetManager.currentBuilt.ToString();

            switch (planetManager.GasRate)
            {
                case 0:
                    GasGrowth.text = "";
                    break;
                case 1:
                    GasGrowth.text = "+";
                    break;
                case 2:
                    GasGrowth.text = "++";
                    break;
                case 3:
                    GasGrowth.text = "+++";
                    break;
                default:
                    GasGrowth.text = "";
                    break;
            }

            switch (planetManager.OreRate)
            {
                case 0:
                    OreGrowth.text = "";
                    break;
                case 1:
                    OreGrowth.text = "+";
                    break;
                case 2:
                    OreGrowth.text = "++";
                    break;
                case 3:
                    OreGrowth.text = "+++";
                    break;
                default:
                    OreGrowth.text = "";
                    break;
            }

            switch (planetManager.CrystalRate)
            {
                case 0:
                    CrystalGrowth.text = "";
                    break;
                case 1:
                    CrystalGrowth.text = "+";
                    break;
                case 2:
                    CrystalGrowth.text = "++";
                    break;
                case 3:
                    CrystalGrowth.text = "+++";
                    break;
                default:
                    GasGrowth.text = "";
                    break;
            }


            
        }
    }
}
