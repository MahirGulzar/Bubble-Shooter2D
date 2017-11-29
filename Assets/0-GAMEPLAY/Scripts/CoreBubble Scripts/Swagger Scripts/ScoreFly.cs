using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreFly : MonoBehaviour {

    public int ScoreToFly;
    public Sprite[] sps;


    void OnEnable()
    {
        iTween.FadeTo(this.gameObject, 1, 0.01f);
        float rand = Random.Range(0.7f, 1.2f);
        if(ScoreToFly==5)
        {
            this.GetComponent<SpriteRenderer>().sprite = sps[0];
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = sps[1];
        }
        iTween.MoveBy(this.gameObject, iTween.Hash("y", rand, "speed", 0.7f, "easetype", iTween.EaseType.linear, "ignoretimescale", true));
        //StartCoroutine(Fader());

        //Invoke("Dest", 1.2f);
        StartCoroutine(Dest());

    }


    IEnumerator Dest()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 0.7f)
        {
            yield return null;
        }
        //iTween.FadeTo(this.gameObject, 255f, 0.0001f);
        //this.GetComponent<SpriteRenderer>().color = new Color32(255, 220, 71, 255);
        this.gameObject.SetActive(false);
    }

    void OnDisable()
    {
       // Debug.Log("Disabling");
        
        
        CancelInvoke("Dest");
    }



    //IEnumerator Fader()
    //{
    //    Color col = this.GetComponent<SpriteRenderer>().color;
    //    for (int i = 0; i < 100; i++)
    //    {
    //        Debug.Log(i);
    //        yield return new WaitForSeconds(0.009f);
    //        col.a = col.a - 0.01f;
    //        this.GetComponent<SpriteRenderer>().color = col;
    //        //Debug.Log(this.GetComponent<Text>().color.a);
    //    }
    //    //Debug.Log("Call Reset");

    //    Dest();
       
    //    yield break;
    //}
}
