using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NextBubbleManager : MonoBehaviour {

    GameManager gameManager;
    public GameObject Up_Color;
    public ColorProperty Current_Color;         // to be given to current shooting projectile
    public ColorProperty next_Color;            // next coming color


    public GameObject Current, Upcoming,Flip_Parent;
    public GameObject Current_SplineRoot, Upcoming_SplineRoot;



    public SpriteRenderer upComing1,upComing2;
    private bool flip1, flip2;

    //public bool Same_Color_Generation[]= new bool3

    void Awake()
    {
        gameManager = GameManager.FindObjectOfType<GameManager>();
    }
    void Start()
    {
        Current_Color = GetNextColor();
        next_Color = GetNextColor();
        Current.GetComponent<Current_Cannon_Switcher>().this_switch_color = Current_Color;
        Upcoming.GetComponent<Current_Cannon_Switcher>().this_switch_color = next_Color;
    }

    void OnEnable()
    {
        Up_Color.GetComponent<UpComingColors>().OnFlip += this.flip;

    }

    void OnDisable()
    {
        Up_Color.GetComponent<UpComingColors>().OnFlip -= this.flip;
    }


    #region Color Management

    /**
     * Returns a random color based on the xml provided colors of current level
     **/
    public ColorProperty GetNextColor()
    {
        //int my_rand = Random.Range(0, 3);
        //switch (my_rand)
        //{
        //    case 0:
        //        return ColorProperty.Red;
        //        break;
        //    case 1:
        //        return ColorProperty.Blue;
        //        break;
        //    case 2:
        //        return ColorProperty.Black;
        //        break;

        //    default:
        //        break;
        //}

              // My chuss
        if (gameManager.Remaining_Colors.Count > 0)
        {
            int rand_num = Random.Range(0, gameManager.Remaining_Colors.Count);
            return gameManager.Remaining_Colors[rand_num];
        }
        else
        {
            int[] choosen = new int[GamePrefs.NO_OF_SCENE_COLORS];
            int count = 0;
            for (int i = 0; i < GamePrefs.SCENE_COLORS.Length; i++)
            {
                if (GamePrefs.SCENE_COLORS[i] == true)
                {
                    choosen[count] = i;
                    //print(choosen[count]);
                    count++;
                }
            }
            int rand_num = Random.Range(0, GamePrefs.NO_OF_SCENE_COLORS);

            return (ColorProperty)choosen[rand_num];
        }
    }


    /**
     * Returns a current color on demand of the new bubble which is to be shoot
     * Assigns next color to current color and requests a new next color from GetNextColor()
     **/
    public ColorProperty GetCurrent()
    {
        ColorProperty temp = Current_Color;
        Current_Color = next_Color;
        if (GamePrefs.NO_OF_BUBBLES > 1)
        {
            upComing2.gameObject.GetComponent<UpComingColors>().BubbleCountOneLock = false;
            next_Color = GetNextColor();
            Current.GetComponent<Current_Cannon_Switcher>().this_switch_color = Current_Color;
            Upcoming.GetComponent<Current_Cannon_Switcher>().this_switch_color = next_Color;
            //UpdateUpColor();                        // Update coming color spriteRenderer
            SpriteRenderer sp = Up_Color.GetComponent<SpriteRenderer>();
            switch (next_Color)
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
        else
        {
            //Cannot Flip Code Here
            //.GetComponent<SpriteRenderer>().sprite = null;
            upComing2.gameObject.GetComponent<UpComingColors>().BubbleCountOneLock = true;
            upComing1.sprite = null;
            upComing2.sprite = null;
        }
        //print("Current "+Current_Color);
        //print("Next" + next_Color);
        return Current_Color;
    }


    /**
     * Update colors of the current bubble to shoot just returns the current color to it.
     **/
    public ColorProperty UpdateColorsOfObstacle()          
    {
        return Current_Color;
    }


    /**
     * flip action is called when user switches between bubbles in game
     **/
    public void flip()
    {
        Current.GetComponent<SpriteRenderer>().enabled = true;
        Upcoming.GetComponent<SpriteRenderer>().enabled = true;
        //print("Current Object color before flip" + Current.GetComponent<Current_Cannon_Switcher>().this_switch_color);
        //print("Next Object color before flip" + Upcoming.GetComponent<Current_Cannon_Switcher>().this_switch_color);
        //print("Current Color Before Flip " + Current_Color);
        //print("Next Color Before Flip " + next_Color);
        ColorProperty temp = Current_Color;
        Current_Color = next_Color;
        next_Color = temp;

        Current.GetComponent<Current_Cannon_Switcher>().this_switch_color = next_Color;
        Upcoming.GetComponent<Current_Cannon_Switcher>().this_switch_color = Current_Color;
        //print("Current Color After Flip " + Current_Color);
        //print("Next Color After Flip " + next_Color);



        ///----------------------------------------------------------------------
        ///
        //Current.GetComponent<SplineInterpolator>().Reset();
        //Upcoming.GetComponent<SplineInterpolator>().Reset();

        //Current.GetComponent<SplineController>().enabled = true;
        //Upcoming.GetComponent<SplineController>().enabled = true;

        //Upcoming

        Current.GetComponent<SplineController>().FollowSpline();
        Upcoming.GetComponent<SplineController>().FollowSpline();





        GameObject temp3 = Current_SplineRoot;
        Current_SplineRoot = Upcoming_SplineRoot;
        Upcoming_SplineRoot = temp3;





        StartCoroutine("WaitForSpline");
        
        
        UpdateUpColor();            //update upcoming color
        
    }


    IEnumerator WaitForSpline()
    {
        yield return new WaitForSeconds(0.6f);
        Current.GetComponent<SpriteRenderer>().enabled = false;
        Upcoming.GetComponent<SpriteRenderer>().enabled = false;
        
        
    }


    /**
     * Update UpComing color object's spriterenderer
     **/
    void UpdateUpColor()
    {
        SpriteRenderer sp = Up_Color.GetComponent<SpriteRenderer>();
        switch (next_Color)
        {
            case ColorProperty.Green:
                //sp.sprite = gameManager.sp_bubble[0];
                break;
            case ColorProperty.Blue:
                //sp.sprite = gameManager.sp_bubble[1];
                break;
            case ColorProperty.Yellow:
               // sp.sprite = gameManager.sp_bubble[2];
                break;
            case ColorProperty.Red:
                //sp.sprite = gameManager.sp_bubble[3];
                break;
            case ColorProperty.Purple:
                //sp.sprite = gameManager.sp_bubble[4];
                break;
            case ColorProperty.Black:
               // sp.sprite = gameManager.sp_bubble[5];
                break;
        }
        sp.enabled = false;
        StartCoroutine(WaitToTurnOnRenderer());

    }



    IEnumerator WaitToTurnOnRenderer()
    {
        yield return new WaitForSeconds(0.4f);
        Up_Color.GetComponent<SpriteRenderer>().enabled = true;
    }
    #endregion


}
