using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;

    //public GameObject pauseMenu;

    public AudioSource jumpAudio;
    public AudioSource hurtAudio;

    public GameObject life1, life2, life3;
    public int lifes = 6;

    private Rigidbody2D myRigbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    public bool isHurt;
    private bool isTextDisplayed = false;

    [Header("Hook Settings")]
    public LineRenderer lineRenderer;
    public Transform hookPoint; // 钩子的最终位置
    public float hookForce = 10f;
    public float hookDuration = 2f; // y秒，钩锁保持的时间
    public float hookCooldown = 5f; // z秒，钩锁的冷却时间
    private bool canUseHook = true;
    private bool isHooked = false; // 检查玩家是否被勾住
    private Vector2 hookDirection;

    [SerializeField] TextMeshProUGUI fallText; 
    [SerializeField] float fallDistanceThreshold = 3f;
    [SerializeField] float checkTimeDuration = 3f;
    [SerializeField] float textDisplayTime = 3f;
    private float timeSinceLastCheck = 0f;
    private Vector3 positionAtLastCheck; 
    private List<string> messages = new List<string> 
{
    "Oof, you lost a lot of progress. That’s a deep frustration, a real punch in the gut.",
    "Oh no, it happened again. Keep on trying, don’t let it get to you.",
    "The pain I feel now is the happiness I had before.",
    "Whenever I climb, I am followed by a dog called Ego.",
    "The soul would have no rainbow had the eyes no tears.",
    "To live is to suffer. To survive is to find meaning in the suffering.",
    "Of all sad words of tongue or pen, the saddest are these, 'It might have been'.",
    "There are no regrets in life, just lessons.",
    "Your failure here is a metaphor. To learn for what, please resume climbing.",
    "There I was again tonight, forcing laughter, faking smiles.",
};

    void Start()
    {
        myRigbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        positionAtLastCheck = transform.position;
    }

    void Update()
    {
            Run();
            Filp();
            Jump();
            CheckGrounded();
            SwitchAnimation();
            CheckFallDistance();
        if (Input.GetMouseButtonDown(0) && canUseHook)
            
        {
            FireHook();
            Debug.Log("mouse clicked");
        }

        if (isHooked)
        {
            PullPlayerToHookPoint();
        }

    }

     void CheckGrounded()
     {
         isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
         //Debug.Log(isGround);
     }

     void Filp()
     {
         bool playerHasXAixsSpeed = Mathf.Abs(myRigbody.velocity.x) > Mathf.Epsilon;
         if(playerHasXAixsSpeed)
         {
             if(myRigbody.velocity.x > 0.1f)
             {
                 transform.localRotation = Quaternion.Euler(0, 0, 0);
             }
             if (myRigbody.velocity.x < -0.1f)
             {
                 transform.localRotation = Quaternion.Euler(0, 180, 0);
             }
         }
     }

     void Run()
     {
         float moveDir = Input.GetAxis("Horizontal");
         Vector2 playerVe1 = new Vector2(moveDir * runSpeed, myRigbody.velocity.y);
         myRigbody.velocity = playerVe1;
         bool playerHasXAxisSpeed = Mathf.Abs(myRigbody.velocity.x) > Mathf.Epsilon;
         myAnim.SetBool("Run", playerHasXAxisSpeed);
     }

     void Jump()
     {
         if(Input.GetButtonDown("Jump"))
         {
             if (isGround)
             {
                 jumpAudio.Play();
                 myAnim.SetBool("Jump", true);
                 Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                 myRigbody.velocity = Vector2.up * jumpVel;
                 canDoubleJump = true;
             }
             else
             {
                 if(canDoubleJump)
                 {
                     jumpAudio.Play();
                     myAnim.SetBool("DoubleJump", true);
                     Vector2 doubleJumpVe1 = new Vector2(0.0f, doubleJumpSpeed);
                     myRigbody.velocity = Vector2.up * doubleJumpVe1;
                     canDoubleJump = false;
                 }
             }
         }
     }
     

     void SwitchAnimation()
     {
         myAnim.SetBool("Idle", false);

         if (myAnim.GetBool("Jump"))
         {
             if (myRigbody.velocity.y < 0.0f)
             {
                 myAnim.SetBool("Jump", false);
                 myAnim.SetBool("Fall", true);
                 if (myAnim.GetBool("Hurt"))
                 {
                     myAnim.SetBool("Hurt", true);
                     myAnim.SetBool("Fall", false);
                 }
             }
         }
         else if (myRigbody.velocity.y < 0 && !isGround) 
         {
             myAnim.SetBool("Idle", false);   
             myAnim.SetBool("Fall", true);
         }
         else if (isHurt)
         {
             hurtAudio.Play();
             myAnim.SetBool("Hurt", true);
             if (Mathf.Abs(myRigbody.velocity.x) < 0.1f)
             {
                 myAnim.SetBool("Idle", true);
                 myAnim.SetBool("Hurt", false);
                 isHurt = false;
             }
         }
         else if (isGround)
         {
             myAnim.SetBool("Fall", false);
             myAnim.SetBool("Idle", true);
         }
         if (myAnim.GetBool("DoubleJump"))
         {
             if (myRigbody.velocity.y < 0.6f)
             {
                 myAnim.SetBool("DoubleJump", false);
                 myAnim.SetBool("DoubleFall", true);
             }
         }
         else if (isGround)
         {
             myAnim.SetBool("DoubleFall", false);
             myAnim.SetBool("Idle", true);
         }
     }
    void FireHook()
    {
        
        hookDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        Debug.DrawRay(transform.position, hookDirection * 10f, Color.red, 2f);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + (Vector3)hookDirection * 10); 

        RaycastHit2D hit = Physics2D.Raycast(transform.position, hookDirection, 10f); 

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isHooked = true;
            hookPoint.position = hit.point;
            lineRenderer.SetPosition(1, hookPoint.position);
            StartCoroutine(HookDuration());
        }
        else
        {
            Debug.Log("Did not hit any object.");  
        }
    }

    void PullPlayerToHookPoint()
    {
        Vector2 pullDirection = ((Vector2)hookPoint.position - (Vector2)transform.position).normalized;
        myRigbody.AddForce(pullDirection * hookForce);
    }

    IEnumerator HookDuration()
    {
        yield return new WaitForSeconds(hookDuration);
        isHooked = false;
        lineRenderer.SetPosition(1, transform.position); 
        StartCoroutine(HookCooldown());
    }

    IEnumerator HookCooldown()
    {
        canUseHook = false;
        yield return new WaitForSeconds(hookCooldown);
        canUseHook = true;
    }
    public  void OnTriggerEnter2D(Collider2D collision)
     {
         if(collision.tag == "Object")
         {
             Destroy(collision.gameObject);
             lifes++;
             Life();
         }

         if(collision.tag == "Die")
         {
             Invoke("Restart", 1f);
             //lifes--;
             Life();
         }

         if (collision.tag == "End")
         {
             //Debug.Log(1);
             End();
         }

         if (collision.gameObject.tag == "Spike")
         {
             isHurt = true;
             //lifes--;
             Life();
         }
             if (collision.gameObject.tag == "Linedie")
         {
             isHurt = true;
             //lifes--;
             Life();
         }
     }

     void Restart()
     {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

     void End()
     {
         SceneManager.LoadScene("End");
     }
     void CheckFallDistance()
     {
         timeSinceLastCheck += Time.deltaTime;

         if (timeSinceLastCheck >= checkTimeDuration)
         {
             float distanceFell = positionAtLastCheck.y - transform.position.y;

             if (distanceFell >= fallDistanceThreshold)
             {
                 ShowRandomText();
             }

             positionAtLastCheck = transform.position;
             timeSinceLastCheck = 0f;
         }
     }

     void ShowRandomText()
     {
         if (!isTextDisplayed)
         {
             int randomIndex = Random.Range(0, messages.Count);
             fallText.text = messages[randomIndex];
             fallText.gameObject.SetActive(true);
             isTextDisplayed = true;
             StartCoroutine(HideTextAfterDelay(textDisplayTime));
         }
     }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fallText.gameObject.SetActive(false);
        isTextDisplayed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            if(myAnim.GetBool("Fall"))
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigbody.velocity = Vector2.up * jumpVel;
            }
            else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                myRigbody.velocity = new Vector2(-10, myRigbody.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                myRigbody.velocity = new Vector2(10, myRigbody.velocity.y);
                isHurt = true;
            }
        }

    }

    public void Life()
    {
        if(lifes == 6)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
        else if (lifes == 4)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(false);
        }
        else if (lifes == 2)
        {
            life1.SetActive(true);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        else if (lifes < 1)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
            //pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        /*if(siHurt)
          {
              lifes--;
              life();
          }
         */
        
    }
}
