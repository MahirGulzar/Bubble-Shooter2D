using UnityEngine;
using System.Collections;

public class UI_Overlayer : MonoBehaviour {

    public GameObject Target;
    public bool Act_As_Toggle;
    public void OnClicked()
    {
        //SoundManager.Instance.ClickPlayButton();
        if (Target.active && Act_As_Toggle)
        {
            Target.SetActive(false);
        }
        else
        {
            Target.SetActive(true);
        }

    }
}
