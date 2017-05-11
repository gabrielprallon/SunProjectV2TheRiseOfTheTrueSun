using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Vectrosity;

public class SSGCapitalShip : MonoBehaviour
{
    public SSGShipPool sp;

    public int health = 1200;
    public int damage = 10;
    public bool building = true;
    public float timeToBuild = 120f;
    public GameObject under;
    public GameObject done;
    public GameObject laser;
    private float timeSinceStart;
    
    public List<GameObject> TargetEnemies = new List<GameObject>();
    public int maxTargets = 6;

    public bool shooting = false;
    public float shootdelay = 0.4f;

    public int faction = 0;
    public LineRenderer[] lines;

    public GameObject planetToDock;
    private Transform DockingStation;
    public bool Docked = true;
    public bool moving = false;

    public float distanceToRotate = 3f;
    public float moveSpeed = 8f;
    public float rotationSpeed = 5f;

    public float maxTargetingDistance = 8f;

    public GameObject PlanetTest;
    public bool goToTest = false;

    private bool fire0 = false;
    private bool fire1 = false;
    private bool fire2 = false;
    private bool fire3 = false;
    private bool fire4 = false;
    private bool fire5 = false;

    [Header("Obstacles")]
    public int range;
    public bool isThereAnyThing = false;
    private RaycastHit hit;

    [Header("Ship Production")]
    public int ShipMax = 10;
    public int ShipAmount = 10;
    public float ShipProductionTime = 0f;
    public int ShipsPerTime = 0;
    public bool CanBuild = false;
    private float timeSinceLastBuild = 0f;

    [Header("Defenders")]
    public List<GameObject> Defenders = new List<GameObject>();
    public List<GameObject> Attackers = new List<GameObject>();
    public SSGCapitalDefenseSystem spts;

    // Use this for initialization
    void Start () {
        VectorLine.SetCanvasCamera(Camera.main);
        VectorLine.SetCamera3D(Camera.main);
        VectorManager.useDraw3D = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (building)
            Building();
        if (CanBuild)
        {
            timeSinceLastBuild += Time.deltaTime;

            if ((timeSinceLastBuild > ShipProductionTime) && (ShipAmount < ShipMax))
            {
                ShipAmount++;
                timeSinceLastBuild = 0f;
            }
        }
	}

    void FixedUpdate()
    {
        Disengage();
        if (moving)
            Movement();
        Shoot();
        if (health <= 0)
            die();
        if (goToTest)
        {
            GoToPlanet(PlanetTest);
            goToTest = false;
        }
        Attackers.RemoveAll(item => item == null);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship" + ((faction + 1) % 2).ToString())
        {
            Attackers.Add(other.gameObject);
            
            sp.SpawnDefendersCapital(gameObject, 1);
           
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ship" + ((faction + 1) % 2).ToString())
        {
            Attackers.Remove(other.gameObject);
        }

    }

    public void Movement()
    {
        float dist = Vector3.Distance(transform.position, DockingStation.position);
        /* if ( dist <= distanceToRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.parent.rotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {

            Quaternion targetRotation = Quaternion.LookRotation(transform.parent.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }*//*
        if ((transform.localPosition.Equals(Vector3.zero)) && (transform.rotation.Equals(transform.parent.rotation)))
            moving = false;*/
        if (dist > 1f)
        {
            if (!isThereAnyThing)
            {
                Quaternion newRotation = Quaternion.LookRotation(DockingStation.position - transform.position, Vector3.up);
                //newRotation.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
                //transform.LookAt(PlanetTarget.transform);
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            CheckForObstacles();
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, DockingStation.position, moveSpeed * Time.fixedDeltaTime);
        }
        if (dist < 0.08f)
        //if (transform.localPosition.Equals(Vector3.zero))
        {
            moving = false;
            //Orbit();
            Dock();
        }
    }
    void CheckForObstacles()
    {
        //Checking for any Obstacle in front.
        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = transform;
        Transform rightRay = transform;

        //Use Phyics.RayCast to detect the obstacle

        if (Physics.Raycast(leftRay.position + (transform.right * 1.5f), transform.forward, out hit, range) || Physics.Raycast(rightRay.position - (transform.right * 1.5f), transform.forward, out hit, range))
        {

            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                isThereAnyThing = true;
                transform.Rotate(Vector3.up * Time.deltaTime * 50);
            }
            else
            {
                isThereAnyThing = false;
            }
        }

        // Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
        // Just making this boolean variable false it means there is nothing in front of object.
        if (Physics.Raycast(transform.position - (transform.forward * 2), transform.right, out hit, range / 4) ||
        Physics.Raycast(transform.position - (transform.forward * 2), -transform.right, out hit, range / 4))
        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {

                isThereAnyThing = false;
            }
        }

