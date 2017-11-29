using UnityEngine;
using System.Collections;

public class ObjectsToggleForCoins : MonoBehaviour {

    public GameObject toActive,toDeactive;

	public void OnClicked()
    {
        SoundManager.Instance.ClickPlayButton();
        toActive.SetActive(true);
        toDeactive.SetActive(false);
        this.transform.parent.gameObject.GetComponent<EnableAnObject>().OnClicked();
    }
}
