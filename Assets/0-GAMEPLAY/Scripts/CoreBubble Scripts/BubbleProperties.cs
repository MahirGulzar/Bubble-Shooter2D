using UnityEngine;
using System.Collections;

public enum ColorProperty
{
    Green,
    Blue,
    Yellow,
    Red,
    Purple,
    Black,
    NullType,
}





public class BubbleProperties : MonoBehaviour {
    

    public bool showRenderer;
    public bool isEmpty=true;
    public bool inStack = false;
    public bool inRadius = false;
    public ColorProperty  BubbleColor;

    

    public Transform Top_Left,Top_Right,Left,Right,Bottom_Left,Bottom_Right;


    [SerializeField]
    public int i, j;


    private GameManager gameManager;
    private SpriteRenderer sp;

    void Awake()
    {

        gameManager = GameManager.FindObjectOfType<GameManager>();
        sp = this.GetComponent<SpriteRenderer>();
        
    }
    void Start()
    {

        if (i % 2 == 0)
        {
            try { Top_Left = gameManager.MatrixGrid[i - 1][j - 1]; }
            catch (System.Exception e) { Top_Left = null; }
            try { Top_Right = gameManager.MatrixGrid[i - 1][j]; }
            catch (System.Exception e) { Top_Right = null; }

            try { Bottom_Left = gameManager.MatrixGrid[i + 1][j - 1]; }
            catch (System.Exception e) { Bottom_Left = null; }
            try { Bottom_Right = gameManager.MatrixGrid[i + 1][j]; }
            catch (System.Exception e) { Bottom_Right = null; }
        }
        else
        {
            try { Top_Left = gameManager.MatrixGrid[i - 1][j]; }
            catch (System.Exception e) { Top_Left = null; }
            try { Top_Right = gameManager.MatrixGrid[i - 1][j + 1]; }
            catch (System.Exception e) { Top_Right = null; }

            try { Bottom_Left = gameManager.MatrixGrid[i + 1][j]; }
            catch (System.Exception e) { Bottom_Left = null; }
            try { Bottom_Right = gameManager.MatrixGrid[i + 1][j + 1]; }
            catch (System.Exception e) { Bottom_Right = null; }
        }


        try { Left = gameManager.MatrixGrid[i][j - 1]; }
        catch (System.Exception e) { Left = null; }
        try { Right = gameManager.MatrixGrid[i][j + 1]; }
        catch (System.Exception e) { Right = null; }

        

        if (isEmpty)
        {
            
            inStack = false;
            Color tempColor = new Color(1, 1, 1, 0.0f);
            this.GetComponent<SpriteRenderer>().color = tempColor;
            this.GetComponent<CircleCollider2D>().isTrigger = true;
            BubbleColor = ColorProperty.NullType;
        }
        else if (!isEmpty)
        {

            switch (BubbleColor)
            {
                case ColorProperty.Green:
                    sp.sprite = gameManager.sp_bubble[0];
                    //sp.color = new Color(0, 1, 0);
                    break;
                case ColorProperty.Blue:
                    sp.sprite = gameManager.sp_bubble[1];
                    //sp.color = new Color(0, 0, 1);
                    break;
                case ColorProperty.Yellow:
                    sp.sprite = gameManager.sp_bubble[2];
                    //sp.color = new Color(1, 1, 0);
                    break;
                case ColorProperty.Red:
                    sp.sprite = gameManager.sp_bubble[3];
                    //sp.color = new Color(1, 0, 0);
                    break;
                case ColorProperty.Purple:
                    sp.sprite = gameManager.sp_bubble[4];
                    //sp.color = new Color32(238, 130, 238, 255);
                    break;
                case ColorProperty.Black:
                    sp.sprite = gameManager.sp_bubble[5];
                    //sp.color = new Color(0, 1, 0);
                    break;
            }
            Color tempColor = new Color(1, 1, 1, 1f);
            this.GetComponent<SpriteRenderer>().color = tempColor;
            this.GetComponent<CircleCollider2D>().isTrigger = false;
            
        }

        

      
    }

    void OnEnable()
    {
        gameManager.OnGridPlacement += this.OnGridPlacement;
        gameManager.OnTouchDown += this.OnTouchDown;
    }

    void OnDisable()
    {
        gameManager.OnGridPlacement -= this.OnGridPlacement;
        gameManager.OnTouchDown -= this.OnTouchDown;
    }

