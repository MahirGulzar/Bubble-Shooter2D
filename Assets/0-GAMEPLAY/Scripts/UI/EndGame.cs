using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

    public GameObject PanelEnabler;
    public GameManager gameManager;

	public void OnClicked()
    {
        PanelEnabler.GetComponent<Activators>().ActiveBuyBubbleStore();     // Deactive Store beacuse its a toggle
        gameManager.OnLevelFailed();
    }
}
