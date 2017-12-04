using UnityEngine;
using System.Collections;

public class ForPause : MonoBehaviour {

    public GameObject[] Overlays;
    public GameObject Target;
    public GameObject Overlay;
    
    public void OnClicked()
    {
        int checkothers = 0;

        for(int i=0;i<Overlays.Length;i++)
        {
            if(Overlays[i].active)
            {
                checkothers++;
            }
        }
        if(checkothers<=0)
        {
            if(Target.active)
            {
                Time.timeScale = 1;
                Overlay.SetActive(false);
                Target.GetComponent<Tweener>().DisableDeactive();
            }
            else
            {
                Time.timeScale = 0;
                Overlay.SetActive(true);
                Target.SetActive(true);

            }
        }
        else
        {
            if (Target.active)
            {
                Overlay.SetActive(false);
                Target.GetComponent<Tweener>().DisableDeactive();
            }
            else
            {
                //print("yes");
                Overlay.SetActive(true);
                Target.SetActive(true);
            }
        }
    }

}
