using UnityEngine;


/// <summary>
/// Controls the Laser Sight for the player's aim
/// </summary>
public class BasicTrajectorySimulation : MonoBehaviour
{
    // Reference to the LineRenderer we will use to display the simulated path
    public LineRenderer sightLine;


    // Number of segments to calculate - more gives a smoother line
    public int segmentCount = 10;

    // Length scale for each segment
    public float segmentScale = 1;

    // gameobject we're actually pointing at (may be useful for highlighting a target, etc.)
    private Collider2D _hitObject;
    public Collider2D hitObject { get { return _hitObject; } }


    public GameObject parent;

    
    void FixedUpdate()
    {
       // simulatePath();
       // Debug.DrawRay(transform.position, Vector2.up, Color.red,0.2f);
        //Debug.DrawLine(parent.transform.position, parent);

        //Debug.DrawLine(this.transform.position, this.transform.up, Color.red);
        //RaycastHit2D hit = Physics2D.Raycast(parent.transform.position, parent.transform.position - this.transform.position);
        //if (hit.collider != null)
        //{
        //    //print(hit.collider.name);

        //}

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit01 = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
         //------------------------------------------------
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,1<<LayerMask.NameToLayer("Default"));

        //Debug.DrawLine(this.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)-this.transform.position,Color.red);
        //if (hit.collider != null)
        //{
        //    Debug.Log("Target Position: " + hit.collider.gameObject.name);
        //}
 
         

             //foreach (RaycastHit2D asteroidHit in hit)
             //{
                 
             //}
         
    }

    /// <summary>
    /// Simulate the path of a launched ball.
    /// Slight errors are inherent in the numerical method used.
    /// </summary>
    void simulatePath()
    {
        Vector2[] segments = new Vector2[segmentCount];

        // The first line point is wherever the player's cannon, etc is
        segments[0] = transform.position;

        // The initial velocity
        Vector2 segVelocity = transform.up * (100*51) * Time.deltaTime;

        // reset our hit object
        _hitObject = null;

        for (int i = 1; i < segmentCount; i++)
        {
            // Time it takes to traverse one segment of length segScale (careful if velocity is zero)
			float segTime = (segVelocity.sqrMagnitude != 0) ? (100/10) / segVelocity.magnitude : 0;

            // Add velocity from gravity for this segment's timestep
            segVelocity = segVelocity + Physics2D.gravity * segTime;

            // Check to see if we're going to hit a physics object
            RaycastHit2D hit = Physics2D.Raycast(segments[i - 1], segVelocity, 100f);
            if (hit)
			{
                // remember who we hit
                _hitObject = hit.collider;
                print(_hitObject.name);

                //// set next position to the position where we hit the physics object
                //segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
                //// correct ending velocity, since we didn't actually travel an entire segment
                //segVelocity = segVelocity - Physics2D.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;
                //// flip the velocity to simulate a bounce
                //segVelocity = Vector2.Reflect(segVelocity/1.4f, hit.normal);

                ///*
                // * Here you could check if the object hit by the Raycast had some property - was
                // * sticky, would cause the ball to explode, or was another ball in the air for
                // * instance. You could then end the simulation by setting all further points to
                // * this last point and then breaking this for loop.
                // */
            }
            // If our raycast hit no objects, then set the next position to the last one plus v*t
            else
            {
                //segments[i] = segments[i - 1] + segVelocity * segTime;
            }
        }

        // At the end, apply our simulations to the LineRenderer

        // Set the colour of our path to the colour of the next ball
       
		sightLine.SetColors(Color.red, Color.red);

        sightLine.SetVertexCount(segmentCount);
        for (int i = 0; i < segmentCount; i++)
            sightLine.SetPosition(i, segments[i]);
    }
}
//http://wiki.unity3d.com/index.php?title=Trajectory_Simulation