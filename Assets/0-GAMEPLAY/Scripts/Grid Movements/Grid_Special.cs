using UnityEngine;
using System.Collections;

public class Grid_Special : MonoBehaviour
{

    //Currently responsible for grid and border colliders orientation for android
    public bool upcome, grid;
    public Transform IOS_Pos;
    public Transform Android_Pos;
    int WIDTH;
    float y_threshold;
    public bool group1;
    public bool group2;
    public bool group3;
    void Awake()
    {
#if UNITY_IOS
		if(!upcome)
		{
			WIDTH = (int)Screen.width;
			if (WIDTH == 750 || WIDTH == 1242 || WIDTH==1080 || WIDTH==760 || WIDTH == 1440 || group1){
				y_threshold = 0.38f;
			}

			if (WIDTH == 640 || group2 || group3) {
				if (Screen.height == 960 || group3) {
					y_threshold = 0.30f;
				} else {
					y_threshold = 0.38f;
				}
			}
			if (WIDTH == 1536 || WIDTH == 2048 || WIDTH == 768 || group2) {
				y_threshold = 0.26f;
			}

			this.transform.position = new Vector2(IOS_Pos.transform.position.x,y_threshold);
			print("Threshold = "+y_threshold);
		}
		else
		{
			this.transform.position = new Vector2(IOS_Pos.transform.position.x,IOS_Pos.transform.position.y);
			print("else condition");
		}


		this.transform.localScale = IOS_Pos.transform.localScale;
#endif


#if UNITY_ANDROID

        this.transform.position = new Vector2(Android_Pos.transform.position.x, Android_Pos.transform.position.y);



        this.transform.localScale = Android_Pos.transform.localScale;


#endif
    }


}

