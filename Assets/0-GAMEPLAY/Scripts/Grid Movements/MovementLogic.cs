using UnityEngine;
using System.Collections;

public class MovementLogic : MonoBehaviour {

    public GameObject InitialMid, FinalMid,_Grid;

    private bool step_Completed=true;

    //void Start()
    //{
    //   // iTween.MoveTo(_Grid, iTween.Hash("y", _Grid.transform.position.y -(0.463f), "time", 1f));
    //}

    //// Update is called once per frame
    //void Update () {
	
    //    if(!InitialMid.GetComponent<Pivots>().isFilled && step_Completed && _Grid.transform.position.y>0.4f)
    //    {
    //        step_Completed = false;
    //        //iTween.MoveTo(_Grid, iTween.Hash("y", _Grid.transform.position.y -(0.463f), "time", 1f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
    //        iTween.MoveTo(_Grid, iTween.Hash("y", _Grid.transform.position.y - (0.463f), "time", 1f,"oncomplete","MoveStepComplete"));
    //        // Move Grid
    //    }

    //    //if(iTween.)
    //    //{
    //    //    step_Completed = true;
    //    //}
        
    //}

    //void MoveStepComplete()
    //{
    //    print("completed");
    //    step_Completed = true;
    //    InitialMid.GetComponent<Pivots>().isFilled = false;
    //}


    //IEnumerator MoveGrid
}