        // Use to debug the Physics.RayCast.
        Debug.DrawRay(transform.position + (transform.right * 1.5f), transform.forward * range, Color.red);

        Debug.DrawRay(transform.position - (transform.right * 1.5f), transform.forward * range, Color.red);

        Debug.DrawRay(transform.position - (transform.forward * 2), -transform.right * range / 4, Color.yellow);

        Debug.DrawRay(transform.position - (transform.forward * 2), transform.right * range / 4, Color.yellow);

    }

    public void Disengage()
    {
        if(TargetEnemies.Count > 0)
        {
            for(int i = 0; i < TargetEnemies.Count; i++)
            {
                if (TargetEnemies[i] != null)
                {
                    float Distance = Vector3.Distance(gameObject.transform.position, TargetEnemies[i].transform.position);
                    Debug.Log("untargeting? " + i + " " + Distance);
                    if (Distance > maxTargetingDistance)
                    {
                        Debug.Log("untargeting " + i);
                        readyCannon(i);
                        TargetEnemies.RemoveAt(i);

                    }
                }

            }
            TargetEnemies.RemoveAll(item => item == null);
            if (TargetEnemies.Count <= 0) UnTarget();
        }
    }

    void OnEnable()
    {
        health = 1200;
    }

    public void GoToPlanet(GameObject newPlanet)
    {
        if (!building)
        {
            Debug.Log("indo pro planeta");
            //UnDock();
            //planetToDock = ;
            Debug.Log("Dock1");
            SSGDockingControl dc = newPlanet.GetComponent<SSGDockingControl>();
            if (dc != null)
            {
                Debug.Log("Dock2");
                if (dc.ReserveDock(gameObject))
                {
                    Debug.Log("Dock3");
                    UnDock();
                    planetToDock = newPlanet;
                    DockingStation = dc.GetReserve(faction);
                    MoveToPlanet();
                }
            }
        }
    }
    public void MoveToPlanet()
    {
        Debug.Log("move planeta");
        moving = true;
    }

    public void Dock()
    {
        SSGDockingControl dc = planetToDock.GetComponent<SSGDockingControl>();
        if (dc != null)
        {
            dc.Dock(gameObject);
        }
    }
    public void Orbit()
    {
        SSGDockingControl dc = planetToDock.GetComponent<SSGDockingControl>();
        if (dc != null)
            dc.Orbit(gameObject);
    }

    public void UnDock()
    {
        if (transform.parent != null)
        {
            planetToDock.GetComponent<SSGDockingControl>().UnOrbit(gameObject);
            transform.parent = null;
        }
    }

    public void Build(int f, GameObject planet)
    {
        faction = f;
        gameObject.tag = "Capital" + f.ToString();
        building = true;
        done.SetActive(false);
        under.SetActive(true);
        timeSinceStart = 0f;
        planetToDock = planet;
    }

    public void Building()
    {
        timeSinceStart += Time.deltaTime;
        if (timeSinceStart > timeToBuild)
        {
            building = false;
            done.SetActive(true);
            under.SetActive(false);
        }

    }

    public void TargetEnemy(List<GameObject> enemies)    {
        TargetEnemies = enemies;
        while (TargetEnemies.Count > maxTargets)
            TargetEnemies.RemoveAt(0);
        TargetEnemies.TrimExcess();
        shooting = true;
    }

    public void UnTarget()    {
        TargetEnemies.Clear();
        shooting = false;
        readyCannon(0);
        readyCannon(1);
        readyCannon(2);
        readyCannon(3);
        readyCannon(4);
        readyCannon(5);
    }

    public void Damage(int amount)    {
        health -= amount;
    }

    void die()    {
        gameObject.SetActive(false);
    }

    public void Shoot()    {
        shooting = true;
        if (!fire0)
            StartCoroutine(Fire(0));
        if (!fire1)
            StartCoroutine(Fire(1));
        if (!fire2)
            StartCoroutine(Fire(2));
        if (!fire3)
            StartCoroutine(Fire(3));
        if (!fire4)
            StartCoroutine(Fire(4));
        if (!fire5)
            StartCoroutine(Fire(5));
    }
    bool checkCannon(int ti)    {
        switch (ti)        {
            case 1:
                return fire1;
            case 2:
                return fire2;
            case 3:
                return fire3;
            case 4:
                return fire4;
            case 5:
                return fire5;
            case 0:
                return fire0;
            default:
                return true;
        }
        return true;
    }
    void fireCannon(int ti)    {
        switch (ti)        {
            case 1:
                fire1 = true;
                break;
            case 2:
                fire2 = true;
                break;
            case 3:
                fire3 = true;
                break;
            case 4:
                fire4 = true;
                break;
            case 5:
                fire5 = true;
                break;
            case 0:
                fire0 = true;
                break;
            default:
                break;
        }
    }
    void readyCannon(int ti)
    {
        switch (ti)
        {
            case 1:
                fire1 = false;
                break;
            case 2:
                fire2 = false;
                break;
            case 3:
                fire3 = false;
                break;
            case 4:
                fire4 = false;
                break;
            case 5:
                fire5 = false;
                break;
            case 0:
                fire0 = false;
                break;
            default:
                break;
        }
    }
    IEnumerator Fire(int ti)
    {
        yield return new WaitForSeconds(0.3f);
        if (shooting && !checkCannon(ti) && TargetEnemies.Count > 0 && !building)
        {
            fireCannon(ti);
            Vector3 epos = Vector3.zero;
            try
            {
                epos = TargetEnemies[ti].transform.TransformPoint(Vector3.zero) ;//InverseTransformPoint(transform.position);
            }
            catch {  }
            //yield return new WaitForSeconds(shootdelay);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.07f, 0.3f));
            //List<Vector3> points = new List<Vector3>();
            //points.Add(transform.position);
            //points.Add(epos);
            //VectorLine laser = new VectorLine("3D" + ti, points, 3f);
            //laser.drawTransform = transform;
            //try
            //{
            if (epos != Vector3.zero)
            {
                List<Color32> colors = new List<Color32>();

                if (faction == 0)
                {
                    colors.Add(Color.blue);
                    colors.Add(Color.cyan);
                }
                else
                {
                    colors.Add(Color.red);
                    colors.Add(Color.magenta);
                }
                // laser.SetColors(colors);
                // laser.Draw3DAuto();

                float timeRand = UnityEngine.Random.Range(0.2f, 0.4f);
                //VectorLine.SetRay3D(colors[0], timeRand, points[0], points[1]);
                //}
                //catch { }
                GameObject l = Instantiate(laser);
                l.transform.position = transform.position;
                //Debug.Log("Positions:" + transform.position + " | " + epos);
                l.GetComponent<CapitalShipLaser>().init(colors[0], colors[1], transform.TransformPoint(Vector3.zero), epos, timeRand);
                
                yield return new WaitForSeconds(timeRand);
                // laser.StopDrawing3DAuto();
                //VectorLine.Destroy(ref laser);
                try
                {
                    if (TargetEnemies[ti] != null)
                        TargetEnemies[ti].SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
                }
                catch { }
            }
            readyCannon(ti);
        }
        yield return null;
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
       
        spts.Attackers.RemoveAll(item => item == null);
        if ((spts.Attackers.Count > Defenders.Count) && (ShipAmount >= 4))
            sp.SpawnDefenders(gameObject, spts.Attackers.Count - Defenders.Count);
        
    }

    public void RecallDefenders()
    {
        for (int i = 0; i < Defenders.Count; i++)
            Defenders[i].GetComponent<SSGShipIA>().Recall();
    }
}
