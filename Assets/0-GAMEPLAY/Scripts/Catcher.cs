using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Catcher : MonoBehaviour {
    public int ScoreToGive;
    public GameObject toShake;
    public GameManager gameManager;
    public Text Score;

    public int remaning_count = 0;

    public ObjectPoolScript DrumPooler;


	void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
        // SoundManager.Instance.DrumShot();
        
        if (other.collider.name.Contains("Wasted"))
            {
                GamePrefs.temp_remaning_projectiles_shot++;
                //print("Remaining Count ");
                //Debug.Log(GamePrefs.temp_remaning_projectiles_shot);
            
            }
            iTween.ShakeScale(toShake, iTween.Hash("x",0.1f,"y",0.1f,"time",0.2f,"ignoretimescale",true));
            other.gameObject.SetActive(false);
            int prev_Score = GamePrefs.lEVEL_SCORE;
            GamePrefs.lEVEL_SCORE += ScoreToGive;
            GameObject temp = DrumPooler.GetPooledObject();
            temp.transform.position = this.transform.position;
            temp.GetComponent<DrumScoreFly>().ScoreToFly = ScoreToGive;
            temp.SetActive(true);
            

    }
}
