using UnityEngine;
using System.Collections;
using ParticlePlayground;

public class SSGShipIA : MonoBehaviour
{
    [Header("GameManager")]
    public GameManager gm;
    [Header("targets")]
    public GameObject PlanetTarget;
    public GameObject EnemyTarget;
    [Header("Basic info")]
    public float Speed = 6f;
    public float rotationSpeed;
    public int health = 100;
    public int type = 0; // 0 dog, 1 bomber, 2 trade
    public int faction = 0;
    public int damageDone = 2;
    public bool shooting = false;
    public float shootdelay = 0.4f;
    public int range;
    public bool isThereAnyThing = false;

    private bool coward = false;
    private bool rotationConfront = false;

    public bool Defensive = false;
    [Header("Resources")]
    public int crystal = 0;
    public int ore = 0;
    public int gas = 0;

    [Header("Orbits")]
    public Transform objectToOrbit; //Object To Orbit
    public Vector3 orbitAxis = Vector3.up; //Which vector to use for Orbit
    public float orbitRadius = 75.0f; //Orbit Radius
    public float orbitRadiusCorrectionSpeed = 0.5f; //How quickly the object moves to new position
    public float orbitRoationSpeed = 10.0f; //Speed Of Rotation arround object
    public float orbitAlignToDirectionSpeed = 0.5f; //Realign speed to direction of travel

    private Vector3 orbitDesiredPosition;
    private Vector3 previousPosition;
    private Vector3 relativePos;
    private Quaternion rotation;
    private Transform thisTransform;

    [Header("Effects")]
    public GameObject EmpireExplosion;
    private LineRenderer line;


    private RaycastHit hit;

    void OnEnable()
    {
        range = UnityEngine.Random.Range(15, 16);
        rotationSpeed = UnityEngine.Random.Range(0.5f, 4f);
        switch (type)
        {
            case 0:
                Speed = UnityEngine.Random.Range(12f, 16f);
                break;
            case 1:
                Speed = UnityEngine.Random.Range(7.5f, 11f);
                break;
            case 2:
                Speed = UnityEngine.Random.Range(7.5f, 9.5f);
                break;
            default:
                Speed = 15f;
                break;
        }
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                orbitAxis = Vector3.up;
                break;
            case 1:
                orbitAxis = Vector3.right;
                break;
            case 2:
                orbitAxis = Vector3.left;
                break;
            case 3:
                orbitAxis = Vector3.down;
                break;
            default:
                orbitAxis = Vector3.up;
                break;
        }/*
        switch (Random.Range(0, 2))
        {
            case 0:
                orbitRoationSpeed *= -1;
                break;
        }*/
        float cfactor = UnityEngine.Random.Range(0f, 100f);
        if (cfactor > 5f)
            coward = false;
        else
            coward = true;

