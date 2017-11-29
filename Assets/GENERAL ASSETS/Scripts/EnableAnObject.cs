using UnityEngine;
using System.Collections;

public class EnableAnObject : MonoBehaviour {
    public GameObject Target,fadeOverlay;
    public bool Act_As_Toggle;
    public bool tweener;
    public bool fader;

    public bool Sound;
    public bool SwipeSound=true;
    public bool timeScaling;

	public void OnClicked()
    {

        if (Sound)
        {
            SoundManager.Instance.ClickPlayButton();
        }
        if(SwipeSound)
        {
            SoundManager.Instance.DragSwipe();
        }
        if(Target.active && Act_As_Toggle)
        {
            if (tweener)
            {
                Target.GetComponent<Tweener>().DisableDeactive();
            }
            else
            {
                Target.SetActive(false);
            }
            if(fader)
            {
                fadeOverlay.SetActive(false);
            }
            if (timeScaling)
            {
                Time.timeScale = 1;
            }
            
        }
        else
        {
            if (fader)
            {
                fadeOverlay.SetActive(true);
            }
            if(timeScaling)
            {
                Time.timeScale = 0;
            }
            Target.SetActive(true);
        }


        
    }
}
