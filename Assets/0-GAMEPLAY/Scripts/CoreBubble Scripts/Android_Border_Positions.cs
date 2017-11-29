using UnityEngine;
using System.Collections;

public class Android_Border_Positions : MonoBehaviour {

    //Currently responsible for grid and border colliders orientation for android
    public bool upcome;
    public Transform Android_Pos,IOS_Pos;
    void Awake()
    {
#if UNITY_ANDROID
		if(!upcome)
			this.transform.position = new Vector2(Android_Pos.transform.position.x,Android_Pos.transform.position.y);



        this.transform.localScale = Android_Pos.transform.localScale;
#endif


#if UNITY_IOS
        if(IOS_Pos!=null)
        {
        if (!upcome)
            this.transform.position = new Vector2(IOS_Pos.transform.position.x, IOS_Pos.transform.position.y);



        this.transform.localScale = IOS_Pos.transform.localScale;
        }
#endif
    }


  
}
