using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Obstacle : MonoBehaviour {
    
    [SerializeField]
    ColorProperty BubbleColor;

    [SerializeField]
    bool atRest=false;

    private GameManager gameManager;
    private SpriteRenderer sp;
    private Dictionary<float, Transform> nearestDistance;


    public Transform Android_Pos;
    public GameObject BubblePop_Animation;
 



    // temps

    int prev_Score;
    void Awake()
    {
    gameManager = GameManager.FindObjectOfType<GameManager>();

    #if UNITY_ANDROID
    this.transform.localScale = Android_Pos.transform.localScale;
    #endif

    }

    void Start()
    {

        StartCoroutine(ColorPopulation());

    }

    IEnumerator ColorPopulation()
    {
        yield return new WaitForSeconds(0.1f);
        sp = this.GetComponent<SpriteRenderer>();
        this.BubbleColor = GameObject.FindObjectOfType<NextBubbleManager>().GetCurrent();
        switch (this.BubbleColor)
        {
            case ColorProperty.Green:
                // sp.sprite = gameManager.sp_bubble[0];
                sp.color = new Color(0, 1, 0);
                break;
            case ColorProperty.Blue:
                // sp.sprite = gameManager.sp_bubble[1];
                sp.color = new Color(0, 0, 1);
                break;
            case ColorProperty.Yellow:
                // sp.sprite = gameManager.sp_bubble[2];
                sp.color = new Color(1, 1, 0);
                break;
            case ColorProperty.Red:
                //sp.sprite = gameManager.sp_bubble[3];
                sp.color = new Color(1, 0, 0);
                break;
            case ColorProperty.Purple:
                // sp.sprite = gameManager.sp_bubble[4];
                sp.color = new Color32(238, 130, 238, 255);
                break;
            case ColorProperty.Black:
                // sp.sprite = gameManager.sp_bubble[5];
                sp.color = new Color(0, 1, 0);
                break;
        }

    }

 

    #region Action functions assignment 
    void OnEnable()
    {
        GameObject.FindObjectOfType<UpComingColors>().OnFlip += UpdateColorsOnFlip;
        gameManager.OnGridPlacement += this.OnGridPlacement;
        gameManager.OnTouchDown += this.OnTouchDown;
    }

    void OnDisable()
    {
        try
        {
            GameObject.FindObjectOfType<UpComingColors>().OnFlip -= UpdateColorsOnFlip;
        }
        catch(System.Exception e)
        {
            //
        }
        gameManager.OnGridPlacement -= this.OnGridPlacement;
        gameManager.OnTouchDown -= this.OnTouchDown;
    }

    void OnGridPlacement()
    {
        //
    }
    void OnTouchDown()
    {
        //this.inStack = false;
    }
   
    #endregion

    IEnumerator WaitForChange()
    {
        yield return new WaitForSeconds(0.1f);
        this.BubbleColor = GameObject.FindObjectOfType<NextBubbleManager>().UpdateColorsOfObstacle();
        switch (this.BubbleColor)
        {
            case ColorProperty.Green:
                //sp.sprite = gameManager.sp_bubble[0];
                break;
            case ColorProperty.Blue:
                //sp.sprite = gameManager.sp_bubble[1];
                break;
            case ColorProperty.Yellow:
                //sp.sprite = gameManager.sp_bubble[2];
                break;
            case ColorProperty.Red:
                //sp.sprite = gameManager.sp_bubble[3];
                break;
            case ColorProperty.Purple:
               // sp.sprite = gameManager.sp_bubble[4];
                break;
            case ColorProperty.Black:
                //sp.sprite = gameManager.sp_bubble[5];
                break;
        }
        yield return new WaitForSeconds(0.4f);

        sp.enabled = true;                  // Test

    }
    void UpdateColorsOnFlip()
    {
        sp.enabled = false;                 // Test

        StartCoroutine(WaitForChange());
    }


   

    #region Collision with grid bubble or top border and fusing current bubble in grid
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (!atRest)
        {
            if (other.gameObject.tag == "TopBorder" || other.gameObject.tag == "BubbleTag")
            {

                

                    this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    this.gameObject.GetComponent<Collider2D>().isTrigger = true;

                    if (other.gameObject.tag == "TopBorder")
                    {
                        nearestDistance = new Dictionary<float, Transform>();
                        List<float> ForSort = new List<float>();
                        for (int i = 0; i < 10; i++)
                        {
                            if(gameManager.MatrixGrid[0][i].transform.gameObject.GetComponent<BubbleProperties>().isEmpty)
                            {
                             nearestDistance.Add(Vector3.Distance(gameManager.MatrixGrid[0][i].position, this.transform.position), gameManager.MatrixGrid[0][i]);
                            
                                ForSort.Add(Vector3.Distance(gameManager.MatrixGrid[0][i].position, this.transform.position));
                            }
                        }

                        
                        ForSort.Sort();
                        this.transform.position = nearestDistance[ForSort[0]].position;
                        this.GetComponent<CircleCollider2D>().enabled = false;
                       
                            FuseInGrid(ForSort[0]);
                        
                        


                    }
                    if (other.gameObject.tag == "BubbleTag")
                    {
                        BubbleProperties currentBubble = other.gameObject.GetComponent<BubbleProperties>();

                        nearestDistance = new Dictionary<float, Transform>();
                        List<float> ForSort = new List<float>();
                        try
                        {
                            if (currentBubble.Top_Left.GetComponent<BubbleProperties>().isEmpty)
                            {
                                nearestDistance.Add(Vector3.Distance(currentBubble.Top_Left.position, this.transform.position), currentBubble.Top_Left);
                                ForSort.Add(Vector3.Distance(currentBubble.Top_Left.position, this.transform.position));
                            }
                        }
                        catch (System.Exception e)
                        {

                        }
                        try
                        {
                            if (currentBubble.Top_Right.GetComponent<BubbleProperties>().isEmpty)
                            {
                                nearestDistance.Add(Vector3.Distance(currentBubble.Top_Right.position, this.transform.position), currentBubble.Top_Right);
                                ForSort.Add(Vector3.Distance(currentBubble.Top_Right.position, this.transform.position));
                            }
                        }
                        catch (System.Exception e)
                        {

                        }
                        try
                        {
                            if (currentBubble.Left.GetComponent<BubbleProperties>().isEmpty)
                            {
                                nearestDistance.Add(Vector3.Distance(currentBubble.Left.position, this.transform.position), currentBubble.Left);
                                ForSort.Add(Vector3.Distance(currentBubble.Left.position, this.transform.position));
                            }
                        }
                        catch (System.Exception e)
                        {

                        }
                        try
                        {
                            if (currentBubble.Right.GetComponent<BubbleProperties>().isEmpty)
                            {
                                nearestDistance.Add(Vector3.Distance(currentBubble.Right.position, this.transform.position), currentBubble.Right);
                                ForSort.Add(Vector3.Distance(currentBubble.Right.position, this.transform.position));
                            }
                        }
                        catch (System.Exception e)
                        {

                        }
                        try
                        {
                            if (currentBubble.Bottom_Left.GetComponent<BubbleProperties>().isEmpty)
                            {
                                nearestDistance.Add(Vector3.Distance(currentBubble.Bottom_Left.position, this.transform.position), currentBubble.Bottom_Left);
                                ForSort.Add(Vector3.Distance(currentBubble.Bottom_Left.position, this.transform.position));
                            }
                        }
                        catch (System.Exception e)
                        {

                        }
                        try
                        {
                            if (currentBubble.Bottom_Right.GetComponent<BubbleProperties>().isEmpty)
                            {
                                nearestDistance.Add(Vector3.Distance(currentBubble.Bottom_Right.position, this.transform.position), currentBubble.Bottom_Right);
                                ForSort.Add(Vector3.Distance(currentBubble.Bottom_Right.position, this.transform.position));
                            }
                        }
                        catch (System.Exception e)
                        {

                        }
                        ForSort.Sort();
                        if (ForSort[0] != null)
                        {
                            this.transform.position = nearestDistance[ForSort[0]].position;
                            this.GetComponent<CircleCollider2D>().enabled = false;
                            
                                FuseInGrid(ForSort[0]);
                            
                        }

                    }
                    atRest = true;

                    gameManager.OnGridPlacement();

                }
            
            
        }
    }


    void FuseInGrid(float dist)
    {
        
        this.GetComponent<SpriteRenderer>().enabled = false;
        nearestDistance[dist].GetComponent<BubbleProperties>().isEmpty = false;
        nearestDistance[dist].GetComponent<BubbleProperties>().BubbleColor = this.BubbleColor;
        nearestDistance[dist].GetComponent<BubbleProperties>().SearchMatchingNeighbours();
        nearestDistance[dist].GetComponent<BubbleProperties>().inStack = false;


        prev_Score = GamePrefs.lEVEL_SCORE;


        if(gameManager.colorMatcher.Count>=2)
        {


            foreach (Transform temp in gameManager.colorMatcher)
            {
                
                  GamePrefs.lEVEL_SCORE += 5;
                
               // GameObject pop_anim = pop_pooler.GetPooledObject();
                //pop_anim.transform.position = temp.transform.position;
                //pop_anim.transform.rotation = Quaternion.identity;
                //pop_anim.transform.localScale = temp.transform.localScale;
                //pop_anim.SetActive(true);

                temp.GetComponent<BubbleProperties>().isEmpty = true;

            }

            //GameObject pop_anim_again = pop_pooler.GetPooledObject();
            //pop_anim_again.transform.position = nearestDistance[dist].transform.position;
            //pop_anim_again.transform.rotation = Quaternion.identity;
            //pop_anim_again.transform.localScale = nearestDistance[dist].transform.localScale;
            //pop_anim_again.SetActive(true);
            nearestDistance[dist].GetComponent<BubbleProperties>().isEmpty = true;


            gameManager.OnMatch_three();
        }


        gameManager.colorMatcher.Clear();

    }



   


    #endregion

}
