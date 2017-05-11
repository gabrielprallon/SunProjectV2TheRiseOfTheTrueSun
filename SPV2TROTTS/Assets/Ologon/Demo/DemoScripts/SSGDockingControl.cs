using UnityEngine;
using System.Collections;

public class SSGDockingControl : MonoBehaviour {
    public SSGPlanet me;
    public int currentFaction = -1;
    public float orbitSpeed = 4f;

    public Transform target;

    public Transform FriendlyDockArea;
    public Transform EnemyDockArea;

    public bool friendDocked = false;
    public bool enemyDocked = false;

    public bool orbitFriednly = false;
    public bool orbitEnemy = false;

    public GameManager gm;

    public GameObject IncomingCapitalF = null;
    public GameObject IncomingCapitalE = null;

    [Header("DockingAreas Colors")]
    public SgtRing FriendlyDockAreaOrbit;
    public SgtRing EnemyDockAreaOrbit;
    public Material materialAzul;
    public Material materialVermelho;


    void Awake()
    {

        me = gameObject.GetComponent<SSGPlanet>();

        target = gameObject.transform;
    }

    // Use this for initialization
    void Start ()
    {
        
        //BuildCapitalShip();

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {/*
        if(orbitFriednly)
            FriendlyDockArea.RotateAround(Vector3.zero, Vector3.up, orbitSpeed * Time.fixedDeltaTime);
        if(orbitEnemy)
            EnemyDockArea.RotateAround(Vector3.zero, Vector3.up, orbitSpeed * Time.fixedDeltaTime);
            */
        if (currentFaction != me.currentFaction)
        {
            currentFaction = me.currentFaction;
            if (me.currentFaction == 0)
            {
                FriendlyDockAreaOrbit.Color = new Color(0f, 0.51f, 0.7f, 0.5f);
                EnemyDockAreaOrbit.Color = new Color(0.7f, 0f, 0.07f, 0.5f);
                foreach (Renderer r in FriendlyDockArea.gameObject.GetComponentsInChildren<Renderer>())
                {
                    r.material = materialAzul;
                }
                foreach (Renderer r in EnemyDockArea.gameObject.GetComponentsInChildren<Renderer>())
                {
                    r.material = materialVermelho;
                }
            }
            else
            {
                FriendlyDockAreaOrbit.Color = new Color(0.7f, 0f, 0.07f, 0.5f);
                EnemyDockAreaOrbit.Color = new Color(0f, 0.51f, 0.7f, 0.5f);
                foreach (Renderer r in FriendlyDockArea.gameObject.GetComponentsInChildren<Renderer>())
                {
                    r.material = materialVermelho;
                }
                foreach (Renderer r in EnemyDockArea.gameObject.GetComponentsInChildren<Renderer>())
                {
                    r.material = materialAzul;
                }
            }
        }
    }

    public Transform GetReserve(int faction)
    {
        if (faction == me.currentFaction)
            return FriendlyDockArea;
        else
            return EnemyDockArea;
    }

    public bool ReserveDock(GameObject CapitalShip)
    {

        SSGCapitalShip ssgcs = CapitalShip.GetComponent<SSGCapitalShip>();

        if (CapitalShip == null) Debug.Log("capital");
        if (ssgcs == null) Debug.Log("ssgcs");

        if (ssgcs.faction == me.currentFaction)
        {
            Debug.Log("Dc2");
            if ((!friendDocked)&&(IncomingCapitalF == null))
            {
                Debug.Log("Dc3");
                IncomingCapitalF = CapitalShip;
                //CapitalShip.transform.parent = FriendlyDockArea;
                ssgcs.MoveToPlanet();
                return true;
            }
        }
        else
        {
            Debug.Log("Dc4");
            if ((!enemyDocked)&& (IncomingCapitalE == null))
            {
                Debug.Log("Dc5");
                IncomingCapitalE = CapitalShip;
                //CapitalShip.transform.parent = EnemyDockArea;
                ssgcs.MoveToPlanet();
                return true;
            }
        }
        return false;
    }

    public bool Dock(GameObject CapitalShip)
    {
        Debug.Log("Dc1");
        SSGCapitalShip ssgcs = CapitalShip.GetComponent<SSGCapitalShip>();

        if (ssgcs.faction == me.currentFaction)
        {
            
                Debug.Log("Dc2");
                CapitalShip.transform.parent = FriendlyDockArea;
                friendDocked = true;
                Orbit(CapitalShip);
                return true;
        }
        else
        {
            Debug.Log("Dc3");
            CapitalShip.transform.parent = EnemyDockArea;
            enemyDocked = true;
            Orbit(CapitalShip);
            return true;
        }
        return false;
    }

    public void Orbit(GameObject CapitalShip)
    {
        SSGCapitalShip ssgcs = CapitalShip.GetComponent<SSGCapitalShip>();


        if (ssgcs.faction == me.currentFaction)
        {
            orbitFriednly = true;
            IncomingCapitalF = CapitalShip;
            FriendlyDockArea.GetComponent<SgtSimpleOrbit>().enabled = true;
        }
        else
        {
            orbitEnemy = true;
            IncomingCapitalE = CapitalShip;
            EnemyDockArea.GetComponent<SgtSimpleOrbit>().enabled = true;
        }
    }

    public void UnOrbit(GameObject CapitalShip)
    {
        SSGCapitalShip ssgcs = CapitalShip.GetComponent<SSGCapitalShip>();


        if (ssgcs.faction == me.currentFaction)
        {
            orbitFriednly = false;
            friendDocked = false;
            IncomingCapitalF = null;
            FriendlyDockArea.GetComponent<SgtSimpleOrbit>().enabled = false;
        }
        else
        {
            orbitEnemy = false;
            enemyDocked = false;
            IncomingCapitalE = null;
            EnemyDockArea.GetComponent<SgtSimpleOrbit>().enabled = false;
        }

    }

    public void SendFriendly(GameObject destiny)
    {
        if (IncomingCapitalF != null)
        {
            IncomingCapitalF.GetComponent<SSGCapitalShip>().GoToPlanet(destiny);
        }
    }

    public void BuildCapitalShip()
    {
        Debug.Log(friendDocked);
        if (!friendDocked)
        {
            friendDocked = true;
            Debug.Log(me);
            Debug.Log(me.currentFaction);
            Debug.Log(FriendlyDockArea);
            Debug.Log(gm);
            Debug.Log(gm.sp);
            Debug.Log(gameObject);
            Debug.Log(IncomingCapitalF);
            IncomingCapitalF = gm.sp.SpawnCapitalShip(FriendlyDockArea, me.currentFaction, gameObject);
            if (IncomingCapitalF != null)
            {
                Orbit(IncomingCapitalF);
                IncomingCapitalF.GetComponent<SSGCapitalShip>().sp = gm.sp;
            }
        }
    }
}
