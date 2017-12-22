using UnityEngine;




public class RayCastReflection : MonoBehaviour
{
    //this game object's Transform
    public Transform goTransform;
    public GameObject SecondRay;
    //a ray
    private Ray2D ray;
    //a RaycastHit variable, to gather informartion about the ray's collision
    private RaycastHit2D hit, hit2, hit3;

    //reflection direction
    private Vector3 inDirection;
    private LineRenderer LR, LR2;


    private Vector3[] Positions;


    private float min = 0;
    private float max = 0.8f;
    float t = 0f;




    void Awake()
    {
        LR = this.GetComponent<LineRenderer>();
        LR2 = SecondRay.GetComponent<LineRenderer>();
        Positions = new Vector3[3];

    }
    void FixedUpdate()
    {

        ray = new Ray2D(goTransform.position, goTransform.up);
        LR2.SetVertexCount(0);

        //Check if the ray has hit something
        hit = Physics2D.Raycast(goTransform.position, this.transform.up, 50, 1 << LayerMask.NameToLayer("UI"));
        LR.SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (-5)));
        Positions[0] = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (-5));

        if (hit.collider.tag == "TopBorder")
        {
            Debug.DrawRay(goTransform.position, hit.point - (Vector2)goTransform.position, Color.blue);

        }
        else //cast the ray 100 units at the specified direction
        {
            Debug.DrawRay(goTransform.position, hit.point - (Vector2)goTransform.position, Color.blue);


            if (hit.collider.name == "RightBoundry")
            {
                inDirection = Vector3.Reflect(ray.direction + (new Vector2(0.3f, 0.3f)), hit.normal);
            }
            else if (hit.collider.name == "LeftBoundry")
            {
                inDirection = Vector3.Reflect(ray.direction + (new Vector2(0.02f, 0.02f)), hit.normal);

            }
            //the reflection direction is the reflection of the current ray direction flipped at the hit normal

            //cast the reflected ray, using the hit point as the origin and the reflected direction as the direction
            ray = new Ray2D(hit.point, inDirection);


            Debug.DrawRay(hit.point, hit.normal * 3, Color.red);

            ray.direction = ray.direction * -1;
            //hit2 = Physics2D.Raycast(hit.point + ray.direction * 2f, ray.direction, 50f, 1 << LayerMask.NameToLayer("UI"));
            hit2 = Physics2D.Raycast(hit.point + ray.direction * 2f, ray.direction, 50f, 1 << LayerMask.NameToLayer("UI"));


            //represent the ray using a line that can only be viewed at the scene tab
            Debug.DrawRay(hit.point, ray.direction, Color.blue);

            if (hit2)
            {
                print(hit2.collider.name);
                //hit3 = Physics2D.Raycast(hit.point + ray.direction * 2f, -ray.direction, 50f, 1 << LayerMask.NameToLayer("UI"));



                //LR2.SetVertexCount(2);
                //LR2.SetPosition(0, hit.point);
                //LR2.SetPosition(1, hit.point + ray.direction * 20f);
                
                    LR2.SetVertexCount(2);
                    LR2.SetPosition(0, hit2.point);
                    LR2.SetPosition(1, hit2.point + -(ray.direction) * 20f);
                

            }
            else
            {
                LR2.SetVertexCount(0);
            }




        }

        LR.SetPosition(1, transform.up * 20 + transform.position);


        this.GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(Mathf.Lerp(max, min, t), 0f);

        t += 0.15f * Time.deltaTime;
        if (t >= 1f)
        {
            t = 0;
        }



    }
}