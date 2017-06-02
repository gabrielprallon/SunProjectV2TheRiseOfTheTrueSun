using UnityEngine;
using System.Collections;

public class SSGSatelite : MonoBehaviour {
    public int health = 500;
    public int damage = 7;
    public GameObject ExplodingParticle;
    public bool building = true;
    public float timeToBuild = 30f;
    public GameObject underEmpire;
    public GameObject underRebel;
    public GameObject doneEmpire;
    public GameObject doneRebel;
    public float timeSinceStart;

    public GameObject EnemyTarget = null;

    public bool shooting = false;
    public float shootdelay = 0.4f;

    public int faction = 0;
    private LineRenderer line;
    // Use this for initialization
    void Start () {
        line = gameObject.GetComponent<LineRenderer>();
	}

    void OnEnable()
    {
        health = 500;
        //Build();
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (building)
            Building();
        else
        {
            if (faction == 0)
            {
                underRebel.SetActive(false);
                doneRebel.SetActive(true);
                doneEmpire.SetActive(false);
                underEmpire.SetActive(false);
            }
            else
            {
                underRebel.SetActive(false);
                doneRebel.SetActive(false);
                doneEmpire.SetActive(true);
                underEmpire.SetActive(false);
            }
        }
            
        if (health <= 0)
            die();

        if (EnemyTarget != null)
        {
            if (!shooting)
            {
                shooting = true;
                StartCoroutine(Shoot());

            }
        }
    }

    public void Build(int f)
    {
        faction = f;
        gameObject.tag = "Ship" + f.ToString();
        building = true;
        if (faction == 0)
        {
            underRebel.SetActive(true);
            doneRebel.SetActive(false);
            doneEmpire.SetActive(false);
            underEmpire.SetActive(false);
        } else
        {
            underRebel.SetActive(false);
            doneRebel.SetActive(false);
            doneEmpire.SetActive(false);
            underEmpire.SetActive(true);
        }
        timeSinceStart = 0f;

    }

    void Building()
    {
        if (faction == 0)
        {
            underRebel.SetActive(true);
            doneRebel.SetActive(false);
            doneEmpire.SetActive(false);
            underEmpire.SetActive(false);
        }
        else
        {
            underRebel.SetActive(false);
            doneRebel.SetActive(false);
            doneEmpire.SetActive(false);
            underEmpire.SetActive(true);
        }
        timeSinceStart += Time.fixedDeltaTime;
        if (timeSinceStart > timeToBuild)
        {
            building = false;
            if (faction == 0)
            {
                underRebel.SetActive(false);
                doneRebel.SetActive(true);
                doneEmpire.SetActive(false);
                underEmpire.SetActive(false);
            }
            else
            {
                underRebel.SetActive(false);
                doneRebel.SetActive(false);
                doneEmpire.SetActive(true);
                underEmpire.SetActive(false);
            }
        }
    }

    public void Damage(int amount)
    {
        health -= amount;
    }

    void die()
    {
        if (ExplodingParticle != null)
        {
            Instantiate(ExplodingParticle);
            ExplodingParticle.transform.position = transform.position;
        }
        gameObject.SetActive(false);
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
        Vector3 enemypos = EnemyTarget.transform.position;
        while (shooting && EnemyTarget != null && EnemyTarget.activeSelf == true && !building)
        {
            line.enabled = true;
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, enemypos);
            if (faction == 0)
                line.SetColors(Color.blue, Color.cyan);
            else
                line.SetColors(Color.red, Color.magenta);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 0.4f));
            line.material.mainTextureScale = new Vector2(Vector3.Distance(gameObject.transform.position, enemypos) * 0.1f, 1);
            line.enabled = false;
            if (EnemyTarget != null)
                EnemyTarget.SendMessage("Damage", 2, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(shootdelay);
            try
            {
                enemypos = EnemyTarget.transform.position;
            }
            catch
            {

            }
        }
        shooting = false;
        yield return null;
    }

}
