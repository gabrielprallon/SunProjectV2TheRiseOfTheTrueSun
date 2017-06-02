using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SSGIA : MonoBehaviour {
    [Header("Planets")]
    public List<SSGPlanet> AllPlanets;
    public List<float> Ranks;

    [Header("Utils")]
    public int typeOfAI = 0;
    public float timeSinceLastAttack = 0f;
    public SSGShipPool sp;

    [Header("AI Configs")]
    public float attackWaitPeriod = 5f;
    public int buildPreference = 0;
    public int minViableShips = 10;
    public int minAttackShips = 10;
    public float farDistance = 300f;
    [Header("AI Switchs")]
    public bool attackPlayerFirst = false;
    public bool attackSmallest = true;
    public bool attackSmallestArmy = true;
    public bool attackClosest = true;
    public bool onlyAttackIfSmallerArmy = false;
    public bool autorizeSuicideAttacks = false;
    [Header("AI Weight")]
    public float DistanceW = 1f;
    public float SizeW = 1f;
    public float PlayerW = 10f;
    public float popSizeW = 1f;


    public void RankPlanets(SSGPlanet Attacker)
    {
        Ranks.Clear();
        float aux = 0;
        for(int i = 0; i < AllPlanets.Count; i++) {
            aux = 0;
            if (attackPlayerFirst) {
                switch (AllPlanets[i].currentFaction) {
                    case 0:
                        aux += PlayerW;
                        break;
                    case 1:
                        aux -= 5000;
                        break;
                    default:
                        break;
                }

            } else {
                switch (AllPlanets[i].currentFaction) {
                    case 0:
                        break;
                    case 1:
                        aux -= 5000;
                        break;
                    default:
                        aux += PlayerW;
                        break;
                }
            }
            if (attackSmallest) {
                aux += (4 - AllPlanets[i].planetSize) * SizeW;
            } else {
                aux += AllPlanets[i].planetSize * SizeW;
            }
            if (attackSmallestArmy) {
                aux += (AllPlanets[i].popLimit - AllPlanets[i].currentBuilt) * popSizeW;
            } else {
                aux += AllPlanets[i].currentBuilt * popSizeW;
            }

            if (attackClosest) {
                aux -= Vector3.Distance(AllPlanets[i].transform.position, Attacker.transform.position);
            } else
            {
                aux += Vector3.Distance(AllPlanets[i].transform.position, Attacker.transform.position);
            }

            Ranks.Add(aux);

        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastAttack += Time.deltaTime;
        if(timeSinceLastAttack >= attackWaitPeriod)
        {
            Attack();
        }
	}

    public void Attack()
    {
        SSGPlanet attacker;
        SSGPlanet target;
        if(timeSinceLastAttack >= attackWaitPeriod)
        {
            attacker = findAttackingPlanet();
            if(attacker != null)
            {
                target = findTargetPlanet(attacker);
                if(target != null)
                {
                    timeSinceLastAttack = 0f;
                    if(attacker.DogFighterAmount < minAttackShips)
                    {
                        int dog = attacker.DogFighterAmount;
                        int bomber = minAttackShips - dog;
                        if (bomber > attacker.BomberAmount)
                            bomber = attacker.BomberAmount;
                        sp.SpawnDogFighter(attacker.gameObject, target.gameObject, dog);
                        sp.SpawnBomber(attacker.gameObject, target.gameObject, bomber);
                    } else {
                        sp.SpawnDogFighter(attacker.gameObject, target.gameObject, attacker.DogFighterAmount);
                    }
                }
            }
        }
    }

    public SSGPlanet findAttackingPlanet()
    {
        int auxNave = 0;
        SSGPlanet attacker = null;
        for(int i = 0; i < AllPlanets.Count; i++)
        {
            // procurando planeta com mais naves do imperio
            if((AllPlanets[i].currentFaction == 1) && (AllPlanets[i].DogFighterAmount+ AllPlanets[i].BomberAmount > auxNave))
            {
                auxNave = AllPlanets[i].DogFighterAmount + AllPlanets[i].BomberAmount;
                attacker = AllPlanets[i];
            }
        }
        if(auxNave > minViableShips + minAttackShips)
            return attacker;
        else
            return null;
        
    }

    public SSGPlanet findTargetPlanet(SSGPlanet Attacker)
    {
        RankPlanets(Attacker);
        float aux = Ranks[0];
        int position = 0;
        for (int i = 0; i < Ranks.Count; i++)
        {
            if (Ranks[i] > aux) { aux = Ranks[i]; position = i;}
        }
        if(AllPlanets[position].currentBuilt > Attacker.currentBuilt)
        {
            if (autorizeSuicideAttacks)
                return AllPlanets[position];
            else
                return null;
        }
        return AllPlanets[position];
    }

    public SSGPlanet SearchFarPlanet(SSGPlanet Attacker, int faction, bool menor = true)
    {
        return null;
    }

    public SSGPlanet SearchClosePlanet(SSGPlanet Attacker, int faction, bool menor = true)
    {
        return null;
    }

}
