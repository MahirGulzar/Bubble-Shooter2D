using UnityEngine;
using System.Collections;

public class BoundryScaler : MonoBehaviour {

    public Vector3 TabletPosition;
    public Vector2 ColliderOffset;
    public Vector3 TabletScale;


	void Start()
    {

#if UNITY_EDITOR || UNITY_ANDROID || UNITY_STANDALONE_WIN
        if ((Screen.currentResolution.ToString().StartsWith("800") || Screen.currentResolution.ToString().StartsWith("1200")))
        {
            this.transform.position = TabletPosition;
            this.transform.localScale = TabletScale;
            //this.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        }
        //if(Screen.currentResolution.ToString)
        //{

        //}
#endif
    }

}
