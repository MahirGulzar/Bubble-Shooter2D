using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager:MonoBehaviour{

    //-----------------------------------------------------------------
    // Matrix Grids , 

    //TODO
    //Match 3 maintainance, Grid Treversing , Specified Grid, Level Management

    public int Num_Of_Rows;
    public int Num_Of_Columns;
    public float Adjacent_distance;
    public float vertical_distance;
    public Transform Initial_Point_Bubble;
    public float starting_point_x_for_long;
    public float starting_point_y;

    public const float Adjacent = 0.88f;
    public const float Vertical = 0.74f;
    public const float starting_point_x_for_short = -2.64f;

    public Transform[][] MatrixGrid;
    public Transform Grid_Parent;
    [HideInInspector]
    public List<Transform> colorMatcher;
    public Stack<Transform> connectedStack;
    [HideInInspector]
    public List<Transform> InRadiusOfExploision;
    private bool[][] connected_Flag_Cell;

    //------------------------------------------------------------------
    //Previous Actions

    public Action OnGridPlacement;
    public Action OnTouchDown;
    public Action OnMatch_three;
    public Action OnLevel_Cleared;
    public Action OnLevelFailed;
    public Action OnShootCannonEmpty;





    //------------------------------------------------------------------
    // Runtine Colors Remaining Management
    [HideInInspector]
    public List<ColorProperty> Remaining_Colors;


    //------------------------------------------------------------------
    // Falling Bubbles Object Pooling

    public ObjectPoolScript ObjectPool;
  


    public bool level_cleared;

    //public int radiusCount = 0;







    // Tweeners

    public GameObject PanelEnabler;


    // xml
    [HideInInspector]
    public TextAsset LevelXMLFile;
    private XMLNode LevelXML;
    private XMLParser parser;
    public GameObject _Grid;
    

    // Pivot Grid Movement
    public GameObject initialMid;
    private float toLerpY,t=0;
    public GameObject TopBorder,TopBorderUI;
    



    // temp variables

    int prev_Score;





	// iNitial mids pivots
	public Pivots pivots;


    public Sprite[] sp_bubble;
    
    void OnEnable()
    {
        this.OnMatch_three+= this.OnGridPlacementConnected;
        this.OnShootCannonEmpty += this.ShootCannonEmpty;

    }



    void OnDisable()
    {
        this.OnMatch_three -= this.OnGridPlacementConnected;
        this.OnShootCannonEmpty -= this.ShootCannonEmpty;
    }


    void Awake()
    {

        parser = new XMLParser();
        LevelXML = parser.Parse(LevelXMLFile.text);
        string _numOf_rows = LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>@numberOfRows");
        int rows_value;
        int.TryParse(_numOf_rows, out rows_value);

        Num_Of_Rows = rows_value;

        Generate_Dynamic_Grid();
        connectedStack = new Stack<Transform>();
        colorMatcher = new List<Transform>();
        InRadiusOfExploision = new List<Transform>();
  
		if(GameObject.FindGameObjectsWithTag("GameManager").Length > 1) 
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}



        connected_Flag_Cell = new bool[Num_Of_Rows][];
        for (int i = 0; i < Num_Of_Rows; i++)
        {
            if (i % 2 == 0)
            {
                connected_Flag_Cell[i] = new bool[Num_Of_Columns];
            }
            else
            {
                connected_Flag_Cell[i] = new bool[Num_Of_Columns-1];
            }
        }

        List<Transform> getGridChilds = new List<Transform>();



      
        
    }


    void Start()
    {

        if (Num_Of_Rows > 14)
        {
			#if UNITY_ANDROID
            _Grid.transform.position = new Vector3(_Grid.transform.position.x, _Grid.transform.position.y + (Num_Of_Rows - 14) * 0.463f, _Grid.transform.position.z);
            TopBorder.transform.position = new Vector3(TopBorder.transform.position.x, TopBorder.transform.position.y + (Num_Of_Rows - 14) * 0.463f, TopBorder.transform.position.z);
            TopBorderUI.transform.position = new Vector2(TopBorder.transform.position.x, TopBorder.transform.position.y + 2f);
			#endif
        }

    }






    void Generate_Dynamic_Grid()
    {
        starting_point_x_for_long = Initial_Point_Bubble.localPosition.x;
        starting_point_y = Initial_Point_Bubble.localPosition.y;

        Initial_Point_Bubble.name = "[0][0]";
        MatrixGrid = new Transform[Num_Of_Rows][];
        for (int i = 0; i < Num_Of_Rows; i++)
        {
            if (i % 2 == 0)
            {
                MatrixGrid[i] = new Transform[Num_Of_Columns];

            }
            else
            {
                MatrixGrid[i] = new Transform[Num_Of_Columns - 1];
            }
        }


        MatrixGrid[0][0] = Initial_Point_Bubble;
        MatrixGrid[0][0].GetComponent<BubbleProperties>().i = 0;
        MatrixGrid[0][0].GetComponent<BubbleProperties>().j = 0;

        for (int i = 0; i < Num_Of_Rows; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < Num_Of_Columns; j++)
                {

                    if (j == 0)
                    {
                        continue;
                    }
                    float pos_x = Initial_Point_Bubble.localPosition.x;
                    Transform temp = (Transform)Instantiate(Initial_Point_Bubble);
                    temp.parent = Initial_Point_Bubble.parent;
                    if (pos_x > 0)
                    {
                        temp.localPosition = new Vector3(Adjacent + Mathf.Abs(pos_x), Initial_Point_Bubble.localPosition.y, Initial_Point_Bubble.localPosition.z);
                    }
                    else
                    {
                        temp.localPosition = new Vector3(Adjacent - Mathf.Abs(pos_x), Initial_Point_Bubble.localPosition.y, Initial_Point_Bubble.localPosition.z);
                    }
                    temp.localScale = Initial_Point_Bubble.localScale;
                    Initial_Point_Bubble = temp;
                    Initial_Point_Bubble.name = "[" + i + "][" + j + "]";
                    MatrixGrid[i][j] = Initial_Point_Bubble;
                    MatrixGrid[i][j].GetComponent<BubbleProperties>().i = i;
                    MatrixGrid[i][j].GetComponent<BubbleProperties>().j = j;

                }
                if (i < Num_Of_Rows - 1)
                {
                    starting_point_y = starting_point_y - 0.74f;
                    Transform temp2 = (Transform)Instantiate(Initial_Point_Bubble);
                    temp2.parent = Initial_Point_Bubble.parent;
                    temp2.localPosition = new Vector3(starting_point_x_for_short, starting_point_y, Initial_Point_Bubble.localPosition.z);
                    temp2.localScale = Initial_Point_Bubble.localScale;
                    Initial_Point_Bubble = temp2;
                    Initial_Point_Bubble.name = "[" + (i + 1) + "][0]";
                    MatrixGrid[i + 1][0] = Initial_Point_Bubble;
                    MatrixGrid[i + 1][0].GetComponent<BubbleProperties>().i = i + 1;
                    MatrixGrid[i + 1][0].GetComponent<BubbleProperties>().j = 0;
                }
            }
            else
            {
                for (int j = 0; j < Num_Of_Columns - 1; j++)
                {
                    if (j == 0)
                    {
                        continue;
                    }
                    float pos_x = Initial_Point_Bubble.localPosition.x;
                    Transform temp = (Transform)Instantiate(Initial_Point_Bubble);
                    temp.parent = Initial_Point_Bubble.parent;
                    if (pos_x > 0)
                    {
                        temp.localPosition = new Vector3(Adjacent + Mathf.Abs(pos_x), Initial_Point_Bubble.localPosition.y, Initial_Point_Bubble.localPosition.z);
                    }
                    else
                    {
                        temp.localPosition = new Vector3(Adjacent - Mathf.Abs(pos_x), Initial_Point_Bubble.localPosition.y, Initial_Point_Bubble.localPosition.z);
                    }
                    temp.localScale = Initial_Point_Bubble.localScale;
                    Initial_Point_Bubble = temp;
                    Initial_Point_Bubble.name = "[" + i + "][" + j + "]";
                    MatrixGrid[i][j] = Initial_Point_Bubble;
                    MatrixGrid[i][j].GetComponent<BubbleProperties>().i = i;
                    MatrixGrid[i][j].GetComponent<BubbleProperties>().j = j;

                }
                if (i < Num_Of_Rows - 1)
                {
                    starting_point_y = starting_point_y - 0.74f;
                    Transform temp2 = (Transform)Instantiate(Initial_Point_Bubble);
                    temp2.parent = Initial_Point_Bubble.parent;
                    temp2.localPosition = new Vector3(starting_point_x_for_long, starting_point_y, Initial_Point_Bubble.localPosition.z);
                    temp2.localScale = Initial_Point_Bubble.localScale;
                    Initial_Point_Bubble = temp2;
                    Initial_Point_Bubble.name = "[" + (i + 1) + "][0]";
                    MatrixGrid[i+1][0] = Initial_Point_Bubble;
                    MatrixGrid[i+1][0].GetComponent<BubbleProperties>().i = i+1;
                    MatrixGrid[i+1][0].GetComponent<BubbleProperties>().j = 0;
                }
            }



        }

    }

    #region Match 3 and Binary Tree Search

    void OnGridPlacementConnected()
    {
        
        connectedStack.Clear();
        Remaining_Colors.Clear();

        // Clear connected Flag Bool Array
        for (int i = 0; i < Num_Of_Rows; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < Num_Of_Columns; j++)
                {
                    connected_Flag_Cell[i][j] = false;

                }
            }
            else
            {
                for (int j = 0; j < Num_Of_Columns-1; j++)
                {
                    connected_Flag_Cell[i][j] = false;
                }
            }

        }
        Connected_Nodes_Search_Initial_Call();                 
    }


    // Gonna work on it...

    void Connected_Nodes_Search_Initial_Call()
    {
        for (int i = 0; i < Num_Of_Columns; i++)
        {
            if (!MatrixGrid[0][i].GetComponent<BubbleProperties>().isEmpty)
                FindConnectedNodes(MatrixGrid[0][i]);
        }


        for (int i = 0; i < Num_Of_Rows; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < Num_Of_Columns; j++)
                {
                    if (connected_Flag_Cell[i][j] && !(Remaining_Colors.Contains(MatrixGrid[i][j].GetComponent<BubbleProperties>().BubbleColor)))
                    {
                        Remaining_Colors.Add(MatrixGrid[i][j].GetComponent<BubbleProperties>().BubbleColor);
                    }

                }
            }
            else
            {
                for (int j = 0; j < Num_Of_Columns - 1; j++)
                {
                    if (connected_Flag_Cell[i][j] && !(Remaining_Colors.Contains(MatrixGrid[i][j].GetComponent<BubbleProperties>().BubbleColor)))
                    {
                        Remaining_Colors.Add(MatrixGrid[i][j].GetComponent<BubbleProperties>().BubbleColor);
                    }
                }
            }

        }


        //Count For Cheers
        int falling_Count = 0;


        for (int i = 0; i < Num_Of_Rows; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < Num_Of_Columns; j++)
                {
                    if (!connected_Flag_Cell[i][j])
                    {
                        if (!MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty)
                        {
                            
                                GameObject temp = ObjectPool.GetPooledObject();
                                temp.transform.parent = MatrixGrid[i][j].transform.parent;
                                temp.transform.position = MatrixGrid[i][j].transform.position;
                                temp.transform.localScale = MatrixGrid[i][j].transform.localScale;
                                temp.GetComponent<FallingBubbleScript>().BubbleColor = MatrixGrid[i][j].GetComponent<BubbleProperties>().BubbleColor;
                                falling_Count++;
                                temp.SetActive(true);

                                int RandForce = UnityEngine.Random.Range(1000, 1200);
                                ExplosionForce2D.AddExplosionForce(temp.GetComponent<Rigidbody2D>(), RandForce, Vector2.up, 5);

                            
                            MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty = true;
                        }
                    }

                }
            }
            else
            {
                for (int j = 0; j < Num_Of_Columns - 1; j++)
                {
                    if (!connected_Flag_Cell[i][j])
                    {
                        if (!MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty)
                        {
                            
                                GameObject temp = ObjectPool.GetPooledObject();
                                temp.transform.parent = MatrixGrid[i][j].transform.parent;
                                temp.transform.position = MatrixGrid[i][j].transform.position;
                                temp.transform.localScale = MatrixGrid[i][j].transform.localScale;
                                temp.GetComponent<FallingBubbleScript>().BubbleColor = MatrixGrid[i][j].GetComponent<BubbleProperties>().BubbleColor;
                                falling_Count++;
                                temp.SetActive(true);
                                int RandForce = UnityEngine.Random.Range(1000, 1100);
                                ExplosionForce2D.AddExplosionForce(temp.GetComponent<Rigidbody2D>(), RandForce, Vector2.up, 5);
                                //print("Calling Fall Down...");
                            


                            MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty = true;
                            ////;
                        }
                    }
                }
            }

        }


        




        CheckLevelCleared();

        // Pivot check
        initialMid.SendMessage("MoveDown");
    }




    void FindConnectedNodes(Transform operationalNode)
    {
        BubbleProperties b_property = operationalNode.GetComponent<BubbleProperties>();
        if (!b_property.isEmpty && !(connectedStack.Contains(operationalNode)))
        {
            connectedStack.Push(operationalNode);                       // Push this node in stack as non empty connected node
            connected_Flag_Cell[b_property.i][b_property.j] = true;


            try
            {

                if (!connectedStack.Contains(b_property.Top_Left) && b_property.Top_Left != null && !b_property.Top_Left.GetComponent<BubbleProperties>().isEmpty)
                {
                    FindConnectedNodes(b_property.Top_Left);

                }
            }
            catch (System.Exception e)
            {

            }
            try
            {
                if (!connectedStack.Contains(b_property.Top_Right) && b_property.Top_Right != null && !b_property.Top_Right.GetComponent<BubbleProperties>().isEmpty)
                {
                    FindConnectedNodes(b_property.Top_Right);

                }
            }
            catch (System.Exception e)
            {

            }
            try
            {

                if (!connectedStack.Contains(b_property.Left) && b_property.Left!=null && !b_property.Left.GetComponent<BubbleProperties>().isEmpty)
                {
                    FindConnectedNodes(b_property.Left);

                }
            }
            catch (System.Exception e)
            {

            }
            try
            {
                if (!connectedStack.Contains(b_property.Right) && b_property.Right != null && !b_property.Right.GetComponent<BubbleProperties>().isEmpty)
                {
                    FindConnectedNodes(b_property.Right);

                }
            }
            catch (System.Exception e)
            {

            }
            try
            {
                if (!connectedStack.Contains(b_property.Bottom_Left) && b_property.Bottom_Left != null && !b_property.Bottom_Left.GetComponent<BubbleProperties>().isEmpty)
                {
                    FindConnectedNodes(b_property.Bottom_Left);

                }
            }
            catch (System.Exception e)
            {

            }

            try
            {
                if (!connectedStack.Contains(b_property.Bottom_Right) && b_property.Bottom_Right != null && !b_property.Bottom_Right.GetComponent<BubbleProperties>().isEmpty)
                {
                    FindConnectedNodes(b_property.Bottom_Right);

                }
            }
            catch (System.Exception e)
            {

            }
        }

            return;
    }




    void CheckLevelCleared()
    {


        switch(GamePrefs.LEVEL_OBJECTIVE)
        {
            case "clearAll":
                ClearAllFunctionality();
                break;
            case "topRow":
                ClearAllFunctionality();
                break;
            default:
                break;
        }


        
        
        //d

    }





    void ClearAllFunctionality()
    {
        bool filled_detected = false;
        for (int i = 0; i < Num_Of_Rows; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < Num_Of_Columns; j++)
                {
                    if (!MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty)
                    {
                        filled_detected = true;
                        continue;
                    }

                }
            }
            else
            {
                for (int j = 0; j < Num_Of_Columns - 1; j++)
                {
                    if (!MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty)
                    {
                        filled_detected = true;
                        continue;
                    }
                }
            }

        }

        if (!filled_detected)
        {

            OnLevel_Cleared();

            GamePrefs.temp_no_of_bubbles_remaining = GamePrefs.NO_OF_BUBBLES;


        }
    }
#endregion
	
    void ShootCannonEmpty()
    {
        
       
         PanelEnabler.GetComponent<Activators>().ActiveClear();
        
    }



}
