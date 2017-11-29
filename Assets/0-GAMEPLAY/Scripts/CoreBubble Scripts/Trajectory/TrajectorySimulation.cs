using UnityEngine;

/// <summary>
/// Controls the Laser Sight for the player's aim
/// </summary>
public class TrajectorySimulation : MonoBehaviour
{
    private float rotationSpeed = 170.0f;       // Pointer Rotation Speed
    private float maxLeftAngle = 67.5f;         // Max Left pointer Angle
    private float maxRightAngle = 294.5f;       // Max Right pointer Angle

    public ObjectPoolScript pooler;

    public GameObject pooled_Trajectory,parent;
    public Vector3 spawnPos;
    void Start()
    {
        spawnPos = pooled_Trajectory.transform.position;
        InvokeRepeating("SimulateTrajectory", 0.01f, 0.1f);
    }
    void SimulateTrajectory()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        //if (transform.eulerAngles.z > this.maxLeftAngle && transform.eulerAngles.z < 180.0)
        //{
        //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, maxLeftAngle);
        //}
        //if (transform.eulerAngles.z < this.maxRightAngle && transform.eulerAngles.z > 180.0)
        //{
        //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, maxRightAngle);
        //}
        Vector2 velocity = pooled_Trajectory.transform.rotation*parent.transform.up * 100;
        pooled_Trajectory.gameObject.GetComponent<Rigidbody2D>().AddForce(velocity * 40);
        GameObject temp = pooler.GetPooledObject();
        temp.SetActive(true);

        temp.transform.parent = pooled_Trajectory.transform.parent;
        temp.transform.localScale = pooled_Trajectory.transform.localScale;
        temp.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        pooled_Trajectory = temp;
    }
}