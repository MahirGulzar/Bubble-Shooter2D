﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XMLReaderExample : MonoBehaviour
{
    public TextAsset LevelXMLFile;
    private XMLNode LevelXML;
    private XMLParser parser;
    public GameObject _Grid;


    void Awake()
    {
        //Time.timeScale = 0.2f;
        parser = new XMLParser();
        LevelXML = parser.Parse(LevelXMLFile.text);

        

       
            
        int _numOf_Coordinates = LevelXML.GetNodeList("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>colors").Count;
        GamePrefs.NO_OF_SCENE_COLORS = _numOf_Coordinates;
        for (int i = 0; i < _numOf_Coordinates; i++)
        {
            string _attribute_color_String = LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>colors>" + i + ">@attribute");
            int _attribute_color_ValueInt;
            int.TryParse(_attribute_color_String, out _attribute_color_ValueInt);

            GamePrefs.SCENE_COLORS[_attribute_color_ValueInt] = true;


        }

        //Objectives
        string level_objective = LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL +">0>@attribute_objective");
        GamePrefs.LEVEL_OBJECTIVE = level_objective;

        //Number of Bubbles in level
        string number_of_bubbles = LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>@numberOfBubbles");
        int attribute_num_of_bubbles;
        int.TryParse(number_of_bubbles, out attribute_num_of_bubbles);
        if(attribute_num_of_bubbles>0)
        GamePrefs.NO_OF_BUBBLES = attribute_num_of_bubbles;
        GamePrefs.temp_no_of_bubbles = attribute_num_of_bubbles;


        //Number of Birds to rescue in a level
        //string number_of_birds = LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>@BirdsToFree");
        //int attribute_num_of_birds;
        //int.TryParse(number_of_birds, out attribute_num_of_birds);
        //if (attribute_num_of_birds > 0)
        //    GamePrefs.NO_OF_BIRDS = attribute_num_of_birds;
        //print(GamePrefs.NO_OF_BIRDS);

    }
    void Start()
    {
        PopulateGrid();
    }

   void PopulateGrid()
    {
       

        int _numOf_Coordinates = LevelXML.GetNodeList("LevelsContainer>0>Coordinates>0>Level"+GamePrefs.CURRENT_LEVEL+">0>value").Count;

        for(int i=0; i<_numOf_Coordinates;i++)
        {
            string _attribute_x_ValueString = LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>value>" + i + ">@attribute_i");
            int _attribute_x_ValueInt;
            int.TryParse(_attribute_x_ValueString, out _attribute_x_ValueInt);

            string _attribute_y_ValueString = LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>value>" + i + ">@attribute_j");
            int _attribute_y_ValueInt;
            int.TryParse(_attribute_y_ValueString, out _attribute_y_ValueInt);


            BubbleProperties b_property=GameManager.FindObjectOfType<GameManager>().MatrixGrid[_attribute_x_ValueInt][_attribute_y_ValueInt].GetComponent<BubbleProperties>();

            GameManager.FindObjectOfType<GameManager>().MatrixGrid[_attribute_x_ValueInt][_attribute_y_ValueInt].GetComponent<BubbleProperties>().isEmpty = false;
            switch (LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>value>" + i + ">@attribute_color"))
            {
                    
                case "Green":
                    b_property.BubbleColor = ColorProperty.Green;
                    break;
                case "Yellow":
                    b_property.BubbleColor = ColorProperty.Yellow;
                    break;
                case "Blue":
                    b_property.BubbleColor = ColorProperty.Blue;
                    break;
                case "Red":
                    b_property.BubbleColor = ColorProperty.Red;
                    break;
                case "Purple":
                    b_property.BubbleColor = ColorProperty.Purple;
                    break;
                case "Black":
                    b_property.BubbleColor = ColorProperty.Black;
                    break;
            }

           //string isBirdy= LevelXML.GetValue("LevelsContainer>0>Coordinates>0>Level" + GamePrefs.CURRENT_LEVEL + ">0>value>" + i + ">@Bird");
           ////print("x " +_attribute_x_ValueInt);
           ////print("y" + _attribute_y_ValueInt);
           ////print(isBirdy);
           // if(isBirdy=="1")
           // {
           //     GameManager gameManager = GameManager.FindObjectOfType<GameManager>();
           //     gameManager.MatrixGrid[_attribute_x_ValueInt][_attribute_y_ValueInt].GetComponent<BubbleProperties>().isBird = true;
           //     BubbleProperties b_properties = gameManager.MatrixGrid[_attribute_x_ValueInt][_attribute_y_ValueInt].GetComponent<BubbleProperties>();
           //     b_properties.Native_Bird_Object = GameObject.Find("ObjectPooling_" + b_properties.BubbleColor + "_Bird").GetComponent<ObjectPoolScript>().GetPooledObject();
           //     b_properties.Native_Bird_Object.transform.parent = gameManager.MatrixGrid[_attribute_x_ValueInt][_attribute_y_ValueInt].transform.parent;
           //     b_properties.Native_Bird_Object.transform.position = gameManager.MatrixGrid[_attribute_x_ValueInt][_attribute_y_ValueInt].transform.position;
           //     b_properties.Native_Bird_Object.transform.localScale = gameManager.MatrixGrid[_attribute_x_ValueInt][_attribute_y_ValueInt].transform.localScale;
           //     b_properties.Native_Bird_Object.SetActive(true);
           // }
        }
    }
}
