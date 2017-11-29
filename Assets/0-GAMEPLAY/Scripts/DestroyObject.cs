using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Destroy_this", 0.5f);
	}
	
	void Destroy_this()
    {
        Destroy(this.gameObject);
    }
}
