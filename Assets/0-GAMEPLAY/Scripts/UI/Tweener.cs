using UnityEngine;
using System.Collections;

public class Tweener : MonoBehaviour {

    public bool MoveFrom;
    public Vector3 MoveFromPos;




    private Vector3 spawnPos;


    void Awake()
    {
        spawnPos = this.transform.position;
    }
    

	void OnEnable()
    {
		iTween.MoveFrom(this.gameObject, iTween.Hash("x", 10, "time", 1, "ignoretimescale", true));
    }


    public void DisableDeactive()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("x", 10, "time", 1, "ignoretimescale", true));

        StartCoroutine(Reset());
    }


    IEnumerator Reset()
    {
        float start = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup<start+1f)
        {
            yield return null;
        }
        //yield return new WaitForSeconds(1f);
        this.transform.position = spawnPos;
        this.gameObject.SetActive(false);

    }

}
