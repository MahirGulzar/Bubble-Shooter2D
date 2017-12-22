using UnityEngine;
using System.Collections;

public class Android_Border_Positions : MonoBehaviour {

    //Currently responsible for grid and border colliders orientation for android
    public bool upcome;
    public Transform Android_Pos;
    void Awake()
    {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
		if(!upcome)
			this.transform.position = new Vector2(Android_Pos.transform.position.x,Android_Pos.transform.position.y);



        this.transform.localScale = Android_Pos.transform.localScale;
#endif



    }



}
