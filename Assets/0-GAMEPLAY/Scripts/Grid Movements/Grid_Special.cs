using UnityEngine;
using System.Collections;

public class Grid_Special : MonoBehaviour
{

    //Currently responsible for grid and border colliders orientation for android
    public bool upcome, grid;
    public Transform Android_Pos;
    int WIDTH;
    float y_threshold;
    public bool group1;
    public bool group2;
    public bool group3;
    void Awake()
    {


#if UNITY_ANDROID || UNITY_STANDALONE_WIN

        this.transform.position = new Vector2(Android_Pos.transform.position.x, Android_Pos.transform.position.y);

        this.transform.localScale = Android_Pos.transform.localScale;


#endif
    }


}

