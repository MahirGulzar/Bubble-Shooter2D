using UnityEngine;
using System.Collections;

public class Dynamic_Grid : MonoBehaviour {

    public int Num_Of_Rows;
    public int Num_Of_Columns;
    public float Adjacent_distance;
    public float vertical_distance;
    public Transform Initial_Point_Bubble;
    public Transform[][] MatrixGrid;

    public float starting_point_x_for_long;
    public float starting_point_y;

    public const float Adjacent = 0.88f;
    public const float Vertical = 0.74f;
    public const float starting_point_x_for_short = -2.64f;

	void Awake () {

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
                MatrixGrid[i] = new Transform[Num_Of_Columns-1];
            }
        }




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
                        Initial_Point_Bubble.name = "["+i+"]["+j+"]";

                }
                if(i<Num_Of_Rows-1)
                {
                    starting_point_y = starting_point_y - 0.74f;
                    Transform temp2 = (Transform)Instantiate(Initial_Point_Bubble);
                    temp2.parent = Initial_Point_Bubble.parent;
                    temp2.localPosition = new Vector3(starting_point_x_for_short, starting_point_y, Initial_Point_Bubble.localPosition.z);
                    temp2.localScale = Initial_Point_Bubble.localScale;
                    Initial_Point_Bubble = temp2;
                    Initial_Point_Bubble.name = "[" + (i + 1) + "][0]";
                }
            }
            else
            {
                for (int j = 0; j < Num_Of_Columns - 1; j++)
                {
                    if (j==0)
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
                    Initial_Point_Bubble.name = "[" +i + "][" + j + "]";

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
                }
            }

            

        }


	}
	
	
}
