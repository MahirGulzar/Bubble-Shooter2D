using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager> {


    public AudioSource OneShotAudioSource;

    //BG loops
    public AudioClip BG_Main_Menu;
    public AudioClip BG_Day_Level;
    //AudioClip BG_Main_Menu;



    // One Shots
    public AudioClip PlayButtonSound;
    public AudioClip PlayNext_PreviousSound;
    public AudioClip PlayBuySound;
    public AudioClip PlaySwipeSound;
    public AudioClip PlayCannonShotSound;
    public AudioClip PlayDrumSound1, PlayDrumSound2;
    public AudioClip PlayWallHitSound;
    public AudioClip PlaySparrowSound;
    public AudioClip PlayOneStarSound;
    public AudioClip PlayTwoStarSound;
    public AudioClip PlayThreeStarSound;
    public AudioClip PlayLevelFailedSound;
    public AudioClip PlayLevelCompleteSound;

    public AudioClip PlayCombo1Sound;
    public AudioClip PlayCombo2Sound;
    public AudioClip PlayCombo3Sound;
    public AudioClip PlayCombo4Sound;

    public AudioClip PlayRewardedCoinsSound;
    public AudioClip PlayCoinsOneSound;
    public AudioClip PlayCoinsTwoSound;
    public AudioClip PlayCoinsUsedSound;


    public AudioClip PlayBladeSound;
    public AudioClip PlayFireSound;
    public AudioClip PlayIceSound;

    public AudioClip PlaySpinSound;
    public AudioClip PlaySurpriseSound;
    public AudioClip PlayFallDownTitle;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

   
	// Use this for initialization
	void Start () {

        //if (!GetComponent<AudioSource>().isPlaying)
        //{
        //    GetComponent<AudioSource>().clip = BG_Main_Menu;
        //    GetComponent<AudioSource>().Play();
        //    ClickFallDownTitle();
        //}

	}



    #region OneShots
    public void ClickPlayButton()
    {
        //OneShotAudioSource.PlayOneShot(PlayButtonSound);
    }

    public void ClickNext_PreviousButton()
    {
        //OneShotAudioSource.PlayOneShot(PlayNext_PreviousSound);
    }

    public void ClickBuyButton()
    {
       // OneShotAudioSource.PlayOneShot(PlayBuySound);
    }

    public void DragSwipe()
    {
        //OneShotAudioSource.PlayOneShot(PlaySwipeSound);
    }

    public void ClickCannonShot()
    {
       // OneShotAudioSource.PlayOneShot(PlayCannonShotSound);
    }

    public void DrumShot()
    {
        int rand = Random.Range(0, 4);
        if(rand==0)
        {
           // OneShotAudioSource.PlayOneShot(PlayDrumSound1);
        }
        else
        {
            // OneShotAudioSource.PlayOneShot(PlayDrumSound2);
        }
    }

    public void ClickBirdSound()
    {
        //OneShotAudioSource.PlayOneShot(PlaySparrowSound);
    }


    public void ClickOneStarSound()
    {
        //OneShotAudioSource.PlayOneShot(PlayOneStarSound);
    }
    public void ClickTwoStarSound()
    {
        //OneShotAudioSource.PlayOneShot(PlayTwoStarSound);
    }
    public void ClickThreeStarSound()
    {
        //OneShotAudioSource.PlayOneShot(PlayThreeStarSound);
    }
    public void ClickLevelFailedSound()
    {
        //OneShotAudioSource.PlayOneShot(PlayLevelFailedSound);
    }
    public void ClickLevelCompleteSound()
    {
        this.GetComponent<AudioSource>().volume = 0.02f;
        StartCoroutine("MaxAgain");
        //OneShotAudioSource.PlayOneShot(PlayLevelCompleteSound);
    }


    public void ClickCombo1Sound()
    {
        //OneShotAudioSource.PlayOneShot(PlayCombo1Sound);
    }
    public void ClickCombo2Sound()
    {
       // OneShotAudioSource.PlayOneShot(PlayCombo2Sound);
    }
    public void ClickCombo3Sound()
    {
        //OneShotAudioSource.PlayOneShot(PlayCombo3Sound);
    }
    public void ClickCombo4Sound()
    {
        //OneShotAudioSource.PlayOneShot(PlayCombo4Sound);
    }



    public void ClickRewaredCoinsSound()
    {
        //OneShotAudioSource.PlayOneShot(PlayRewardedCoinsSound);
    }
    public void ClickCoinsOneSound()
    {
       // OneShotAudioSource.PlayOneShot(PlayCoinsOneSound);
    }
    public void ClickCoinsTwoSound()
    {
       // OneShotAudioSource.PlayOneShot(PlayCoinsTwoSound);
    }

    public void ClickCoinsUsedSound()
    {
       // OneShotAudioSource.PlayOneShot(PlayCoinsUsedSound);
    }





    public void ClickBladeSound()
    {
        OneShotAudioSource.PlayOneShot(PlayBladeSound);
    }

    public void ClickFireSound()
    {
        OneShotAudioSource.PlayOneShot(PlayFireSound);
    }

    public void ClickIceSound()
    {
        OneShotAudioSource.PlayOneShot(PlayIceSound);
    }

    public void ClickSpinSound()
    {
        OneShotAudioSource.PlayOneShot(PlaySpinSound);
    }

    public void ClickSurpriseSound()
    {
        this.GetComponent<AudioSource>().volume = 0.02f;
        StartCoroutine("MaxAgain");
        OneShotAudioSource.PlayOneShot(PlaySurpriseSound);
        
    }


    public void ClickFallDownTitle()
    {
        OneShotAudioSource.PlayOneShot(PlayFallDownTitle);
    }

    #endregion
    void OnLevelWasLoaded(int number)
    {
        if(number==0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = BG_Main_Menu;
            GetComponent<AudioSource>().Play();
            ClickFallDownTitle();
        }
        if (number == 2)
        {
            //GetComponent<AudioSource>().Stop();
            //GetComponent<AudioSource>().clip = BG_Day_Level;
            //GetComponent<AudioSource>().Play();
        }
    }


    IEnumerator MaxAgain()
    {
        yield return new WaitForSeconds(2.5f);
        this.GetComponent<AudioSource>().volume = 0.25f;
    }
}
