using UnityEngine;
using System.Collections;

public class Pivots : MonoBehaviour {

    public bool isFilled;


    public GameManager gameManager;
    public int pivotRow,pivotcolumn;
    private int nextPivot;

    public GameObject InitialMid, FinalMid, _Grid, TopBorder, TopBorderUI;




    private bool step_Completed = true;
    private bool stop = false;
    private bool dontMoveGrid = false;


    int WIDTH;
	[SerializeField] public float y_threshold=0.4f;
	[SerializeField] public float move_Distance = 0.46f;



    public bool group1;
    public bool group2;
    public bool group3;



	void Awake()
	{

		
	}



	void Start()
	{
		
		pivotRow = gameManager.Num_Of_Rows - 5;
		Invoke("MoveDown", 1f);

	}



	void MoveDown()
	{
		int checkEven = 0;
		int checkOdd = 0;

		int empty_row = 0;

		for (int i = 0; i < gameManager.Num_Of_Rows; i++)
		{
			if (i % 2 == 0)
			{
				for (int j = 0; j < gameManager.Num_Of_Columns; j++)
				{
					if (gameManager.MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty)
					{
						checkEven++;

					}

				}
				if (checkEven == gameManager.Num_Of_Columns)
				{
					empty_row = i;
					//print(checkEven);
					//print("Breaking");
					break;
				}
				checkEven = 0;
			}
			else
			{
				for (int j = 0; j < gameManager.Num_Of_Columns - 1; j++)
				{
					if (gameManager.MatrixGrid[i][j].GetComponent<BubbleProperties>().isEmpty)
					{
						checkOdd++;
					}
				}
				if (checkOdd == gameManager.Num_Of_Columns - 1)
				{
					empty_row = i;
					//print(i);
					//print("Breaking");
					break;
				}
				checkOdd = 0;
			}

		}



		if (empty_row <= 9 )
		{
			if (!dontMoveGrid)
			{
				dontMoveGrid = true;
				int gap = pivotRow - 9;
				//print("gap" + gap);
				stop = true;
				if (_Grid.transform.position.y > y_threshold)
				{
					iTween.MoveTo(_Grid, iTween.Hash("y", _Grid.transform.position.y - gap * (move_Distance), "speed", 4f,"easetype",iTween.EaseType.linear, "onComplete", "MoveStepComplete", "onCompleteTarget", this.gameObject));
					iTween.MoveTo(TopBorder, iTween.Hash("y", TopBorder.transform.position.y - gap * (move_Distance)));
					iTween.MoveTo(TopBorderUI, iTween.Hash("y", TopBorderUI.transform.position.y - gap * (move_Distance)));
					//print("Tween Empty");
				}
			}
		}
		else
		{
			//print("Empty"+empty_row);
			//print("Pivot" + pivotRow);
			int stepstoMove = (pivotRow - empty_row);
			nextPivot = pivotRow - (pivotRow - empty_row);

			if (nextPivot < 9)
			{
				//int diff = (pivotRow - nextPivot)-;
			}
			//print("NextPivot" + nextPivot);
			if (stepstoMove <= 0)
			{
				stepstoMove = 0;
			}

			//print("stepsTomove"+stepstoMove);
			if (pivotRow % 2 == 0)
			{
				pivotcolumn = gameManager.Num_Of_Columns;
			}
			else
			{
				pivotcolumn = gameManager.Num_Of_Columns - 1;
			}

			bool emptyCheck = true;
			for (int i = 0; i < pivotcolumn; i++)
			{
				if (!gameManager.MatrixGrid[pivotRow][i].GetComponent<BubbleProperties>().isEmpty)
				{
					emptyCheck = false;
					break;
				}
			}
			////print(emptyCheck);


			if (emptyCheck && step_Completed && _Grid.transform.position.y > y_threshold)
			{
				step_Completed = false;
				iTween.MoveTo(_Grid, iTween.Hash("y", _Grid.transform.position.y - stepstoMove * (move_Distance), "speed", 4f, "easetype", iTween.EaseType.linear, "onComplete", "MoveStepComplete", "onCompleteTarget", this.gameObject));
				iTween.MoveTo(TopBorder, iTween.Hash("y", TopBorder.transform.position.y -stepstoMove * (move_Distance)));
				iTween.MoveTo(TopBorderUI, iTween.Hash("y", TopBorderUI.transform.position.y - stepstoMove * (move_Distance)));
			}
		}
	}


	void MoveStepComplete()
	{
		step_Completed = true;
		if(!stop)
			pivotRow = nextPivot;
		//  InitialMid.GetComponent<Pivots>().isFilled = false;
	}


   
}
