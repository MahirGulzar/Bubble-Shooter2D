using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CalculateStars : MonoBehaviour {

    public GameManager gameManager;


    public GameObject[] stars;

	void OnEnable()
    {

        //gameManager.IncreamentLevelClearScore(GamePrefs.lEVEL_SCORE);

        //Invoke("ShowStars", 1f);

       
        
    }




    void ShowStars()
    {
        int margin = 0;
        if (GamePrefs.CURRENT_LEVEL <= 9)
        {
            margin = 0;
        }
        else if (GamePrefs.CURRENT_LEVEL <= 19)
        {
            margin = 8;
        }
        else if (GamePrefs.CURRENT_LEVEL <= 49)
        {

            if (GamePrefs.temp_no_of_bubbles >= 70)
            {
                margin = 20;
            }

            if (GamePrefs.temp_no_of_bubbles >= 60)
            {
                margin = 15;
            }
            if (GamePrefs.temp_no_of_bubbles >= 40)
            {
                margin = 12;
            }
            else if (GamePrefs.temp_no_of_bubbles >= 30)
            {
                margin = 10;
            }
            else
            {
                margin = 5;
            }
        }
        float percentage = ((((float)GamePrefs.temp_no_of_bubbles_remaining + (float)margin) / (float)GamePrefs.temp_no_of_bubbles)) * 100f;

        //print("Percentage " + Mathf.RoundToInt(percentage));



        if (percentage >= 40)
        {
            //this.GetComponent<Image>().sprite=gameManager.sp_stars[2];
            StartCoroutine(ActiveStar(stars[0], 0.8f));
            StartCoroutine(ActiveStar(stars[1], 1.5f));
            StartCoroutine(ActiveStar(stars[2], 2.5f));

            if (PlayerPrefs.GetInt("LEVEL_" + GamePrefs.CURRENT_LEVEL + "STARS") < 3)
            {
                PlayerPrefs.SetInt("LEVEL_" + GamePrefs.CURRENT_LEVEL + "STARS", 3);
                StartCoroutine("IncreaseCoins");
            }
        }
        else if (percentage >= 30)
        {
            //this.GetComponent<Image>().sprite = gameManager.sp_stars[1];
            StartCoroutine(ActiveStar(stars[0], 0.8f));
            StartCoroutine(ActiveStar(stars[1], 1.5f));
            if (PlayerPrefs.GetInt("LEVEL_" + GamePrefs.CURRENT_LEVEL + "STARS") <= 2)
            {
                PlayerPrefs.SetInt("LEVEL_" + GamePrefs.CURRENT_LEVEL + "STARS", 2);
            }
        }
        else if (percentage < 30)
        {
            //this.GetComponent<Image>().sprite = gameManager.sp_stars[0];
            StartCoroutine(ActiveStar(stars[0], 0.8f));
            if (PlayerPrefs.GetInt("LEVEL_" + GamePrefs.CURRENT_LEVEL + "STARS") >= 1)
            {
                //
            }
            else
            {
                PlayerPrefs.SetInt("LEVEL_" + GamePrefs.CURRENT_LEVEL + "STARS", 1);
            }
        }

        // Number Of Unlocked Levels

        if (PlayerPrefs.GetInt("LEVELS_UNLOCKED") <= GamePrefs.CURRENT_LEVEL)
        {
            if (PlayerPrefs.GetInt("LEVELS_UNLOCKED") < 49)
            {
                PlayerPrefs.SetInt("LEVELS_UNLOCKED", GamePrefs.CURRENT_LEVEL + 1);
            }
        }
        PlayerPrefs.Save();
        if(PlayerPrefs.GetInt("LEVELS_UNLOCKED")==50)
        {
            PlayerPrefs.SetInt("LEVELS_UNLOCKED", 49);
        }
            PlayerPrefs.Save();
    }


    IEnumerator ActiveStar(GameObject star, float time)
    {
        
        yield return new WaitForSeconds(time);
        if (star == stars[0])
        {
            SoundManager.Instance.ClickOneStarSound();
        }
        else if (star == stars[1])
        {
            SoundManager.Instance.ClickTwoStarSound();
        }
        else if (star == stars[2])
        {
            SoundManager.Instance.ClickThreeStarSound();
        }
        star.SetActive(true);
    }


    IEnumerator IncreaseCoins()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 1f)
        {
            yield return null;
        }
        
            PlayerPrefs.SetInt("NO_OF_COINS", PlayerPrefs.GetInt("NO_OF_COINS") + 5);
            PlayerPrefs.Save();
            

        
    }
}
