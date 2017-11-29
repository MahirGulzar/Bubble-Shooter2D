using UnityEngine;
using System.Collections;

public class FallingBubbleScript : MonoBehaviour {


    public ColorProperty BubbleColor;
    SpriteRenderer sp;
    GameManager gameManager;


	void OnEnable()
    {
        sp = this.GetComponent<SpriteRenderer>();
        gameManager = GameManager.FindObjectOfType<GameManager>();
        //switch (BubbleColor)
        //{
        //    case ColorProperty.Green:
        //       // sp.sprite = gameManager.sp_bubble[0];
        //        break;
        //    case ColorProperty.Blue:
        //        //sp.sprite = gameManager.sp_bubble[1];
        //        break;
        //    case ColorProperty.Yellow:
        //        //sp.sprite = gameManager.sp_bubble[2];
        //        break;
        //    case ColorProperty.Red:
        //        //sp.sprite = gameManager.sp_bubble[3];
        //        break;
        //    case ColorProperty.Purple:
        //       // sp.sprite = gameManager.sp_bubble[4];
        //        break;
        //    case ColorProperty.Black:
        //       // sp.sprite = gameManager.sp_bubble[5];
        //        break;
        //}

        switch (BubbleColor)
        {
            case ColorProperty.Green:
                //sp.sprite = gameManager.sp_bubble[0];
                sp.color = new Color(0, 1, 0);
                break;
            case ColorProperty.Blue:
                //  sp.sprite = gameManager.sp_bubble[1];
                sp.color = new Color(0, 0, 1);
                break;
            case ColorProperty.Yellow:
                sp.color = new Color(1, 1, 0);
                //sp.sprite = gameManager.sp_bubble[2];
                //
                break;
            case ColorProperty.Red:
                // sp.sprite = gameManager.sp_bubble[3];
                sp.color = new Color(1, 0, 0);
                break;
            case ColorProperty.Purple:
                // sp.sprite = gameManager.sp_bubble[4];
                //sp.color = new Color(0, 1, 0);
                sp.color = new Color32(238, 130, 238, 255);
                break;
            case ColorProperty.Black:
                // sp.sprite = gameManager.sp_bubble[5];
                sp.color = new Color(0, 1, 0);
                break;
        }
        //Invoke("Destroy", 7f);

    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name== "Catcher")
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
