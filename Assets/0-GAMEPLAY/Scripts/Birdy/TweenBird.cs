using UnityEngine;
using System.Collections;

public class TweenBird : MonoBehaviour {

    public bool tweener = false;
    public Vector3 target;
    public int rand;
    
    void OnEnable()
    {
        rand = Random.Range(-1, 1);

        if(this.transform.position.x>=0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            rand = -1;
        }
        else
        {
            rand = 1;
        }
        
        
            target = new Vector3(transform.position.x + (rand)*10, this.transform.position.y, transform.position.z);
        
    }
    void Update()
    {
        if(tweener)
        {
            //tweener = false;
            //iTween.MoveBy(gameObject, iTween.Hash("x", 7,"time",6f));
            if (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, 3 * Time.deltaTime);
            }
            else
            {
                tweener = false;
            }
        }
    }
}
