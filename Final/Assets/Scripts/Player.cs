using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IObserver
{
    public float jumpForce = 2.0f;
    public float gravityScale = 5f;
    public float speed = 6f;

    public Rigidbody player;
    public Vector3 jump;

    public bool isGrounded;
    public PlayerAnimation playerAnimation;
    Collider collider;
    Collider comboCollider;
    public GameObject stump;
    public AudioSource audioSource;
    private bool isAttacking = false;
    private Animator animator;
    public AudioClip punch;
    public AudioClip collect;
    public AudioClip moveClip;
    public AudioClip danceClip;
    public AudioClip jumpClip;
    public GameManager gameManager;
    public GameObject vfx;
    public Text txt;

    private int combo = 0;
    bool canPunch;
    bool canKick;
    bool canComboAttack;
    bool attacked = false;
    int lives = 3;
    float waitTime = 5;
    private void Start()
    {
        player = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        collider = GetComponent<CapsuleCollider>();
        comboCollider = GetComponent<BoxCollider>();
        collider.enabled = false;
        comboCollider.enabled = false;
        audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        if(SceneManager.GetActiveScene().name == "GameO")
        {
            canPunch = true;
            canKick = false;
            canComboAttack = false;
        }
        else
        {
            canPunch = true;
            canKick = true;
            canComboAttack = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            player.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
           // playerAnimation.isJumping(true);
        }
       /* else
        {
            playerAnimation.isJumping(false);
        }*/

        /*if (Input.GetKeyDown(KeyCode.K))
        {
            playerAnimation.isDancing(true);
            Invoke("ResetDance", 2);
        }*/
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isAttacking = true;
            collider.enabled = true;
            collider.tag = "Punch";
           //playerAnimation.isAttacking(true);
            Invoke("ResetAttack", 1);
            combo = 1;
            Invoke("ResetComboMid", 2);
        }
        if (Input.GetKeyDown(KeyCode.E) && canKick)
        {
            isAttacking = true;
            collider.enabled = true;
            collider.tag = "Kick";
            //playerAnimation.isAttacking(true);
            Invoke("ResetAttack", 1);
            if(combo == 1)
            {
                combo = 2;
                Invoke("ResetComboMid", 2);
            }
            else
            {
                combo = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && canComboAttack)
        {
            isAttacking = true;
            comboCollider.enabled = true;
            comboCollider.tag = "Combo";
            if (combo == 2)
            {
                combo = 3;
            }
            else
            {
                combo = 0;
            }
            Instantiate(vfx, this.transform.position + Vector3.forward, Quaternion.identity);
            Invoke("ResetCombo", 3);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(txt)
         txt.text = lives.ToString();
    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        player.MovePosition(transform.position + dir * Time.deltaTime * speed);
        if (dir.sqrMagnitude > 0.001f)
        { // If-check as a rotation that looks in no direction isn't valid
            Quaternion forwardsRotation = Quaternion.LookRotation(dir);
            player.MoveRotation(forwardsRotation);
        }
        
       /* if (dir.magnitude < 0.001f)
        {
            playerAnimation.isMoving(false);
        }
        else if (dir.magnitude > 0.001f)
        {
            playerAnimation.isMoving(true);
        }*/
       // playerAnimation.isDancing(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mushroom")
        {
            playerAnimation.isDancing(true);
            new WaitForSeconds(2f);
            //Invoke("ResetDance", 2);
            Destroy(collision.gameObject);
        }
        /*if (collision.gameObject.tag == "Enemy" && isAttacking)
        {
            new WaitForSeconds(1f);
            Destroy(collision.gameObject, 1f);
        }*/
        if (collision.gameObject.tag == "tree" && isAttacking)
        {
            new WaitForSeconds(1f);
            Destroy(collision.gameObject, 1f);
            stump.transform.localScale = new Vector3(4.0f,4.0f,4.0f);
            Instantiate(stump, collision.gameObject.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "Water" || collision.gameObject.tag == "Door0")
        {
            SceneManager.LoadScene("Game1");
        }

        if (collision.gameObject.tag == "Door1" || collision.gameObject.tag == "Water1")
        {
            SceneManager.LoadScene("Game2");
        }

        if (collision.gameObject.tag == "Door2" || collision.gameObject.tag == "Water2")
        {
            SceneManager.LoadScene("Game3");
        }

        if (collision.gameObject.tag == "Door3")
        {
            SceneManager.LoadScene("Win");
        }

    }

    public bool canCombo()
    {
        return (combo == 3);
    }

    public bool canKickAttack()
    {
        return canKick;
    }
    public void ResetDance()
    {
        new WaitForSeconds(2f);
        playerAnimation.isDancing(false);
    }

    private void ResetAttack()
    {
        collider.enabled = false;
        //playerAnimation.isAttacking(false);
        isAttacking = false;
    }

    private void ResetCombo()
    {
        combo = 0;
        comboCollider.enabled = false;
        //playerAnimation.isAttacking(false);
        isAttacking = false;
    }

    private void ResetComboMid()
    {
        if(combo!=3)
            combo = 0;
       
    }

    public void OnNotify(int id)
    {
        if (id == 2)
        {
            canKick = true;
        } 
        else if(id == 3)
        {
            canComboAttack = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack")
        {
            if (!attacked)
            {
                attacked = true;
                new WaitForSeconds(1f);
                if (lives > 1)
                    lives--;
                else
                {
                    SceneManager.LoadScene("Game1");
                }
            }
            else
            {
                if(waitTime < 0)
                {
                    waitTime = 5;
                    attacked = false;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}
