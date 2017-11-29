using UnityEngine;
using System.Collections;

public class ObjectiveScript : MonoBehaviour {

    public GameObject[] stars;


    void Start()
    {
        int starsTaken = PlayerPrefs.GetInt("LEVEL_" + GamePrefs.CURRENT_LEVEL + "STARS");
        for(int i=0;i<starsTaken;i++)
        {
            stars[i].SetActive(true);
        }
    }
}
