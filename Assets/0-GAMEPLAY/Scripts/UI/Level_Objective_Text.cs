using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level_Objective_Text : MonoBehaviour {


    public Text Level_Number;
	// Use this for initialization
	void Start () {
        if(Level_Number)
        Level_Number.text = (GamePrefs.CURRENT_LEVEL + 1) + "";
        
	switch(GamePrefs.LEVEL_OBJECTIVE)
    {
        case "clearAll":
            this.GetComponent<Text>().text = "Clear All Bubbles!";
            break;
        case "freeBirds":
            //this.GetComponent<Text>().text = "Free "+GamePrefs.NO_OF_BIRDS+" Birds!";
            break;
        case "topRow":
            this.GetComponent<Text>().text = "Eliminate Top Row!";
            break;
        default:
            break;

    }
	}
	
	
}
