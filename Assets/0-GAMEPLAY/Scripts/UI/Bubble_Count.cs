using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bubble_Count : MonoBehaviour {

	void Start()
    {

            this.GetComponent<Text>().text = "x" + GamePrefs.NO_OF_BUBBLES;
        
    }

    public void UpdateCount()
    {
        if (GamePrefs.NO_OF_BUBBLES >=0)
        {

            
            this.GetComponent<Text>().text = "x" + GamePrefs.NO_OF_BUBBLES;
            if (GamePrefs.NO_OF_BUBBLES!=0)
            iTween.ShakePosition(gameObject, iTween.Hash("x", 0.1f, "time", 0.1f,"ignoretimescale",true));
        }
    }

    void Update()
    {
        if (GamePrefs.NO_OF_BUBBLES >= 5)
        {
            this.GetComponent<Text>().color = new Color32(251, 234, 0, 255);
        }
        else
        {
            this.GetComponent<Text>().color = Color.red;
        }
        this.GetComponent<Text>().text = "x" + GamePrefs.NO_OF_BUBBLES;
    }
}
