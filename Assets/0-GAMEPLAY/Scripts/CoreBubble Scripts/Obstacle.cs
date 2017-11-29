using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SpecialType
{
    Blade,
    Fire,
    Ice,
}


public class Obstacle : MonoBehaviour {
    
    [SerializeField]
    ColorProperty BubbleColor;

    [SerializeField]
    bool atRest=false;

    private GameManager gameManager;
    private SpriteRenderer sp;
    private Dictionary<float, Transform> nearestDistance;




    //Object Pooling
   // public GameObject ObjectPool_bubble_pop;
   // public GameObject ObjectPool_ScoreFly;
   // private ObjectPoolScript pop_pooler;
    public Transform Android_Pos, IOS_Pos;
    public GameObject BubblePop_Animation;
    public bool isSpecial;
    public bool isIce;
    public bool isBlade;
    public SpecialType special_type;
    public GameObject VoiceOver;


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
       // ObjectPool = GameObject.FindObjectOfType<ObjectPoolScript>();
        //ObjectPool_ScoreFly = GameObject.Find("ObjectPooling_ScoreFlyer");
        //pop_pooler = ObjectPool_bubble_pop.GetComponent<ObjectPoolScript>();
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
        gameManager.OnSpecialBubbleSelected += this.OnSpecialBubbleSelected;
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
        gameManager.OnSpecialBubbleSelected -= this.OnSpecialBubbleSelected;
    }

    void OnGridPlacement()
    {
        //
    }
    void OnTouchDown()
    {
        //this.inStack = false;
    }
    void OnSpecialBubbleSelected()
    {
        // Perform Special Bubble Functionality
         GameObject particles=null;
          switch(special_type)
            {
              case SpecialType.Blade:
                   // sp.sprite = gameManager.sp_special[0];
                    particles = Instantiate(Resources.Load("BladeParticle")) as GameObject;
                    isBlade = true;
                    break;
              case SpecialType.Fire:
                   // sp.sprite = gameManager.sp_special[1];
                    particles = Instantiate(Resources.Load("FireParticle")) as GameObject;
                    isSpecial = true;
                    break;
              case SpecialType.Ice:
                   // sp.sprite = gameManager.sp_special[2];
                    particles = Instantiate(Resources.Load("IceParticle")) as GameObject;
                    isIce = true;
                    break;

            }
          particles.transform.parent = this.transform;
          particles.transform.localPosition = new Vector3(0, 0, 0);
          
        
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
                        if (isSpecial)
                        {
                            // BlastGrid_In_Radius(ForSort[0]);
                            Collider2D[] Colliders = Physics2D.OverlapCircleAll(nearestDistance[ForSort[0]].position, 1.5f, 1 << LayerMask.NameToLayer("Default"));
                            BlastGrid_In_Cirle_Radius(Colliders, ForSort[0]);
                        }
                        else if (isIce)
                        {
                            int rowNumber = 0;
                            int columnLength;
                            if ((rowNumber % 2) == 0)
                                columnLength = 10;
                            else
                                columnLength = 9;

                            Ice_Functionality(rowNumber, columnLength);

                        }
                        else if (isBlade)
                        {
                            GameObject temp2;
                            switch (special_type)
                            {
                                case SpecialType.Blade:
                                    SoundManager.Instance.ClickBladeSound();
                                    temp2 = Instantiate(Resources.Load("BladeAnimation")) as GameObject;
                                    break;

                                case SpecialType.Fire:
                                    SoundManager.Instance.ClickFireSound();
                                    temp2 = Instantiate(Resources.Load("FireAnimation")) as GameObject;
                                    break;

                                default:
                                    SoundManager.Instance.ClickIceSound();
                                    temp2 = Instantiate(Resources.Load("IceAnimation")) as GameObject;
                                    break;
                            }
                            temp2.transform.parent = this.gameObject.transform.parent;
                            temp2.transform.position = this.transform.position;
                            this.isBlade = false;
                        }
                        else
                        {
                            FuseInGrid(ForSort[0]);
                        }
                        


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
                            if (isSpecial)
                            {
                                // BlastGrid_In_Radius(ForSort[0]);
                                Collider2D[] Colliders = Physics2D.OverlapCircleAll(nearestDistance[ForSort[0]].position, 1.5f, 1 << LayerMask.NameToLayer("Default"));
                                BlastGrid_In_Cirle_Radius(Colliders, ForSort[0]);
                            }
                            else if(isIce)
                            {
                                int rowNumber = other.gameObject.GetComponent<BubbleProperties>().i;
                                int columnLength;
                                if ((rowNumber % 2) == 0)
                                    columnLength = 10;
                                else
                                    columnLength = 9;

                                Ice_Functionality(rowNumber, columnLength);

                            }
                            else if(isBlade)
                            {
                                Blade_Functionality(other.gameObject.GetComponent<BubbleProperties>().BubbleColor);
                            }
                            else
                            {
                                FuseInGrid(ForSort[0]);
                            }
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
        //gameManager.OnVoiceOvers();

        if(gameManager.colorMatcher.Count>=2)
        {


            foreach (Transform temp in gameManager.colorMatcher)
            {
                
                if (temp.GetComponent<BubbleProperties>().isBird)
                {
                    SoundManager.Instance.ClickBirdSound();
                    temp.GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<Animator>().SetTrigger("FreeBird");
                    temp.GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<TweenBird>().tweener = true;
                    //gameManager.birds_rescued++;
                    GamePrefs.lEVEL_SCORE += 10;
                    temp.GetComponent<BubbleProperties>().isBird = false;
                    //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                    //flyer.GetComponent<ScoreFly>().ScoreToFly = 10;
                    //flyer.transform.position = temp.gameObject.transform.position;
                    //flyer.SetActive(true);
                }
                else
                {
                    //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                    //flyer.GetComponent<ScoreFly>().ScoreToFly = 5;
                    //flyer.transform.position = temp.gameObject.transform.position;
                    //flyer.SetActive(true);
                    //gameManager.Level_Score += 5;
                    GamePrefs.lEVEL_SCORE += 5;
                }
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

            
            //VoiceOver.GetComponent<SpriteRenderer>().sprite = gameManager.sp_VoiceOvers[Rand];
            
            if (gameManager.colorMatcher.Count>=15)
            {
                SoundManager.Instance.ClickCombo4Sound();
            
               // Debug.Log("Combo4");
            }
            else if (gameManager.colorMatcher.Count >= 10)
            {
                SoundManager.Instance.ClickCombo3Sound();
 
              //  Debug.Log("Combo3");
            }
            else if (gameManager.colorMatcher.Count >= 5)
            {
                SoundManager.Instance.ClickCombo2Sound();

               // Debug.Log("Combo2");
            }
            else
            {
                SoundManager.Instance.ClickCombo1Sound();
            }


            gameManager.OnMatch_three();
        }

        //gameManager.IncreamentScore(GamePrefs.lEVEL_SCORE - prev_Score);
        //StartCoroutine(IncreamentalScore(GamePrefs.lEVEL_SCORE - prev_Score));

        gameManager.colorMatcher.Clear();

    }



    void Ice_Functionality(int rowNumber, int columnLength)
    {
        
        List<Transform> inRows = new List<Transform>();

        for (int j = 0; j < columnLength; j++)
        {
            inRows.Add(gameManager.MatrixGrid[rowNumber][j]);
        }

        if(rowNumber>0)
        {
            rowNumber--;
            if(columnLength==10)
            {
                columnLength = 9;
                
            }
            else
            {
                columnLength = 10;
            }
            for (int j = 0; j < columnLength; j++)
            {
                inRows.Add(gameManager.MatrixGrid[rowNumber][j]);
            }
            
        }
        Instantiate(Resources.Load("StripesEffect") as GameObject, new Vector2(0, this.transform.position.y+0.5f), Quaternion.identity);

        foreach(Transform temp in inRows)
        {
            if (!temp.GetComponent<BubbleProperties>().isEmpty)
            {
                if (temp.GetComponent<BubbleProperties>().isBird)
                {
                    SoundManager.Instance.ClickBirdSound();
                    temp.GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<Animator>().SetTrigger("FreeBird");
                    temp.GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<TweenBird>().tweener = true;
                    //gameManager.birds_rescued++;
                   // gameManager.Level_Score += 10;          // scoring bird
                    //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                    //flyer.GetComponent<ScoreFly>().ScoreToFly = 10;
                    //flyer.transform.position = temp.gameObject.transform.position;
                    //flyer.SetActive(true);
                }
                else
                {
                    //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                    //flyer.GetComponent<ScoreFly>().ScoreToFly = 5;
                    //flyer.transform.position = temp.gameObject.transform.position;
                    //flyer.SetActive(true);
                    //gameManager.Level_Score += 5;
                }
               // GameObject pop_anim = pop_pooler.GetPooledObject();
                //pop_anim.transform.position =temp.transform.position;
                //pop_anim.transform.rotation = Quaternion.identity;
                //pop_anim.transform.localScale = temp.transform.localScale;
                //pop_anim.SetActive(true);

                temp.GetComponent<BubbleProperties>().isEmpty = true;
            }
        }

            GameObject temp2;
            switch (special_type)
            {
                case SpecialType.Blade:
                    SoundManager.Instance.ClickBladeSound();
                    temp2 = Instantiate(Resources.Load("BladeAnimation")) as GameObject;
                    break;

                case SpecialType.Fire:
                    SoundManager.Instance.ClickFireSound();
                    temp2 = Instantiate(Resources.Load("FireAnimation")) as GameObject;
                    break;

                default:
                    SoundManager.Instance.ClickIceSound();
                    temp2 = Instantiate(Resources.Load("IceAnimation")) as GameObject;
                    break;
            }
            temp2.transform.parent = this.gameObject.transform.parent;
            temp2.transform.position = this.transform.position;
            this.isIce = false;
            inRows.Clear();
            gameManager.OnMatch_three();
            gameManager.colorMatcher.Clear();
        
        
    }

    void Blade_Functionality(ColorProperty cp)
    {
        for (int i = 0; i < gameManager.Num_Of_Rows; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < gameManager.Num_Of_Columns; j++)
                {
                    
                    if(gameManager.MatrixGrid[i][j].GetComponent<BubbleProperties>().BubbleColor==cp && !gameManager.MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty)
                    {
                        Transform temp = gameManager.MatrixGrid[i][j];
                        if (temp.GetComponent<BubbleProperties>().isBird)
                        {
                            SoundManager.Instance.ClickBirdSound();
                            temp.GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<Animator>().SetTrigger("FreeBird");
                            temp.GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<TweenBird>().tweener = true;
                           // gameManager.birds_rescued++;
                          //  gameManager.Level_Score += 10;          // scoring bird
                            //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                            //flyer.GetComponent<ScoreFly>().ScoreToFly = 10;
                            //flyer.transform.position = temp.gameObject.transform.position;
                            //flyer.SetActive(true);
                        }
                        else
                        {
                            //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                            //flyer.GetComponent<ScoreFly>().ScoreToFly = 5;
                            //flyer.transform.position = temp.gameObject.transform.position;
                            //flyer.SetActive(true);
                           // gameManager.Level_Score += 5;
                        }
                        //GameObject pop_anim = pop_pooler.GetPooledObject();
                        //pop_anim.transform.position = temp.transform.position;
                        //pop_anim.transform.rotation = Quaternion.identity;
                        //pop_anim.transform.localScale = temp.transform.localScale;
                        //pop_anim.SetActive(true);

                        temp.GetComponent<BubbleProperties>().isEmpty = true;
                    }

                }
            }
            else
            {
                for (int j = 0; j < gameManager.Num_Of_Columns - 1; j++)
                {
                    if (gameManager.MatrixGrid[i][j].GetComponent<BubbleProperties>().BubbleColor == cp && !gameManager.MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty)
                    {
                        Transform temp = gameManager.MatrixGrid[i][j];
                        if (temp.GetComponent<BubbleProperties>().isBird)
                        {
                            SoundManager.Instance.ClickBirdSound();
                            temp.GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<Animator>().SetTrigger("FreeBird");
                            temp.GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<TweenBird>().tweener = true;
                           // gameManager.birds_rescued++;
                          //  gameManager.Level_Score += 10;          // scoring bird
                            //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                            //flyer.GetComponent<ScoreFly>().ScoreToFly = 10;
                            //flyer.transform.position = temp.gameObject.transform.position;
                            //flyer.SetActive(true);
                        }
                        else
                        {
                            //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                            //flyer.GetComponent<ScoreFly>().ScoreToFly = 5;
                            //flyer.transform.position = temp.gameObject.transform.position;
                            //flyer.SetActive(true);
                            //gameManager.Level_Score += 5;
                        }
                        //GameObject pop_anim = pop_pooler.GetPooledObject();
                        //pop_anim.transform.position = temp.transform.position;
                        //pop_anim.transform.rotation = Quaternion.identity;
                        //pop_anim.transform.localScale = temp.transform.localScale;
                        //pop_anim.SetActive(true);

                        temp.GetComponent<BubbleProperties>().isEmpty = true;
                    }
                }
            }

        }

        GameObject temp2;
        switch (special_type)
        {
            case SpecialType.Blade:
                SoundManager.Instance.ClickBladeSound();
                temp2 = Instantiate(Resources.Load("BladeAnimation")) as GameObject;
                break;

            case SpecialType.Fire:
                SoundManager.Instance.ClickFireSound();
                temp2 = Instantiate(Resources.Load("FireAnimation")) as GameObject;
                break;

            default:
                SoundManager.Instance.ClickIceSound();
                temp2 = Instantiate(Resources.Load("IceAnimation")) as GameObject;
                break;
        }
        temp2.transform.parent = this.gameObject.transform.parent;
        temp2.transform.position = this.transform.position;
        this.isBlade = false;
        gameManager.OnMatch_three();
        gameManager.colorMatcher.Clear();
    }


    void BlastGrid_In_Cirle_Radius(Collider2D[] colliders, float dist)
   {


        for(int i=0;i<colliders.Length;i++)
        {
            if (colliders[i].tag == "BubbleTag")
            {
                if (!colliders[i].GetComponent<BubbleProperties>().isEmpty)
                {
                    if (colliders[i].GetComponent<BubbleProperties>().isBird)
                    {
                        SoundManager.Instance.ClickBirdSound();
                        colliders[i].GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<Animator>().SetTrigger("FreeBird");
                        colliders[i].GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<TweenBird>().tweener = true;
                        //gameManager.birds_rescued++;
                       // gameManager.Level_Score += 10;          // scoring bird
                        //GameObject flyer=ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                        //flyer.GetComponent<ScoreFly>().ScoreToFly = 10;
                        //flyer.transform.position = colliders[i].gameObject.transform.position;
                        //flyer.SetActive(true);
                    }
                    else
                    {
                        //GameObject flyer = ObjectPool_ScoreFly.GetComponent<ObjectPoolScript>().GetPooledObject();
                        //flyer.GetComponent<ScoreFly>().ScoreToFly = 5;
                        //flyer.transform.position = colliders[i].gameObject.transform.position;
                        //flyer.SetActive(true);
                       // gameManager.Level_Score += 5;
                    }
                    //GameObject pop_anim = pop_pooler.GetPooledObject();
                    //pop_anim.transform.position = colliders[i].transform.position;
                    //pop_anim.transform.rotation = Quaternion.identity;
                    //pop_anim.transform.localScale = colliders[i].transform.localScale;
                    //pop_anim.SetActive(true);

                    colliders[i].GetComponent<BubbleProperties>().isEmpty = true;
                }
            }
        }
        if (nearestDistance[dist].GetComponent<BubbleProperties>().isBird)
        {
            SoundManager.Instance.ClickBirdSound();
            nearestDistance[dist].GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<Animator>().SetTrigger("FreeBird");
            nearestDistance[dist].GetComponent<BubbleProperties>().Native_Bird_Object.GetComponent<TweenBird>().tweener = true;
            //gameManager.birds_rescued++;
           // gameManager.Level_Score += 10;          // scoring bird
        }
        //else
        //{
        //    gameManager.Level_Score += 5;
        //}
        nearestDistance[dist].GetComponent<BubbleProperties>().isEmpty = true;
        GameObject temp2;
        switch(special_type)
        {
            case SpecialType.Blade:
                SoundManager.Instance.ClickBladeSound();
                temp2 = Instantiate(Resources.Load("BladeAnimation")) as GameObject;
                break;
            
            case SpecialType.Fire:
                SoundManager.Instance.ClickFireSound();
                temp2 = Instantiate(Resources.Load("FireAnimation")) as GameObject;
                break;

            default:
                SoundManager.Instance.ClickIceSound();
                temp2 = Instantiate(Resources.Load("IceAnimation")) as GameObject;
                break;
        }
        
        temp2.transform.parent = this.gameObject.transform.parent;
        temp2.transform.position = this.transform.position;
        this.isSpecial = false;

        gameManager.OnMatch_three();
        gameManager.colorMatcher.Clear();
        gameManager.InRadiusOfExploision.Clear();
        //print()
   }


    #endregion

}
