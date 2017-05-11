using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SSGShipPool : MonoBehaviour {
    [Header("Rebel Ships")]
    public GameObject RebelDogFighter;
    public GameObject RebelBomber;
    public GameObject RebelFreighter;
    public GameObject RebelCapitalShipPrefab;
    public GameObject RebelSat;
    [Header("Empire Ships")]
    public GameObject EmpireDogFighter;
    public GameObject EmpireBomber;
    public GameObject EmpireFreighter;
    public GameObject EmpireCapitalShipPrefab;
    public GameObject EmpireSat;
    [Header("Config")]
    public GameManager gm;
    public float minShipDistance = 3;
    public float maxShipDistance = 8;
    public int threadsTargeting = 5;
    public List<SSGSateliteControl> SatControls = new List<SSGSateliteControl>();
    List<GameObject> Ships = new List<GameObject>();
    List<GameObject> Satelites = new List<GameObject>();
    List<GameObject> CapitalShips = new List<GameObject>();
    private float TimeSinceCalc;

    enum TargetingTypes {Ship, Capital, Satelite}

    void CalculateTargets(int numThreads)
    {
        for (int i = 0; i < numThreads; i++)
            StartCoroutine(Targets(Ships, i*Ships.Count/numThreads, Ships.Count/numThreads, TargetingTypes.Ship));

        StartCoroutine(Targets(Satelites,0,Satelites.Count, TargetingTypes.Satelite));
        StartCoroutine(TargetCapital(CapitalShips, 0, CapitalShips.Count));
    }

    public int CountShips(int type)
    {
        switch (type)
        {
            case 0:
            case 1:
            case 2:
                int sum = 0;
                for(int i = 0; i < Ships.Count; i++)
                {
                    if (Ships[i].GetComponent<SSGShipIA>().type == type)
                        sum++;
                }
                return sum;
                break;
            case 3:
                return Satelites.Count;
                break;
            case 4:
                return CapitalShips.Count;
                break;
            default:
                break;
        }
        return 0;
    }

    IEnumerator Targets(List<GameObject> List, int index, int amount, TargetingTypes t)
    {
        GameObject auxEt = null;
        int auxF = 0;
        GameObject target = null;
        float min = minShipDistance;
        float max = maxShipDistance;
        float minimalDistance = minShipDistance;
        for(int i = index; (i < index+amount) && (i<List.Count); i++)
        {
            target = null;
            if(List[i].activeSelf == true)
            {
                switch (t)
                {
                    case TargetingTypes.Ship:
                        auxEt = List[i].GetComponent<SSGShipIA>().EnemyTarget;
                        auxF = List[i].GetComponent<SSGShipIA>().faction;
                        break;
                    case TargetingTypes.Satelite:
                        auxEt = List[i].GetComponent<SSGSatelite>().EnemyTarget;
                        auxF = List[i].GetComponent<SSGSatelite>().faction;
                        min *= 2;
                        max *= 2;
                        break;
                }
                if ((auxEt == null) || (Vector3.Distance(auxEt.transform.position, List[i].transform.position) > max))
                {
                    List[i].SendMessage("UnTarget");
                    for (int j = 0; j < Ships.Count; j++)
                    {
                        if (Ships[j].activeSelf == true)
                        {
                            if (Ships[j].tag == "Ship" + ((auxF + 1) % 2).ToString())
                            {
                                float dist = Vector3.Distance(List[i].transform.position, Ships[j].transform.position);
                                if ((dist <= min)&&(dist<=minimalDistance))
                                {
                                    minimalDistance = dist;
                                    target = Ships[j];
                                    //Debug.Log("found good target");
                                }
                            }
                        }
                    }
                    for (int j = 0; j < Satelites.Count; j++)
                    {
                        if (Satelites[j].activeSelf == true)
                        {
                            if (Satelites[j].tag == "Ship" + ((auxF + 1) % 2).ToString())
                            {
                                float dist = Vector3.Distance(List[i].transform.position, Satelites[j].transform.position);
                                if ((dist <= min) && (dist <= minimalDistance))
                                {
                                    minimalDistance = dist;
                                    target = Satelites[j];
                                    //Debug.Log("found good target");
                                }
                            }
                        }
                    }
                    for (int j = 0; j < CapitalShips.Count; j++)
                    {
                        if (CapitalShips[j].activeSelf == true)
                        {
                            if (CapitalShips[j].tag == "Capital" + ((auxF + 1) % 2).ToString())
                            {
                                float dist = Vector3.Distance(List[i].transform.position, CapitalShips[j].transform.position);
                                if ((dist <= min) && (dist <= minimalDistance))
                                {
                                    minimalDistance = dist;
                                    target = CapitalShips[j];
                                    //Debug.Log("found good target");
                                }
                            }
                        }
                    }
                    //Debug.Log("setting target");
                    if (target == null)
                        List[i].SendMessage("UnTarget");
                    else
                        List[i].SendMessage("TargetEnemy", target);
                }

                else if (auxEt.activeSelf == false)
                    List[i].SendMessage("UnTarget");
            }
        }

        yield return null;
    }
    
    IEnumerator TargetCapital(List<GameObject> List, int index, int amount)
    {
        GameObject auxEt = null;
        int auxF = 0;
        GameObject target = null;
        float min = minShipDistance*4;
        float max = maxShipDistance*5;
        float minimalDistance = minShipDistance;
        for (int i = index; (i < index + amount) && (i < List.Count); i++)
        {
            if (List[i].activeSelf == true)
            {
                SSGCapitalShip aux = List[i].GetComponent<SSGCapitalShip>();
                auxF = aux.faction;
                List<GameObject> Targets = new List<GameObject>();
                for (int k = 0; k < 6; k++)
                {
                    if (aux.TargetEnemies.Count < k + 1)
                        auxEt = null;
                    else
                        auxEt = aux.TargetEnemies[k];
                    if ((auxEt == null) || (Vector3.Distance(auxEt.transform.position, List[i].transform.position) > max))
                    {
                        for (int j = 0; j < Ships.Count; j++)
                        {
                            if (Ships[j].activeSelf == true)
                            {
                                if (Ships[j].tag == "Ship" + ((auxF + 1) % 2).ToString())
                                {
                                    float dist = Vector3.Distance(List[i].transform.position, Ships[j].transform.position);
                                    if ((dist <= min) && (dist <= minimalDistance))
                                    {
                                        minimalDistance = dist;
                                        target = Ships[j];
                                        //Debug.Log("found good target");
                                    }
                                }
                            }
                        }
                        for (int j = 0; j < Satelites.Count; j++)
                        {
                            if (Satelites[j].activeSelf == true)
                            {
                                if (Satelites[j].tag == "Ship" + ((auxF + 1) % 2).ToString())
                                {
                                    float dist = Vector3.Distance(List[i].transform.position, Satelites[j].transform.position);
                                    if ((dist <= min) && (dist <= minimalDistance))
                                    {
                                        minimalDistance = dist;
                                        target = Satelites[j];
                                        //Debug.Log("found good target");
                                    }
                                }
                            }
                        }
                        for (int j = 0; j < CapitalShips.Count; j++)
                        {
                            if (CapitalShips[j].activeSelf == true)
                            {
                                if (CapitalShips[j].tag == "Capital" + ((auxF + 1) % 2).ToString())
                                {
                                    float dist = Vector3.Distance(List[i].transform.position, CapitalShips[j].transform.position);
                                    if ((dist <= min) && (dist <= minimalDistance))
                                    {
                                        minimalDistance = dist;
                                        target = CapitalShips[j];
                                        //Debug.Log("found good target");
                                    }
                                }
                            }
                        }
                        if((target!=null)&&(target.activeSelf== true))
                            if (Targets.Count < 6)
                                Targets.Add(target);
                        //Debug.Log("setting target");
                        /*
                        if (target == null)
                            List[i].SendMessage("UnTarget",target);
                        else
                            List[i].SendMessage("TargetEnemy", target);*/
                    }
                    if (Targets.Count > 0)
                        aux.SendMessage("TargetEnemy", Targets);
                    else
                        aux.SendMessage("UnTarget");
                    /*
                    else if (auxEt.activeSelf == false)
                        List[i].SendMessage("UnTarget");*/
                }
            }
        }
        yield return null;
    }



    // Use this for initialization
    void Start () {
        TimeSinceCalc = 0f;
        for(int i = 0; i < SatControls.Count; i++)
        {
            for(int j = 0; j < SatControls[i].DefSats.Count; j++)
            {
                Satelites.Add(SatControls[i].DefSats[j]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        CleanShips();
        TimeSinceCalc += Time.fixedDeltaTime;
        if (TimeSinceCalc > 0.2f)
        {
            TimeSinceCalc = 0f;
            CalculateTargets(threadsTargeting);
        }
    }

    public void CleanShips()
    {
        for(int i = 0; i < Ships.Count; i++)
        {
            if(Ships[i].activeSelf == false)
            {
                GameObject toDie = Ships[i];
                Ships.RemoveAt(i);
                Destroy(toDie);
            }
        }
        for (int i = 0; i < CapitalShips.Count; i++)
        {
            if (CapitalShips[i].activeSelf == false)
            {
                GameObject toDie = CapitalShips[i];
                CapitalShips.RemoveAt(i);
                Destroy(toDie);
            }
        }
    }

    public GameObject SpawnCapitalShip(Transform FriendlyDockArea, int currentFaction, GameObject planet = null)
    { // 0 rebels 1 empire
        GameObject CS = null;
        switch (currentFaction)
        {
            case 0:
                CS = (GameObject)Instantiate(RebelCapitalShipPrefab, FriendlyDockArea.position, FriendlyDockArea.rotation);
                break;
            case 1:
                CS = (GameObject)Instantiate(EmpireCapitalShipPrefab, FriendlyDockArea.position, FriendlyDockArea.rotation);
                break;
            default:
                CS = (GameObject)Instantiate(RebelCapitalShipPrefab, FriendlyDockArea.position, FriendlyDockArea.rotation);
                break;
        }
        
        CS.transform.parent = FriendlyDockArea;
        CS.GetComponent<SSGCapitalShip>().sp = gameObject.GetComponent<SSGShipPool>();
        CS.GetComponent<SSGCapitalShip>().Build(currentFaction, planet);
        CapitalShips.Add(CS);
        return CS;
    }



    public void SpawnTrader(GameObject Origin, GameObject planettarget, int amount)
    {
        if (amount > 0)
            StartCoroutine(SpamTrader(Origin, planettarget, amount));
    }
    public void SpawnDogFighter(GameObject Origin, GameObject planettarget, int amount)
    {
        if(amount > 0)
            StartCoroutine(SpamDogFighter(Origin, planettarget, amount));
    }
    public void SpawnBomber(GameObject Origin, GameObject planettarget, int amount)
    {
        if (amount > 0)
            StartCoroutine(SpamBomber(Origin, planettarget, amount));
    }

    public void SpawnDefenders(GameObject ToDefend, int amount)
    {
        Debug.Log("__"+ToDefend.GetComponent<SSGPlanet>().currentFaction.ToString());
        if (amount > 0)
            StartCoroutine(SpamDefense(ToDefend, amount, ToDefend.GetComponent<SSGPlanet>().currentFaction));
    }

    public void SpawnDefendersCapital(GameObject ToDefend, int amount)
    {
        if (amount > 0)
            StartCoroutine(SpamDefenseCapital(ToDefend, amount, ToDefend.GetComponent<SSGCapitalShip>().faction));
    }

    IEnumerator SpamTrader(GameObject Origin, GameObject planettarget, int amount)
    {
        SSGPlanet OriginPlanet = Origin.GetComponent<SSGPlanet>();
        Vector3 position;
        int i = amount;
        while (i > 0)
        {
            if (OriginPlanet.FreighterAmount > 0)
            {
                position = Origin.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                i--;
                GameObject NewDude = null;
                int f = OriginPlanet.currentFaction;
                switch (f)
                {
                    case 0:
                        NewDude = Instantiate(RebelFreighter);
                        break;
                    case 1:
                        NewDude = Instantiate(EmpireFreighter);
                        break;
                    default:
                        NewDude = Instantiate(RebelFreighter);
                        break;
                }
                NewDude.transform.position = position;
                NewDude.GetComponent<SSGShipIA>().PlanetTarget = planettarget;
                NewDude.GetComponent<SSGShipIA>().faction = f;
                NewDude.tag = "Ship" + f.ToString();
                NewDude.GetComponent<SSGShipIA>().gm = gm;
                NewDude.GetComponent<SSGShipIA>().type = 2;
                NewDude.GetComponent<SSGShipIA>().setResources(OriginPlanet.GetCrystal(), OriginPlanet.GetOre(), OriginPlanet.GetGas());
                Ships.Add(NewDude);
                OriginPlanet.FreighterAmount--;
                OriginPlanet.currentBuilt--;
                yield return new WaitForSeconds(Random.Range(0.2f, 0.6f));
            }
            else
                break;
        }
        yield return null;
    }
    IEnumerator SpamDogFighter(GameObject Origin, GameObject planettarget, int amount)
    {
        SSGPlanet OriginPlanet = Origin.GetComponent<SSGPlanet>();
        Vector3 position;
        int i = amount;
        while (i > 0)
        {
            if (OriginPlanet.DogFighterAmount > 0)
            {
                position = Origin.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                i--;
                GameObject NewDude = null;
                int f = OriginPlanet.currentFaction;
                switch (f)
                {
                    case 0:
                        NewDude = Instantiate(RebelDogFighter);
                        break;
                    case 1:
                        NewDude = Instantiate(EmpireDogFighter);
                        break;
                    default:
                        NewDude = Instantiate(RebelDogFighter);
                        break;
                }
                NewDude.transform.position = position;
                NewDude.GetComponent<SSGShipIA>().PlanetTarget = planettarget;
                NewDude.GetComponent<SSGShipIA>().faction = f;
                NewDude.tag = "Ship" + f.ToString();
                NewDude.GetComponent<SSGShipIA>().gm = gm;
                NewDude.GetComponent<SSGShipIA>().type = 0;
                Ships.Add(NewDude);
                OriginPlanet.DogFighterAmount--;
                OriginPlanet.currentBuilt--;
                yield return new WaitForSeconds(Random.Range(0.2f, 0.6f));
            }
            else
                break;
        }
        yield return null;
    }

    IEnumerator SpamBomber(GameObject Origin, GameObject planettarget, int amount)
    {
        SSGPlanet OriginPlanet = Origin.GetComponent<SSGPlanet>();
        Vector3 position;
        int i = amount;
        while (i > 0)
        {
            position = Origin.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            i--;
            GameObject NewDude = null;
            int f = OriginPlanet.currentFaction;
            switch (f)
            {
                case 0:
                    NewDude = Instantiate(RebelBomber);
                    break;
                case 1:
                    NewDude = Instantiate(EmpireBomber);
                    break;
                default:
                    NewDude = Instantiate(RebelBomber);
                    break;
            }
            NewDude.transform.position = position;
            NewDude.GetComponent<SSGShipIA>().PlanetTarget = planettarget;
            NewDude.GetComponent<SSGShipIA>().faction = f;
            NewDude.tag = "Ship" + f.ToString();
            NewDude.GetComponent<SSGShipIA>().gm = gm;
            NewDude.GetComponent<SSGShipIA>().type = 1;
            Ships.Add(NewDude);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
        yield return null;
    }


    IEnumerator SpamDefenseCapital(GameObject ToDefend, int amount, int f)
    {
        SSGCapitalShip OriginPlanet = ToDefend.GetComponent<SSGCapitalShip>();

        Vector3 position;
        int i = amount;
        while (amount > 0)
        {
            position = ToDefend.transform.position;
            amount--;

            if (OriginPlanet.ShipAmount > 2)
            {
                GameObject NewDude = null;
                Debug.Log(OriginPlanet.faction.ToString() + " -|- " + f);
                switch (OriginPlanet.faction)
                {
                    case 0:
                        NewDude = Instantiate(RebelDogFighter);
                        OriginPlanet.ShipAmount--;
                        break;
                    case 1:
                        NewDude = Instantiate(EmpireDogFighter);
                        OriginPlanet.ShipAmount--;
                        break;
                    default:
                        NewDude = Instantiate(RebelDogFighter);
                        OriginPlanet.ShipAmount--;
                        break;
                }
                NewDude.transform.position = position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                NewDude.GetComponent<SSGShipIA>().Defend(ToDefend);
                NewDude.GetComponent<SSGShipIA>().faction = OriginPlanet.faction;
                NewDude.tag = "Ship" + NewDude.GetComponent<SSGShipIA>().faction.ToString();
                NewDude.GetComponent<SSGShipIA>().gm = gm;
                Ships.Add(NewDude);
                ToDefend.GetComponent<SSGCapitalShip>().AddDefenders(NewDude);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            yield return null;
        }
        yield return null;
    }
    IEnumerator SpamDefense(GameObject ToDefend, int amount, int f)
    {
        SSGPlanet OriginPlanet = ToDefend.GetComponent<SSGPlanet>();

        Vector3 position;
        int i = amount;
        while (amount > 0)
        {
            position = ToDefend.transform.position;
            amount--;

            if (OriginPlanet.DogFighterAmount > 2)
            {
                GameObject NewDude = null;
                Debug.Log(OriginPlanet.currentFaction.ToString()+ " -|- " + f);
                switch (OriginPlanet.currentFaction)
                {
                    case 0:
                        NewDude = Instantiate(RebelDogFighter);
                        OriginPlanet.DogFighterAmount--;
                        OriginPlanet.currentBuilt--;
                        break;
                    case 1:
                        NewDude = Instantiate(EmpireDogFighter);
                        OriginPlanet.DogFighterAmount--;
                        OriginPlanet.currentBuilt--;
                        break;
                    default:
                        NewDude = Instantiate(RebelDogFighter);
                        OriginPlanet.DogFighterAmount--;
                        OriginPlanet.currentBuilt--;
                        break;
                }
                NewDude.transform.position = position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                NewDude.GetComponent<SSGShipIA>().Defend(ToDefend);
                NewDude.GetComponent<SSGShipIA>().faction = OriginPlanet.currentFaction;
                NewDude.tag = "Ship" + NewDude.GetComponent<SSGShipIA>().faction.ToString();
                NewDude.GetComponent<SSGShipIA>().gm = gm;
                Ships.Add(NewDude);
                ToDefend.GetComponent<SSGPlanet>().AddDefenders(NewDude);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            yield return null;
        }
        yield return null;
    }
}