    void OnGridPlacement()
    {
        //Debug.Log("Event Raised");
    }

    void OnTouchDown()
    {
        this.inStack = false;
    }

    void SetInitialColors()
    {
        if (!isEmpty)
        {
            switch (BubbleColor)
            {
                case ColorProperty.Green:
                    sp.sprite = gameManager.sp_bubble[0];
                    //sp.color = new Color(0, 1, 0);
                    break;
                case ColorProperty.Blue:
                    sp.sprite = gameManager.sp_bubble[1];
                    //sp.color = new Color(0, 0, 1);
                    break;
                case ColorProperty.Yellow:
                    sp.sprite = gameManager.sp_bubble[2];
                    //sp.color = new Color(1, 1, 0);
                    break;
                case ColorProperty.Red:
                    sp.sprite = gameManager.sp_bubble[3];
                    //sp.color = new Color(1, 0, 0);
                    break;
                case ColorProperty.Purple:
                    sp.sprite = gameManager.sp_bubble[4];
                    //sp.color = new Color32(238, 130, 238, 255);
                    break;
                case ColorProperty.Black:
                    sp.sprite = gameManager.sp_bubble[5];
                    //sp.color = new Color(0, 1, 0);
                    break;
            }
        }
    }


    public void SearchInRadius()
    {
        this.inRadius = true;
        //gameManager.radiusCount++;

       if(gameManager.InRadiusOfExploision.Count>=7)
       {
           return;
       }

        try
        {
            if ((!Top_Left.GetComponent<BubbleProperties>().inRadius && !Top_Left.GetComponent<BubbleProperties>().isEmpty &&
                gameManager.InRadiusOfExploision.Count<=10))
            {
                gameManager.InRadiusOfExploision.Add(Top_Left.transform);
                Top_Left.GetComponent<BubbleProperties>().SearchInRadius();
                


            }
        }
        catch (System.Exception e)
        {

        }
        try
        {
            if ((!Top_Right.GetComponent<BubbleProperties>().inRadius && !Top_Right.GetComponent<BubbleProperties>().isEmpty &&
                gameManager.InRadiusOfExploision.Count <= 10))
            {
                gameManager.InRadiusOfExploision.Add(Top_Right.transform);
                Top_Right.GetComponent<BubbleProperties>().SearchInRadius();
                


            }
        }
        catch (System.Exception e)
        {

        }
        try
        {
            if ((!Left.GetComponent<BubbleProperties>().inRadius && !Left.GetComponent<BubbleProperties>().isEmpty &&
                gameManager.InRadiusOfExploision.Count <= 10))
            {
                gameManager.InRadiusOfExploision.Add(Left.transform);
                Left.GetComponent<BubbleProperties>().SearchInRadius();
                


            }
        }
        catch (System.Exception e)
        {

        }
        try
        {
            if ((!Right.GetComponent<BubbleProperties>().inRadius && !Right.GetComponent<BubbleProperties>().isEmpty &&
                gameManager.InRadiusOfExploision.Count <= 10))
            {
                gameManager.InRadiusOfExploision.Add(Right.transform);
                Right.GetComponent<BubbleProperties>().SearchInRadius();
                


            }
        }
        catch (System.Exception e)
        {

        }
        //try
        //{
        //    if ((!Bottom_Left.GetComponent<BubbleProperties>().inRadius && !Bottom_Left.GetComponent<BubbleProperties>().isEmpty &&
        //        gameManager.InRadiusOfExploision.Count <= 10))
        //    {
        //        gameManager.InRadiusOfExploision.Add(Bottom_Left.transform);
        //        Bottom_Left.GetComponent<BubbleProperties>().SearchInRadius();
                


        //    }
        //}
        //catch (System.Exception e)
        //{

        //}

        //try
        //{
        //    if ((!Bottom_Right.GetComponent<BubbleProperties>().inRadius && !Bottom_Right.GetComponent<BubbleProperties>().isEmpty &&
        //        gameManager.InRadiusOfExploision.Count <= 10))
        //    {
        //        gameManager.InRadiusOfExploision.Add(Bottom_Right.transform);
        //        Bottom_Right.GetComponent<BubbleProperties>().SearchInRadius();
                


        //    }
        //}
        //catch (System.Exception e)
        //{

        //}
        return;
    }

