using UnityEngine;
using System.Collections;

public class Current_Cannon_Switcher : MonoBehaviour {

    public ColorProperty this_switch_color;
    GameManager gameManager;
    SpriteRenderer sp;

    //public GameObject Target;



    void Start()
    {
        sp = this.GetComponent<SpriteRenderer>();
        gameManager = GameManager.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        switch (this_switch_color)
        {
            case ColorProperty.Green:
                //sp.sprite = gameManager.sp_bubble[0];
                break;
            case ColorProperty.Blue:
               // sp.sprite = gameManager.sp_bubble[1];
                break;
            case ColorProperty.Yellow:
                //sp.sprite = gameManager.sp_bubble[2];
                break;
            case ColorProperty.Red:
                //sp.sprite = gameManager.sp_bubble[3];
                break;
            case ColorProperty.Purple:
                //sp.sprite = gameManager.sp_bubble[4];
                break;
            case ColorProperty.Black:
                //sp.sprite = gameManager.sp_bubble[5];
                break;
        }




    }
}
