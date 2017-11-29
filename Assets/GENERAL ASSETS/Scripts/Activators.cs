using UnityEngine;
using System.Collections;

public class Activators : MonoBehaviour {

    public GameObject Objective;
    public GameObject Level_Failed;
    public GameObject Level_Clear;
    public GameObject Buy_Bubbles;
    public GameObject Buy_PowerUps;
	// Use this for initialization
	void Start () {

        Invoke("ActiveObjective",0.3f);
        
	}

    void ActiveObjective()
    {
        CancelInvoke("ActiveObjective");
        Objective.GetComponent<EnableAnObject>().OnClicked();
        //Invoke("DeactiveObjective", 2.5f);
    }

    public void DeactiveObjective()
    {
        CancelInvoke("DeactiveObjective");
        Objective.GetComponent<EnableAnObject>().OnClicked();
    }



   public void ActiveFailed()
    {
        Level_Failed.GetComponent<EnableAnObject>().OnClicked();
        //Invoke("DeactiveObjective", 2.5f);
    }

   public void ActiveClear()
   {

       Level_Clear.GetComponent<EnableAnObject>().OnClicked();
       //Invoke("DeactiveObjective", 2.5f);
   }

   public void ActiveBuyBubbleStore()
   {

       Buy_Bubbles.GetComponent<EnableAnObject>().OnClicked();
       //Invoke("DeactiveObjective", 2.5f);
   }


   public void ActiveBuyPowerUpStore()
   {

       Buy_PowerUps.GetComponent<EnableAnObject>().OnClicked();
       //Invoke("DeactiveObjective", 2.5f);
   }
	
}
