using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
    [Header("Game manager")]
    public GameManager gm;
    [Header("Big blocks")]
    public GameObject Ologon;
    public GameObject PlanetInfoLeft;
    public GameObject PlanetInfoRight;
    public GameObject ShipInfo;
    public GameObject TrianglePanel;
    public GameObject ShipsButtons;
    [Header("Planet Info")]
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
    [Header("Ship info")]
    public UnityEngine.UI.Text ShipTitle;
    public UnityEngine.UI.Text ShipAmout;
    public UnityEngine.UI.Text ShipPower;
    public UnityEngine.UI.Text ShipCost;
    public UnityEngine.UI.Image cost1;
    public UnityEngine.UI.Image cost2;
    public UnityEngine.UI.Image cost3;
    public UnityEngine.UI.Image cost4;
    public UnityEngine.UI.Image ShipImage;
    [Header("Images infos")]
    public Sprite gasCost;
    public Sprite crystalCost;
    public Sprite oreCost;
    public Sprite DogFighterImg;
    public Sprite BomberImg;
    public Sprite TraderImg;
    public Sprite SatImg;
    public Sprite CapitalImg;

    private SSGPlanet planetSelected;
    private GameObject Selected = null;
    private int infoSelected = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateShipInfo();
        ShipInfo.SetActive(true);
        if (Selected == null)
        {
            Ologon.SetActive(true);
            PlanetInfoLeft.SetActive(false);
            PlanetInfoRight.SetActive(false);
            PlanetName.gameObject.SetActive(false);
            //ShipsButtons.SetActive(false);
            TrianglePanel.SetActive(false);
        } else
        {
            PlanetName.gameObject.SetActive(true);
            Ologon.SetActive(false);
            PlanetInfoRight.SetActive(true);
            PlanetInfoLeft.SetActive(true);
            TrianglePanel.SetActive(true);

            if(planetSelected != null)
            {
                PlanetName.text = planetSelected.PlanetName;
                ShipsSlots.text = planetSelected.GetShipSlots().ToString(); // not done yet
                Freighters.text = planetSelected.FreighterAmount.ToString();
                Bombers.text = planetSelected.BomberAmount.ToString();
                Dogfighters.text = planetSelected.DogFighterAmount.ToString();
                SatSlots.text = planetSelected.SatsBuilt.ToString() + "/" + planetSelected.SatSlotsAmount.ToString();
                Gas.text = planetSelected.GasAmount.ToString();
                Ore.text = planetSelected.OreAmount.ToString();
                Crystal.text = planetSelected.CrystalAmount.ToString();

                
                switch (planetSelected.GasRate)
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

                switch (planetSelected.OreRate)
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

                switch (planetSelected.CrystalRate)
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

    public void UpdateShipInfo()
    {
        switch (infoSelected)
        {
            case 0:
                ShipTitle.text = "Humming Bird Mk 1 - Dog Fighter Class";
                ShipAmout.text = "Total: " + (planetSelected != null ? gm.SelectedPlanet.DogFighterAmount.ToString() : gm.sp.CountShips(infoSelected).ToString());
                ShipPower.text = "Power: 42";
                ShipCost.text = "Cost:";
                cost1.gameObject.SetActive(true);
                cost2.gameObject.SetActive(false);
                cost3.gameObject.SetActive(false);
                cost4.gameObject.SetActive(false);
                cost1.sprite = oreCost;
                ShipImage.sprite = DogFighterImg;
                break;

            case 1:
                ShipTitle.text = "Bomber Class";
                ShipAmout.text = "Total: " + (planetSelected != null ? gm.SelectedPlanet.BomberAmount.ToString(): gm.sp.CountShips(infoSelected).ToString());
                ShipPower.text = "Power: 90";
                ShipCost.text = "Cost:";
                cost1.gameObject.SetActive(true);
                cost2.gameObject.SetActive(true);
                cost3.gameObject.SetActive(false);
                cost4.gameObject.SetActive(false);
                cost1.sprite = gasCost;
                cost2.sprite = gasCost;
                ShipImage.sprite = BomberImg;
                break;

            case 2:
                ShipTitle.text = "Comet 04 - Freighter Class";
                ShipAmout.text = "Total: " + (planetSelected != null ? gm.SelectedPlanet.FreighterAmount.ToString() : gm.sp.CountShips(infoSelected).ToString());
                ShipPower.text = "Power: 30";
                ShipCost.text = "Cost:";
                cost1.gameObject.SetActive(true);
                cost2.gameObject.SetActive(false);
                cost3.gameObject.SetActive(false);
                cost4.gameObject.SetActive(false);
                cost1.sprite = crystalCost;
                ShipImage.sprite = TraderImg;
                break;

            case 3:
                ShipTitle.text = "Planetary Halo Defense - Satellite System Class";
                ShipAmout.text = "Total: " + (planetSelected != null ? gm.SelectedPlanet.SatsBuilt.ToString() + "/" + gm.SelectedPlanet.SatSlotsAmount.ToString() : gm.sp.CountShips(infoSelected).ToString());
                ShipPower.text = "Power: 508";
                ShipCost.text = "Cost:";
                cost1.gameObject.SetActive(true);
                cost2.gameObject.SetActive(true);
                cost3.gameObject.SetActive(true);
                cost4.gameObject.SetActive(false);
                cost1.sprite = oreCost;
                cost2.sprite = oreCost;
                cost3.sprite = crystalCost;
                ShipImage.sprite = SatImg;
                break;

            case 4:
                ShipTitle.text = "Menace - Capital Class";
                ShipAmout.text = "Total: " + (planetSelected != null ? (gm.SelectedPlanet.dockControl != null ?  (gm.SelectedPlanet.dockControl.friendDocked ? "1" : "0") : "0") : gm.sp.CountShips(infoSelected).ToString());
                ShipPower.text = "Power: 1242";
                ShipCost.text = "Cost: 500 ex omnibus";
                cost1.gameObject.SetActive(false);
                cost2.gameObject.SetActive(false);
                cost3.gameObject.SetActive(false);
                cost4.gameObject.SetActive(false);
                ShipImage.sprite = CapitalImg;
                break;
        }
    }

    public void ChangeSelected()
    {
        Selected = gm.Selected;
        if(Selected != null)
            planetSelected = Selected.GetComponent<SSGPlanet>();
        else
            planetSelected = null;
    }

    public void ChangeShipInfo(int type)
    {
        infoSelected = type;
        UpdateShipInfo();
        
    }
}
