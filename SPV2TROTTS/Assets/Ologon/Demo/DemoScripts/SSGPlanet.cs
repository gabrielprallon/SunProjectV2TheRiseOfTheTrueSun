using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SSGPlanet : MonoBehaviour {
    // Resources

    [Header("Planet Info")]
    public string PlanetName = "Bratslava";
    public int planetSize = 2; // quanto maior.. maior hue max = 3
    public GameObject myCamera;
    public bool ChangingFaction = false;

    [Header("Resources")]
    public int GasAmount = 0;
    public int GasRate = 0;
    public int CrystalAmount = 0;
    public int CrystalRate = 0;
    public int OreAmount = 0;
    public int OreRate = 0;
    private float TimeSinceResources;


    [Header("Ships")]
    public int CurrentBuilding = 0; // 0 Dogfighter - 1 Bomber - 2 Tradeship
    public int DogFighterAmount = 0;
    public int BomberAmount = 0;
    public int FreighterAmount = 0;
    public int popLimit = 0;
    public int currentBuilt = 0;

    [Header("Sats n Capitals")]
    public int SatSlotsAmount = 0;
    public int SatsBuilt = 0;
    public bool hasDock = true;
    public SSGSateliteControl satControl;
    public SSGDockingControl dockControl;

    [Header("Faction")]
    public int currentFaction = 0; // 0 Rebels - 1 Empire
    public SSGFaction faction;

    [Header("Defenders")]
    public List<GameObject> Defenders = new List<GameObject>();
    public SSGPlanetTargetingSystem spts;

    [Header("Game Manager")]
    public GameManager gm;

    public float constructionTimer;

	// Use this for initialization
	void Start () {
        constructionTimer = 0f;
        if (currentFaction == 0)
            faction = new SSGFaction(0, Color.blue);
        if (currentFaction == 1)
            faction = new SSGFaction(1, Color.red);
        TimeSinceResources = 0f;
        if(hasDock)
            dockControl = gameObject.GetComponent<SSGDockingControl>();
    }
	// Update is called once per frame
	void Update () {
	    
	}

    public GameObject BuildSatellite()
    {
        GameObject hold = null;
        if (SatsBuilt < SatSlotsAmount)
        {
            hold = satControl.Build();
            if (hold != null)
            {
                SatsBuilt++;
                return hold;
            }
        }
        return hold;
    }
    
    public int GetShipSlots()
    {
        return popLimit - currentBuilt - Defenders.Count;
    }

    public void AddDefenders(GameObject def)
    {
            Defenders.Add(def);
        
    }

    public void CheckDefenders()
    {
        if (spts.Attackers.Count == 0)
        {
            RecallDefenders();
        }
    }

    public void CheckAttackers()
    {
        if (!ChangingFaction)
        {
            spts.Attackers.RemoveAll(item => item == null);
            if ((spts.Attackers.Count > Defenders.Count) && (DogFighterAmount >= 4))
                gm.sp.SpawnDefenders(gameObject, spts.Attackers.Count - Defenders.Count);
        }
    }

    public void RecallDefenders()
    {
        for (int i = 0; i < Defenders.Count; i++)
            Defenders[i].GetComponent<SSGShipIA>().Recall();
    }

    /*void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Ship" + ((currentFaction + 1) % 2).ToString())
        {
            SSGShipIA littleDude = other.gameObject.GetComponent<SSGShipIA>();
            if (littleDude.PlanetTarget == gameObject)
            {
                if (littleDude.EnemyTarget == null)
                    littleDude.Die();
            }
        }
    }*/
    public void FreighterEnter(int faction, int resource, int amount)
    {

    }

    public void ShipEnter(int faction, int type, int crystal, int ore, int gas)
    {
        //Debug.Log(faction + " " + currentFaction + " " + type);
        if(faction == currentFaction)
        {
           // Debug.Log("same faction");
            //Debug.Log(popLimit + " " + currentBuilt);
            if(popLimit > currentBuilt)
            {
            //Debug.Log("poplimit and currentbuilt ok");
                currentBuilt++;
                switch (type)
                {
                    case 0:
                        DogFighterAmount++;
                        break;
                    case 1:
                        BomberAmount++;
                        break;
                    case 2:
                        FreighterAmount++;
                        CrystalAmount += crystal;
                        OreAmount += ore;
                        GasAmount += gas;
                        break;
                    default:
                        FreighterAmount++;
                        break;
                }
            } else if(type == 2)
            {
                FreighterAmount++;
                CrystalAmount += crystal;
                OreAmount += ore;
                GasAmount += gas;

            }

        } else
        {
            if (currentBuilt <= 0)
            {
                currentBuilt = 0;
                DogFighterAmount = 0;
                BomberAmount = 0;
                FreighterAmount = 0;

                if (Defenders.Count == 0)
                {
                    ChangingFaction = true;
                    if((gm.gameMode == 1)&&(gm.Selected==gameObject))
                        gm.ChangeModeSelect();
                    currentFaction = faction;
                    spts.Attackers.Clear();
                    currentBuilt = 1;
                    switch (type)
                    {
                        case 0:
                            DogFighterAmount++;
                            break;
                        case 1:
                            BomberAmount++;
                            break;
                        case 2:
                            FreighterAmount++;
                            CrystalAmount += crystal;
                            OreAmount += ore;
                            GasAmount += gas;
                            break;
                        default:
                            FreighterAmount++;
                            break;
                    }
                    ChangingFaction = false;
                }
            } else
            {
                switch (type)
                {
                    case 0:
                        if(DogFighterAmount > 0)
                            DogFighterAmount--;
                        else
                        {
                            if (FreighterAmount > 0)
                                FreighterAmount--;
                            else if (BomberAmount > 0)
                                BomberAmount--;
                        }
                        currentBuilt--;
                        break;
                    case 1:
                        if (BomberAmount > 0)
                            BomberAmount--;
                        else
                        {
                            if (DogFighterAmount > 0)
                            {
                                DogFighterAmount -= 2;
                                if (DogFighterAmount < 0)
                                    DogFighterAmount = 0;
                            }
                            else if (FreighterAmount > 0)
                            {
                                FreighterAmount -= 2;
                                if (FreighterAmount < 0)
                                    FreighterAmount = 0;
                            }
                        }
                        currentBuilt--;
                        break;
                    case 2:
                        CrystalAmount += crystal;
                        OreAmount += ore;
                        GasAmount += gas;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    
    void FixedUpdate()
    {
        StartCoroutine(ShipsManagement());
        UpdateResources();
        if (!ChangingFaction)
        {
            UpdateShips();
        }
    }

    public void ChangeConstruction(int i)
    {
        if (i == 4)
        {
            Debug.Log(i + " " + hasDock);
            if (hasDock)
            {
                if (
                            (OreAmount >= gm.CapitalOreCost) &&
                            (GasAmount >= gm.CapitalGasCost) &&
                            (CrystalAmount >= gm.CapitalCrystalCost)
                            )
                {
                    OreAmount -= gm.CapitalOreCost;
                    GasAmount -= gm.CapitalGasCost;
                    CrystalAmount -= gm.CapitalCrystalCost;
                    dockControl.BuildCapitalShip();
                }
            }
            return;
        }
        CurrentBuilding = i;
        constructionTimer = 0f;
    }
    
    public int GetCrystal()
    {
        int toGive = Mathf.FloorToInt(CrystalAmount * (1f / popLimit));
        CrystalAmount -= toGive;
        return toGive;
    }

    public int GetOre()
    {
        int toGive = Mathf.RoundToInt(OreAmount * (1f/popLimit));
        OreAmount -= toGive;
        return toGive;
    }
    public int GetGas()
    {
        int toGive = Mathf.RoundToInt(GasAmount * (1f / popLimit));
        GasAmount -= toGive;
        return toGive;
    }

    public void UpdateShips()
    {
        //ship cost category 1 = 2 ; 2 = 4; 3 = 6
        //ship time category 1 = 2 ; 2 = 3; 3 = 4
        constructionTimer += Time.fixedDeltaTime;
        switch (CurrentBuilding)
        {
            case 0: // dogfighter
                if(constructionTimer > 2f)
                {
                    constructionTimer = 2f;
                    if (
                        (OreAmount >= gm.DogFighterOreCost) &&
                        (GasAmount >= gm.DogFighterGasCost) &&
                        (CrystalAmount >= gm.DogFighterCrystalCost)
                        )
                    {
                        if (currentBuilt + Defenders.Count < popLimit) {
                            OreAmount -= gm.DogFighterOreCost;
                            GasAmount -= gm.DogFighterGasCost;
                            CrystalAmount -= gm.DogFighterCrystalCost;
                            DogFighterAmount += 1;
                            currentBuilt += 1;
                            constructionTimer = 0f;
                        }
                    }
                }
                break;
            case 1: //bomber
                if (constructionTimer > 3f)
                {
                    constructionTimer = 3f;
                    if (
                        (OreAmount >= gm.BomberOreCost) &&
                        (GasAmount >= gm.BomberGasCost) &&
                        (CrystalAmount >= gm.BomberCrystalCost)
                        )
                    {
                        if (currentBuilt + Defenders.Count < popLimit)
                        {
                            OreAmount -= gm.BomberOreCost;
                            GasAmount -= gm.BomberGasCost;
                            CrystalAmount -= gm.BomberCrystalCost;
                            BomberAmount += 1;
                            currentBuilt += 1;
                            constructionTimer = 0f;
                        }
                    }
                }
                break;
            case 2: // Freighter
                if (constructionTimer > 2f)
                {
                    constructionTimer = 2f;
                    if (
                        (OreAmount >= gm.FreighterOreCost) &&
                        (GasAmount >= gm.FreighterGasCost) &&
                        (CrystalAmount >= gm.FreighterCrystalCost)
                        )
                    {
                        if (currentBuilt + Defenders.Count < popLimit)
                        {
                            OreAmount -= gm.FreighterOreCost;
                            GasAmount -= gm.FreighterGasCost;
                            CrystalAmount -= gm.FreighterCrystalCost;
                            FreighterAmount += 1;
                            currentBuilt += 1;
                            constructionTimer = 0f;
                        }
                    }
                }
                break;
            default:
                break;

        }
    }

    IEnumerator ShipsManagement()
    {
        yield return new WaitForSeconds(0.08f);
        Defenders.RemoveAll(item => item == null);
        if (Defenders.Count > 0)
        {

            //Debug.Log("def > 0");
            CheckDefenders();
        }
        yield return null;
    }

    void UpdateResources()
    {
        TimeSinceResources += Time.deltaTime;
        if (TimeSinceResources > 2f)
        {
            TimeSinceResources = 0;
            switch (OreRate)
            {
                case 1:
                    OreAmount += 1;
                    break;
                case 2:
                    OreAmount += 2;
                    break;
                case 3:
                    OreAmount += 3;
                    break;
                default:
                    break;
            }
            switch (GasRate)
            {
                case 1:
                    GasAmount += 1;
                    break;
                case 2:
                    GasAmount += 2;
                    break;
                case 3:
                    GasAmount += 3;
                    break;
                default:
                    break;
            }
            switch (CrystalRate)
            {
                case 1:
                    CrystalAmount += 1;
                    break;
                case 2:
                    CrystalAmount += 2;
                    break;
                case 3:
                    CrystalAmount += 3;
                    break;
                default:
                    break;
            }
        }
    }
}
