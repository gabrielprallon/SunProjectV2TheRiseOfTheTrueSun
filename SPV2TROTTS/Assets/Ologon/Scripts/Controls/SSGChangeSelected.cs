using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class SSGChangeSelected : MonoBehaviour {
    public GameObject myCamera;
    public SSGMinicameraControl smc;

    public GameManager gm;
    // Use this for initialization
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
        if (gesture.pickedObject == gameObject)
        {
            //smc.ChangeCamera(myCamera);
            //gm.ChangeSelected(gameObject, myCamera);
            //Debug.Log(gesture.pickedObject.transform.parent.gameObject.name);
            //if(gesture.pickedObject.transform.parent.gameObject.GetComponent<SSGPlanet>().currentFaction == 0)
                gm.ChangeSelected(gesture.pickedObject.transform.parent.gameObject, myCamera);
        }
    }

    public void ChangeToPlanet()
    {
        //Debug.Log("Tá chegando a parada, mas tem algo errado");
        gm.ChangeSelected(gameObject.transform.parent.gameObject, myCamera);
    }

}
