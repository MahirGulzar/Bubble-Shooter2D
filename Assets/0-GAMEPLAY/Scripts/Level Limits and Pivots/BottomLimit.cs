using UnityEngine;
using System.Collections;

public class BottomLimit : MonoBehaviour {
    public GameManager _gameManager;
    public int detect_number;

    bool stopDetection=false;

    void OnCollisionEnter2D(Collision2D other)
    {
        print(other.collider.name);
        if(other.gameObject.tag=="BubbleTag")
        {
            print("should fail");
            _gameManager.OnLevelFailed();
        }
    }


 

    void OnTriggerStay2D(Collider2D other)
    {


        if (!other.GetComponent<Collider2D>().isTrigger && other.tag=="BubbleTag"&& !stopDetection)
        {
            print("Collider" + other.name);
            stopDetection = true;
            _gameManager.OnLevelFailed();
        }
        //if (other.gameObject.tag == "BubbleTag")
        //{
        //    print("should fail");
        //    _gameManager.OnLevelFailed();
        //}
    }
}
