using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Aim_and_Shoot : MonoBehaviour {

    public bool isAiming,ReleasePowerButton;               // If true pointer moves with touch drag
    public Rigidbody2D projectile;      // New Bubble's rigid body component
    public Transform Spawnpoint,Parent; // Currently bubble ready to be fired in spawnpoint,
                                        // The Parent is Aim Assist
    public Bubble_Count B_count;


    //private float rotationSpeed = 170.0f;       // Pointer Rotation Speed
    //private float maxLeftAngle = 85.0f;         // Max Left pointer Angle
    //private float maxRightAngle = 275.0f;       // Max Right pointer Angle
    private Vector3 spawnPos;                   // SpawnPosition to be stored in it so that next coming bubble prefab is instantiated at this point
    [SerializeField] private bool lockInstance=false;            // Lock next bubble until first is placed on grid
    private GameManager gameManager;

    private float rotationSpeed = 170.0f;       // Pointer Rotation Speed
    private float maxLeftAngle = 67.5f;         // Max Left pointer Angle
    private float maxRightAngle = 294.5f;       // Max Right pointer Angle


    public Transform target;
    private Vector3 v_diff;
    private float atan2;
    private bool isLevel_Cleared;

    // Poolers

    public ObjectPoolScript BubblePooler;
    public ObjectPoolScript RemainingPooler;


    //Physics Materials
    public PhysicsMaterial2D bubble_physics;


    // Upcomings and Currents

    public GameObject Upcoming1, Upcoming2;

    //

    public GameObject Special_Button;


    // shoot and empty canon

    int Projectiles_Instantiated = 0;
 


    //Ad Available
    public bool VideoAvailable;
    

    void Awake()
    {

        rotationSpeed=110;

        #if UNITY_EDITOR
                rotationSpeed=300;
        #endif
                gameManager = GameManager.FindObjectOfType<GameManager>();
    }
    void Start()
    {
        spawnPos = Spawnpoint.transform.position;
    }


    void Update()
    {
        if (isAiming)
        {
           
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
            if (transform.eulerAngles.z > this.maxLeftAngle && transform.eulerAngles.z < 180.0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, maxLeftAngle);
            }
            if (transform.eulerAngles.z < this.maxRightAngle && transform.eulerAngles.z > 180.0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, maxRightAngle);
            }



        }

    }


    void OnEnable()
    {
        gameManager.OnGridPlacement += this.OnGridPlacement;
        gameManager.OnLevel_Cleared += this.OnLevel_Cleared;
        gameManager.OnSpecialBubbleSelected += this.OnSpecialBubbleSelected;
    }

    void OnDisable()
    {
        gameManager.OnGridPlacement -= this.OnGridPlacement;
        gameManager.OnLevel_Cleared -= this.OnLevel_Cleared;
        gameManager.OnSpecialBubbleSelected -= this.OnSpecialBubbleSelected;
    }
    
    public void StartAiming()           // Release Aim locking 
    {
        
        isAiming = true;
        //print("helloo");
    }
    
    public void OutOfBounds()           // If user touch drag during aiming goes in screen corners
    {
        //print("Out OF Bounds......");
        isAiming = false;
        //this.GetComponent<AudioSource>().Stop();
    }

    public void DragCannon()
    {
        //if(!this.GetComponent<AudioSource>().isPlaying)
        //{
        //    this.GetComponent<AudioSource>().Play();
        //}
    }
    public void DragEndCannon()
    {
        
            //this.GetComponent<AudioSource>().Stop();
        
    }

    public void ShootProjectile()            // Stops Aiming and fires if the isAiming bool was true
    {
        //this.GetComponent<AudioSource>().Stop();
        if (isAiming & !lockInstance && GamePrefs.NO_OF_BUBBLES>0 && !isLevel_Cleared)
        {
            SoundManager.Instance.ClickCannonShot();
            GamePrefs.NO_OF_BUBBLES--;
            //print(GamePrefs.NO_OF_BUBBLES);
            gameManager.OnTouchDown();
            // projectile.velocity = Spawnpoint.TransformDirection(this.transform.up * Time.deltaTime * 1000f);
            Vector2 velocity = Parent.transform.up * 100;
            projectile.AddForce(velocity * 40);
            isAiming = false;
            lockInstance = true;
            //Special_Button.GetComponent<Button>().interactable = true;
            
        }
        
    }


    public void RewardedVideoLoaded()
    {
        VideoAvailable = true;
        Debug.Log("Rewared Video Loaded");
    }
    void OnGridPlacement()
    {
        
        GameObject temp = (GameObject)Instantiate(Resources.Load("Bubble2D"),spawnPos,Quaternion.identity);      // Instantiate new bubble 
        temp.transform.parent = Spawnpoint.transform.parent;
        //temp.GetComponent<SpriteRenderer>().sprite = null;
        Destroy(Spawnpoint.gameObject);                                             // Destroy Current bubble if placed on grid
        Destroy(projectile);                                                        // Destroy its rigidbody2d
        Spawnpoint = temp.transform;                                                // Replaces old spawnpoint transform with new
        projectile = temp.GetComponent<Rigidbody2D>();
        lockInstance = false;      
                                                 // Release next bubble to set it ready for shoot


        if(GamePrefs.NO_OF_BUBBLES==0 && !isLevel_Cleared )
        {
            if (PlayerPrefs.GetInt("NO_OF_COINS") >= 25)
            {
                //gameManager.PanelEnabler.GetComponent<Activators>().ActiveBuyBubbleStore();

            }
            else
            {

               // gameManager.OnLevelFailed();
            }
            // Buy Menu
        }
        
    }




   
    void OnLevel_Cleared()
    {
        isLevel_Cleared = true;
        //print("should stop");
        //Time.timeScale = 0.2f;
        StartCoroutine("WaitTillFallingBubbles");
    }


    void OnSpecialBubbleSelected()
    {
        Special_Button.GetComponent<Button>().interactable = false;
    }
    void ShootTillCannonEmpty()
    {
        if (GamePrefs.NO_OF_BUBBLES > 0)
        {
            
            GamePrefs.NO_OF_BUBBLES--;
            
            //B_count.UpdateCount();
            if (GamePrefs.NO_OF_BUBBLES==1)
            {
                Upcoming1.GetComponent<SpriteRenderer>().enabled = false;
                Upcoming2.GetComponent<SpriteRenderer>().enabled = false;
            }
            
            Spawnpoint.gameObject.layer = LayerMask.NameToLayer("Falling");
            Destroy(Spawnpoint.GetComponent<Obstacle>());
            Spawnpoint.GetComponent<SpriteRenderer>().sortingOrder = 55;
            Spawnpoint.GetComponent<CircleCollider2D>().sharedMaterial = bubble_physics;
            Spawnpoint.GetComponent<Rigidbody2D>().mass = 0.8f;
            Spawnpoint.GetComponent<Rigidbody2D>().angularDrag= 0.0f;
            Spawnpoint.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
            Vector2 velocity = Spawnpoint.transform.rotation * Parent.transform.up * 130;
            
            //Spawnpoint.gameObject.layer = LayerMask.NameToLayer("Falling");
            Projectiles_Instantiated++;
            Spawnpoint.name = "Wasted" + Projectiles_Instantiated;
            //print("projectile instantiated "+Spawnpoint.name);
            SoundManager.Instance.ClickCannonShot();
            projectile.AddForce(velocity * 40);
            Invoke("DelayForSelfCollision", 0.03f);
            //Spawnpoint.GetComponent<CircleCollider2D>().isTrigger = false;









            //GameObject temp = RemainingPooler.GetPooledObject();   // Instantiate new bubble 
            //temp.SetActive(true);
            ////Spawnpoint.gameObject.layer = LayerMask.NameToLayer("Falling");
            
            
            //temp.GetComponent<SpriteRenderer>().sprite = null;
            //temp.GetComponent<CircleCollider2D>().isTrigger = true;
            //Spawnpoint = temp.transform;                                                // Replaces old spawnpoint transform with new
            //projectile = temp.GetComponent<Rigidbody2D>();
            //if (GamePrefs.NO_OF_BUBBLES == 0)
            //{
            //    Spawnpoint.GetComponent<SpriteRenderer>().enabled = false;
            //}
        }
        else
        {

            StartCoroutine("WaitForClearScreen");
            CancelInvoke("ShootTillCannonEmpty");
        }
    }


    
    void DelayForSelfCollision()
    {
        Spawnpoint.GetComponent<CircleCollider2D>().isTrigger = false;
        GameObject temp = RemainingPooler.GetPooledObject();   // Instantiate new bubble 
        temp.SetActive(true);
        //Spawnpoint.gameObject.layer = LayerMask.NameToLayer("Falling");


        temp.GetComponent<SpriteRenderer>().sprite = null;
        temp.GetComponent<CircleCollider2D>().isTrigger = true;
        Spawnpoint = temp.transform;                                                // Replaces old spawnpoint transform with new
        projectile = temp.GetComponent<Rigidbody2D>();
        if (GamePrefs.NO_OF_BUBBLES == 0)
        {
            Spawnpoint.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    IEnumerator WaitTillFallingBubbles()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => BubblePooler.Deactive_Count == BubblePooler.pooledObjects.Count);
        //print("all deactivated");
        if (GamePrefs.NO_OF_BUBBLES > 0)
        {
            //Time.timeScale = 0.1f;
            InvokeRepeating("ShootTillCannonEmpty", 0f, 0.2f);
        }
        else
        {
            gameManager.OnShootCannonEmpty();
        }
    }


    IEnumerator WaitForClearScreen()
    {
        yield return new WaitUntil(() => GamePrefs.temp_remaning_projectiles_shot==Projectiles_Instantiated-1 );
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.ClickLevelCompleteSound();
#if UNITY_IOS

        AGameUtils.LogAnalyticEvent ("Level" + (GamePrefs.CURRENT_LEVEL + 1) + " : " + "Level_Completed");
#endif
        Upcoming2.GetComponent<SpriteRenderer>().enabled = false;
        gameManager.OnShootCannonEmpty();
    }
}
