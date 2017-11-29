using UnityEngine;
using System.Collections;

public class ResetTest : MonoBehaviour {

	// Use this for initialization
	public void OnClick()
    {
        Destroy(GameObject.Find("GameManager"));
        GamePrefs.CURRENT_LEVEL = 2;
        GamePrefs.LEVEL_OBJECTIVE = "";
        //GamePrefs.NO_OF_BIRDS = 0;
        GamePrefs.NO_OF_BUBBLES = 0;
        GamePrefs.NO_OF_SCENE_COLORS = 0;
        for (int i = 0; i < GamePrefs.SCENE_COLORS.Length;i++)
        {
            GamePrefs.SCENE_COLORS[i] = false;
        }
            Application.LoadLevel(1);
    }
}
