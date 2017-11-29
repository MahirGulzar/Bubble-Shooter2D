using UnityEngine;
using System.Collections;

public class Laser_two : MonoBehaviour {


    private float min = 0;
    private float max = 0.8f;
    float t = 0f;

    private LineRenderer LR;
	// Use this for initialization
	void Start () {
        LR = this.GetComponent<LineRenderer>();
	}

    void FixedUpdate()
    {



        this.GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(Mathf.Lerp(max, min, t), 0f);

        t += 0.15f * Time.deltaTime;
        if (t >= 0.8f)
        {
            t = 0;
        }

        //this.GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(Time.time, 0);


    }
}