        cfactor = UnityEngine.Random.Range(0f, 100f);
        if (cfactor > 50f)
            rotationConfront = false;
        else
            rotationConfront = true;
    }

    // Use this for initialization
    void Start ()
    {
        thisTransform = transform;
        line = gameObject.GetComponent<LineRenderer>();
        if(line != null)
            line.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyTarget != null && type != 2)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, EnemyTarget.transform.position);

            if (Distance > gm.sp.maxShipDistance)
            {
                Debug.Log("untargeting");
                EnemyTarget = null;
                shooting = false;
            }

            if (!shooting)
            {
                shooting = true;
                StartCoroutine(Shoot());

            }
            else
            {

                if (Distance > gm.sp.maxShipDistance)
                {
                    Debug.Log("untargeting");
                    EnemyTarget = null;
                    shooting = false;
                }
            }
            //Movement
            // Orbit(EnemyTarget, 4f, 40f);
            if ((Distance > 1 && Distance <= gm.sp.maxShipDistance) && EnemyTarget != null)
            {
                if (!isThereAnyThing)
                {
                    Quaternion newRotation;
                    if (coward)
                    {
                        if (rotationConfront)
                            newRotation = Quaternion.LookRotation(transform.position - EnemyTarget.transform.position, Vector3.forward);
                        else
                            newRotation = Quaternion.LookRotation(transform.position - EnemyTarget.transform.position, Vector3.up);
                    }
                    else
                    {
                        if (rotationConfront)
                            newRotation = Quaternion.LookRotation(EnemyTarget.transform.position - transform.position, Vector3.forward);
                        else
                            newRotation = Quaternion.LookRotation(EnemyTarget.transform.position - transform.position, Vector3.up);
                    }
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
                }
            }
            //Orbit(EnemyTarget);
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            CheckForObstacles();
        }
        else
        {
            if (!Defensive)
            {
                if (PlanetTarget != null)
                {
                    float Distance = Vector3.Distance(gameObject.transform.position, PlanetTarget.transform.position);

                    if (Distance <= orbitRadius + 0.1f)
                    {
                        if (((PlanetTarget.GetComponent<SSGPlanet>().Defenders.Count <= 0) && (PlanetTarget.GetComponent<SSGPlanet>().currentFaction != faction)) || (PlanetTarget.GetComponent<SSGPlanet>().currentFaction == faction))
                        {
                            if (Distance <= 0.5f)
                            {
                                PlanetTarget.GetComponent<SSGPlanet>().ShipEnter(faction, type, crystal, ore, gas);
                                DieNoExplosion();
                            }
                            else
                            {
                                if (!isThereAnyThing)
                                {
                                    Quaternion newRotation = Quaternion.LookRotation(PlanetTarget.transform.position - transform.position, Vector3.up);
                                    //newRotation.z = 0;
                                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
                                    //transform.LookAt(PlanetTarget.transform);
                                }
                                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                                CheckForObstacles();
                            }
                        }
                        else
                        {

                            if (!isThereAnyThing)
                            {
                                Quaternion newRotation = Quaternion.LookRotation(PlanetTarget.transform.position - transform.position, Vector3.up);
                                //newRotation.z = 0;
                                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
                                //transform.LookAt(PlanetTarget.transform);
                            }
                            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                            CheckForObstacles();
                            //Orbit(PlanetTarget);
                            //CheckForObstaclesNoRotation();
                        }
                    }
                    else
                    {
                        if (!isThereAnyThing)
                        {
                            Quaternion newRotation = Quaternion.LookRotation(PlanetTarget.transform.position - transform.position, Vector3.up);
                            //newRotation.z = 0;
                            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
                            //transform.LookAt(PlanetTarget.transform);
                        }
                        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                        CheckForObstacles();
                        //Die();
                    }

                }
            }
            else
            {
                if (PlanetTarget != null)
                {
                    if (PlanetTarget.GetComponent<SSGPlanet>() != null)
                    {
                        if (PlanetTarget.GetComponent<SSGPlanet>().currentFaction != faction)
                            Defensive = false;
                    }
                    float Distance = Vector3.Distance(gameObject.transform.position, PlanetTarget.transform.position);

                    if (Distance <= orbitRadius + 0.1f)
                    {
                        Orbit(PlanetTarget);
                        CheckForObstaclesNoRotation();
                    }
                    else
                    {
                        if (!isThereAnyThing)
                        {
                            Quaternion newRotation = Quaternion.LookRotation(PlanetTarget.transform.position - transform.position, Vector3.up);
                            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
                            //newRotation.z = 0;
                            //transform.LookAt(PlanetTarget.transform);
                        }
                        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                        CheckForObstacles();
                    }

                }
            }
        }

    }
    /*
	// Update is called once per frame
	void Update () {
            if (EnemyTarget != null)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, EnemyTarget.transform.position);
                if (!shooting)
                {
                    shooting = true;
                    StartCoroutine(Shoot());

                }
                else
                {

                    if (Distance > gm.sp.maxShipDistance)
                    {
                        EnemyTarget = null;
                        shooting = false;
                    }
                }
                //Movemetn
                // Orbit(EnemyTarget, 4f, 40f);
                if (Distance > 1 && Distance < gm.sp.maxShipDistance)
                {

                    if (!isThereAnyThing)
                    {
                        Quaternion newRotation;
                        if (coward)
                            newRotation = Quaternion.LookRotation(transform.position - EnemyTarget.transform.position, Vector3.forward);
                        else
                            newRotation = Quaternion.LookRotation(EnemyTarget.transform.position - transform.position, Vector3.forward);

                        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
                    }
                }
                //Orbit(EnemyTarget);
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                CheckForObstacles();
            }
            else
            {
                if (!Defensive)
                {
                    if (PlanetTarget != null)
                    {
                        float Distance = Vector3.Distance(gameObject.transform.position, PlanetTarget.transform.position);

                        if (Distance <= orbitRadius + 0.1f)
                        {
                            if (((PlanetTarget.GetComponent<SSGPlanet>().Defenders.Count <= 0) && (PlanetTarget.GetComponent<SSGPlanet>().currentFaction != faction)) || (PlanetTarget.GetComponent<SSGPlanet>().currentFaction == faction))
                            {
                                if (Distance <= 0.5f)
                                {

                                    PlanetTarget.GetComponent<SSGPlanet>().ShipEnter(faction, type);
                                    DieNoExplosion();

                                }
                                else
                                {

                                    if (!isThereAnyThing)
                                    {
                                        Quaternion newRotation = Quaternion.LookRotation(PlanetTarget.transform.position - transform.position, Vector3.forward);
                                        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
                                    //transform.LookAt(PlanetTarget.transform);
                                    }
                                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                                    CheckForObstacles();
                                }
                            }
                            else
                                Orbit(PlanetTarget);
                        }
                        else
                        {

                            if (!isThereAnyThing)
                            {
                                Quaternion newRotation = Quaternion.LookRotation(PlanetTarget.transform.position - transform.position, Vector3.forward);
                                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
                                //transform.LookAt(PlanetTarget.transform);
                            }
                            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                            CheckForObstacles();
                    }

                    }
                }
                else
                {
                    if (PlanetTarget != null)
                    {
                        float Distance = Vector3.Distance(gameObject.transform.position, PlanetTarget.transform.position);

                        if (Distance <= orbitRadius + 0.1f)
                        {
                            Orbit(PlanetTarget);
                        }
                        else
                        {

                            if (!isThereAnyThing)
                            {
                                Quaternion newRotation = Quaternion.LookRotation(PlanetTarget.transform.position - transform.position, Vector3.forward);
                                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
                                //transform.LookAt(PlanetTarget.transform);
                            }
                            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                            CheckForObstacles();
                        }

                    }
                }
            }
        
    }
    */


    void CheckForObstaclesNoRotation()
    {
        //Checking for any Obstacle in front.
        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = transform;
        Transform rightRay = transform;

        //Use Phyics.RayCast to detect the obstacle

        if (Physics.Raycast(leftRay.position + (transform.right * 2), transform.forward, out hit, range) || Physics.Raycast(rightRay.position - (transform.right * 2), transform.forward, out hit, range))
        {

            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                isThereAnyThing = true;
            }
            else
            {
                isThereAnyThing = false;
            }
        }

        // Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
        // Just making this boolean variable false it means there is nothing in front of object.
        if (Physics.Raycast(transform.position - (transform.forward * 4), transform.right, out hit, range/4) ||
        Physics.Raycast(transform.position - (transform.forward * 4), -transform.right, out hit, range/4))
        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {

                isThereAnyThing = false;
            }
        }

        // Use to debug the Physics.RayCast.
        Debug.DrawRay(transform.position + (transform.right * 1.5f), transform.forward * range, Color.red);

        Debug.DrawRay(transform.position - (transform.right * 1.5f), transform.forward * range, Color.red);

        Debug.DrawRay(transform.position - (transform.forward * 2), -transform.right * range/4, Color.yellow);

        Debug.DrawRay(transform.position - (transform.forward * 2), transform.right * range/4, Color.yellow);

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
            } else
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

    void FixedUpdate()
    {
    }

    public void Orbit(GameObject toOrbit)//, float ORS, float OR)
    {
       // orbitRoationSpeed = ORS;
       // orbitRadius = OR;
        //orbitAxis = Vector3.up;
        objectToOrbit = toOrbit.transform;
        thisTransform.RotateAround(objectToOrbit.position, orbitAxis, orbitRoationSpeed * Time.deltaTime);
        orbitDesiredPosition = (thisTransform.position - objectToOrbit.position).normalized * orbitRadius + objectToOrbit.position;
        thisTransform.position = Vector3.Slerp(thisTransform.position, orbitDesiredPosition, Time.deltaTime * orbitRadiusCorrectionSpeed);

        //Rotation
        
        relativePos = thisTransform.position - previousPosition;
        rotation = Quaternion.LookRotation(relativePos);
        //rotation = Quaternion.LookRotation(orbitDesiredPosition);
        thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, rotation, orbitAlignToDirectionSpeed * Time.deltaTime);
        previousPosition = thisTransform.position;
    }

    public void Defend(GameObject ToDefend)
    {
        Defensive = true;
        transform.rotation = UnityEngine.Random.rotation;
        PlanetTarget = ToDefend;
    }

    public void Recall()
    {
        Defensive = false;
    }

    public void Die()
    {
        EnemyTarget = null;
        shooting = false;
        //PlaygroundC.InstantiatePreset("Fire", "Implode Rebel 1");
        if (EmpireExplosion != null)
        {
            GameObject aux = Instantiate(EmpireExplosion);
            aux.transform.position = transform.position;
        }
        gameObject.SetActive(false);
    }

    public void DieNoExplosion()
    {
        EnemyTarget = null;
        shooting = false;
        gameObject.SetActive(false);
    }

    public bool Damage(int amount)
    {
        health = health - amount;
        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }
    

    public void TargetEnemy (GameObject enemy)
    {
        EnemyTarget = enemy;
    }

    public void UnTarget()
    {
        EnemyTarget = null;
    }
    IEnumerator Shoot()
    {
        Vector3 enemypos = gameObject.transform.position;

        try
        {
            enemypos = EnemyTarget.transform.position;
        }
        catch
        {

        }

        while (shooting && EnemyTarget != null && EnemyTarget.activeSelf == true)
        {
            line.enabled = true;
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, enemypos);
         
               /* if (faction == 0)
                    line.SetColors(Color.blue, Color.cyan);
                else
                    line.SetColors(Color.red, Color.magenta);*/
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.2f));
          
            line.enabled = false;
            if (EnemyTarget != null)
            {
                // pegar loot dos inimigos, geladeira
                /*if (EnemyTarget.GetComponent<SSGShipIA>() != null)
                {
                    if (EnemyTarget.activeSelf)
                    {
                        SSGShipIA enemy = EnemyTarget.GetComponent<SSGShipIA>();
                        if (enemy.type == 2)
                        {
                            if (enemy.Damage(damageDone) == true)
                            {
                                crystal = 
                            }
                        }
                    }
                } else
                {*/
                EnemyTarget.SendMessage("Damage", damageDone, SendMessageOptions.DontRequireReceiver);
                //}
            }
            yield return new WaitForSeconds(shootdelay);
            try {
                enemypos = EnemyTarget.transform.position;
            } catch
            {

            }
        }
        shooting = false;
        yield return null;
    }

    public void setResources(int c, int o, int g)
    {
        crystal = c;
        ore = o;
        gas = g;
    }
}