    public void SearchMatchingNeighbours()
    {
        this.inStack=true;
        //this.GetComponent<Animator>().enabled = true;
        //this.GetComponent<Animator>().SetTrigger("SimplePop");
        
       // gameManager.tracer.Add(this.gameObject);
        
        try
        {
            if ((this.BubbleColor == Top_Left.GetComponent<BubbleProperties>().BubbleColor) && (!Top_Left.GetComponent<BubbleProperties>().inStack))
            {

                Top_Left.GetComponent<BubbleProperties>().SearchMatchingNeighbours();
                gameManager.colorMatcher.Add(Top_Left.transform);
                //print("TL");
                //gameManager.matcher.Add()
                
            }
        }
        catch(System.Exception e)
        {

        }
        try
        {
            if ((this.BubbleColor == Top_Right.GetComponent<BubbleProperties>().BubbleColor) && (!Top_Right.GetComponent<BubbleProperties>().inStack))
            {
               Top_Right.GetComponent<BubbleProperties>().SearchMatchingNeighbours();
               gameManager.colorMatcher.Add(Top_Right.transform);
               //print("TR");
            }
        }
        catch (System.Exception e)
        {

        }
        try
        {
            if ((this.BubbleColor == Left.GetComponent<BubbleProperties>().BubbleColor) && (!Left.GetComponent<BubbleProperties>().inStack) )
            {
                 Left.GetComponent<BubbleProperties>().SearchMatchingNeighbours();
                 gameManager.colorMatcher.Add(Left.transform);
                 //print("L");
            }
        }
        catch (System.Exception e)
        {

        }
        try
        {
            if ((this.BubbleColor == Right.GetComponent<BubbleProperties>().BubbleColor) && (!Right.GetComponent<BubbleProperties>().inStack) )
            {
                Right.GetComponent<BubbleProperties>().SearchMatchingNeighbours();
                gameManager.colorMatcher.Add(Right.transform);
                //print("R");
            }
        }
        catch (System.Exception e)
        {

        }
        try
        {
            if ((this.BubbleColor == Bottom_Left.GetComponent<BubbleProperties>().BubbleColor) && (!Bottom_Left.GetComponent<BubbleProperties>().inStack))
            {
                Bottom_Left.GetComponent<BubbleProperties>().SearchMatchingNeighbours();
                gameManager.colorMatcher.Add(Bottom_Left.transform);
                //print("BL");
            }
        }
        catch (System.Exception e)
        {

        }

        try
        {
            if ((this.BubbleColor == Bottom_Right.GetComponent<BubbleProperties>().BubbleColor) && (!Bottom_Right.GetComponent<BubbleProperties>().inStack))
            {
                Bottom_Right.GetComponent<BubbleProperties>().SearchMatchingNeighbours();
                gameManager.colorMatcher.Add(Bottom_Right.transform);
                //print("BR");
            }
        }
        catch (System.Exception e)
        {

        }
        return;
    }


	
	// Update is called once per frame
    void Update()
    {
        if (isEmpty)
        {
            inStack = false;
            Color tempColor = new Color(1, 1, 1, 0.0f);
            this.GetComponent<SpriteRenderer>().color = tempColor;
            this.GetComponent<CircleCollider2D>().isTrigger = true;
            BubbleColor = ColorProperty.NullType;
            
        }
        else if (!isEmpty)
        {
           
            switch (BubbleColor)
            {
                case ColorProperty.Green:
                    sp.sprite = gameManager.sp_bubble[0];
                    //sp.color = new Color(0, 1, 0);
                    break;
                case ColorProperty.Blue:
                    sp.sprite = gameManager.sp_bubble[1];
                    //sp.color = new Color(0, 0, 1);
                    break;
                case ColorProperty.Yellow:
                    sp.sprite = gameManager.sp_bubble[2];
                    //sp.color = new Color(1, 1, 0);
                    break;
                case ColorProperty.Red:
                    sp.sprite = gameManager.sp_bubble[3];
                    //sp.color = new Color(1, 0, 0);
                    break;
                case ColorProperty.Purple:
                    sp.sprite = gameManager.sp_bubble[4];
                    //sp.color = new Color32(238, 130, 238, 255);
                    break;
                case ColorProperty.Black:
                    sp.sprite = gameManager.sp_bubble[5];
                    //sp.color = new Color(0, 1, 0);
                    break;
            }
            Color tempColor = new Color(1, 1, 1, 1f);
            this.GetComponent<SpriteRenderer>().color = tempColor;
            this.GetComponent<CircleCollider2D>().isTrigger = false;
        }

    }
    

    
}
