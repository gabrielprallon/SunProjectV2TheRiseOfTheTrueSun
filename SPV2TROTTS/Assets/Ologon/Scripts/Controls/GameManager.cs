using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;
using HedgehogTeam.EasyTouch;

public class GameManager : MonoBehaviour {

    public Texture2D CustomCursor;

    [Header("Costs")]
    public int DogFighterGasCost = 0;
    public int DogFighterCrystalCost = 0;
    public int DogFighterOreCost = 2;
    public int BomberGasCost = 4;
    public int BomberCrystalCost = 0;
    public int BomberOreCost = 0;
    public int FreighterGasCost = 0;
    public int FreighterCrystalCost = 2;
    public int FreighterOreCost = 0;
    public int CapitalGasCost = 800;
    public int CapitalCrystalCost = 800;
    public int CapitalOreCost = 800;
    public int SatGasCost = 0;
    public int SatCrystalCost = 80;
    public int SatOreCost = 40;

    [Header("Planet")]
    public GameObject Selected;
    public SSGPlanet SelectedPlanet;
    public int SelectedShipAttack = 0; // 0 dogfighter 1 bomber 2 freighter
    public int SelectedShipBuilt = 0; // 0 dogfighter 1 bomber 2 freighter

    private List<Vector2> points = new List<Vector2>();
    private VectorLine myLine;

    [Header("Managers")]
    public SSGShipPool sp;
    public SSGMinicameraControl smc;

    public GUIManager gui;

    public List<GameObject> Planets;

    public int gameMode = 0; // 0 - select 1 - attack
    


    void OnMouseEnter()
    {
        //Cursor.SetCursor(CustomCursor, Vector2.zero, CursorMode.Auto);
    }
    void OnMouseExit()
    {
       // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    // Use this for initialization
    void Start ()
    {
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(0, 0));
        myLine = new VectorLine("Line", points, 4f); 
    }
	
	// Update is called once per frame
	void Update () {
        if ((gameMode == 1) && (Selected != null))
        {
            Vector2 planetPost = Camera.main.WorldToScreenPoint(Selected.transform.position);
            Vector2 mousePos = Input.mousePosition;
            myLine.points2[0] = planetPost;
            myLine.points2[1] = mousePos;
            myLine.active = true;
            myLine.color = Color.cyan;
            myLine.Draw();
        }
        else
            myLine.active = false;
	}

    void LateUpdate()
    {
    }

    public void AttackMode()
    {
        gameMode = 1;
    }

    public void SelectMode()
    {
        gameMode = 0;
    }

    public void ChangeSelected(GameObject newSelected, int SelectedSA, GameObject newCamera)
    {
        if (gameMode == 0)
        {
            Selected = newSelected;
            smc.ChangeCamera(newCamera);
            SelectedPlanet = Selected.GetComponent<SSGPlanet>();
            SelectedShipBuilt = SelectedPlanet.CurrentBuilding;
            SelectedShipAttack = SelectedSA;
            gui.ChangeSelected();
        }

    }

    public void ChangeModeAttack(int SelectedShip)
    {
        /* ships: 0 = dogfighter; 1 = bomber; 2 = trader */
        SelectedShipAttack = SelectedShip;
        gameMode = 1; // modo de ataque/envio de nave
    }

    public void AlternateMode(int SelectedShip)
    {
        if(gameMode == 0)
        {
            ChangeModeAttack(SelectedShip);
        } else
        {
            ChangeModeSelect();
        }
    }

    public void ChangeModeSelect()
    {
        gameMode = 0;
    }

    public void ChangeSelected(GameObject newSelected, GameObject newCamera) {
        /*if (Selected != null)
        {
            if (newSelected.GetComponent<SSGPlanet>().currentFaction == Selected.GetComponent<SSGPlanet>().currentFaction)
                gameMode = 0;
            else
                gameMode = 1;
        } else
        {
            gameMode = 0;
        }*/
        
        if (gameMode == 1)
        {
            if (Selected != null)
            {
                if (Selected.GetComponent<SSGPlanet>().currentFaction == 0) // só ataca se selecionado for do proprio jogador.
                {
                    //if (Selected.GetComponent<SSGPlanet>().CurrentBuilding == 0)
                    switch (SelectedShipAttack) {
                        case 0:
                            sp.SpawnDogFighter(Selected, newSelected, 1);
                            break;
                        case 1:
                            sp.SpawnBomber(Selected, newSelected, 1);
                            break;
                        case 2:
                            sp.SpawnTrader(Selected, newSelected, 1);
                            break;
                        case 3:
                            Selected.GetComponent<SSGDockingControl>().SendFriendly(newSelected);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
                gameMode = 0;
        }
        else
        {
            
                Selected = newSelected;
                smc.ChangeCamera(newCamera);
                SelectedPlanet = Selected.GetComponent<SSGPlanet>();
                SelectedShipBuilt = SelectedPlanet.CurrentBuilding;
                SelectedShipAttack = SelectedShipBuilt;
        }
        gui.ChangeSelected();
    }
    void OnEnable()
    {
        EasyTouch.On_TouchStart += On_TouchStart;
    }

    void OnDisable()
    {
        UnsubscribeEvent();
    }

    void OnDestroy()
    {
        UnsubscribeEvent();
    }

    void UnsubscribeEvent()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
    }

    private void On_TouchStart(Gesture gesture)
    {
        if(gesture.pickedObject == null || gesture.pickedObject.tag != "Planet")
        {
            Selected = null;
            gui.ChangeSelected();
            gameMode = 0;
            smc.ClearCamera();
        }
    }
}
