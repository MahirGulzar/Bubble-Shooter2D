using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
    
	void Start(){
        
        Invoke("ScaleIn", 0.3f);
        Invoke("ScaleOut", 2.3f);
	}

    void ScaleIn()
    {
        CancelInvoke("ScaleIn");
        iTween.MoveTo(this.gameObject, new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z), 1);
        //iTween.ScaleTo(gameObject,new Vector3(1,1,1), 1.5f);
        
    }

    void ScaleOut()
    {
        CancelInvoke("ScaleOut");
        iTween.MoveTo(gameObject,iTween.Hash("x",10,"easeType","easeInOutExpo"));
    }
}

