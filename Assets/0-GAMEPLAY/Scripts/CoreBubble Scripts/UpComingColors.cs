using UnityEngine;
using System.Collections;
using System;

public class UpComingColors : MonoBehaviour {

    public Action OnFlip;       // On Color switching action
    private bool release_switch_btn=true;
    private bool levelCleared = false;
    public bool BubbleCountOneLock = false;
    public GameManager gameManager;

    public GameObject FlipArrows;


    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnEnable()
    {
        gameManager.OnLevel_Cleared += this.OnLevel_Cleared;
        gameManager.OnGridPlacement += this.OnGridPlacement;
    }
    void OnDisable()
    {
        gameManager.OnLevel_Cleared -= this.OnLevel_Cleared;
        gameManager.OnGridPlacement -= this.OnGridPlacement;
    }

    public void ChangeColor()
    {
        if (release_switch_btn && !levelCleared && !BubbleCountOneLock)
        {
            iTween.RotateAdd(FlipArrows, new Vector3(0, 0, -180), 0.5f);
            release_switch_btn = false;
//			Time.timeScale = 0.2f;
            OnFlip();               // Call action on flip
            StartCoroutine(WaitForRelease());
        }
    }


    IEnumerator WaitForRelease()
    {
        yield return new WaitForSeconds(0.8f);
        release_switch_btn = true;
    }


    void OnLevel_Cleared()
    {
        levelCleared = true;
    }

    void OnGridPlacement()
    {
        release_switch_btn = true;
    }

}
