using UnityEngine;
using System.Collections;

public class ObjectsToggleForCoins : MonoBehaviour {

    public GameObject toActive,toDeactive;

	public void OnClicked()
    {
        toActive.SetActive(true);
        toDeactive.SetActive(false);
        this.transform.parent.gameObject.GetComponent<EnableAnObject>().OnClicked();
    }
}
