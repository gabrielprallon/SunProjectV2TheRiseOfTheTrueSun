using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class SSGMapConnect : MonoBehaviour
{
    public bool Empire = false;
    public GameObject[] planets;
    public List<VectorLine> lines = new List<VectorLine>();

    private VectorLine guideLine;
	// Use this for initialization
	void Start ()
    {
        //VectorLine.SetCanvasCamera(Camera.main);
        Vector3 vaux = transform.position;
        vaux.y = 0;
        int flex = vaux.y < transform.position.y ? -1 : 1;
        guideLine = VectorLine.SetRay3D(
                Color.white, transform.position, transform.up * flex * Vector3.Distance(transform.position, vaux)
            );
        VectorLine.SetCamera3D(Camera.main);
        for (int i = 0; i < planets.Length; i++)
        {
            List<Vector3> points = new List<Vector3>();
            points.Add(gameObject.transform.position);
            points.Add(Lerp(gameObject.transform.position, planets[i].transform.position,0.025f));
            points.Add(Lerp(gameObject.transform.position, planets[i].transform.position, 0.5f));
            points.Add(Lerp(gameObject.transform.position, planets[i].transform.position, 0.75f));
            points.Add(planets[i].transform.position);
            
            VectorLine aux = VectorLine.SetRay3D(new Color(9f,192f,255f), gameObject.transform.position, planets[i].transform.position);
            aux.SetWidth(2f);
            //aux.SetColor(Color.blue);
            aux.smoothColor = true;
            aux.points3 = points;
            
            List<Color32> myColors = new List< Color32 > ();
            myColors.Add(Color.clear);
            if (Empire){
                myColors.Add(Color.red);

            } else {
                myColors.Add(new Color32(9, 192, 255, 255));
            }
            if (planets[i].gameObject.GetComponent<SSGMapConnect>().Empire){
                myColors.Add(Color.red);
            } else {
                myColors.Add(new Color32(9, 192, 255, 255));
            }


            //myColors.Add(Color.blue);


            myColors.Add(Color.clear);
            aux.SetColors(myColors);
            lines.Add(aux);
        }
	}

    Vector3 Lerp(Vector3 start, Vector3 end, float percent)
    {
        return (start + percent * (end - start));
    }

    public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }

    // Update is called once per frame
    void Update () {
	    for(int i = 0; i < lines.Count; i++)
        {
            lines[i].Draw3D();
        }
        guideLine.Draw3D();
	}
}

