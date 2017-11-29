using UnityEngine;
using System.Collections;

public class LineTrailer : MonoBehaviour {

	void OnEnable()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Invoke("Destroy", 1.5f);
    }


    void Destroy()
    {
        gameObject.SetActive(false);
    }


    void OnDisable()
    {
        CancelInvoke();
    }
}
