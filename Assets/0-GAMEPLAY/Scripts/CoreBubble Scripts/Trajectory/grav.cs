using UnityEngine;
using System.Collections;

public class grav : MonoBehaviour
{

    public Vector3 StartVelocity;
    public float PredictionTime;
    private Vector3 G;

    void OnDrawGizmos()
    {
        if (G == Vector3.zero)
        {
            // a hacky way of making sure this gets initialized in editor too...
            // this assumes 60 samples / sec
            G = new Vector3(0, -3f, 0) / 360f;
        }
        Vector3 momentum = StartVelocity;
        Vector3 pos = gameObject.transform.position;
        Vector3 last = gameObject.transform.position;
        for (int i = 0; i < (int)(PredictionTime * 60); i++)
        {
            momentum += G;
            pos += momentum;
            Gizmos.DrawLine(last, pos);
            last = pos;
        }

    }
}