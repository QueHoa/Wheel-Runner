using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float laneDis = 4;
    public float jumpPower;

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public Animator anim;
    public AudioClip scoreClip;
    public AudioClip dieClip;

    private AudioSource audioSource;
    private CharacterController controller;
    private Vector3 direction;
    private float lane = 0;//left:-1 middle:0 right:1
    private float gravity = -22;
    private bool isSliding = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameStart)
        {
            return;
        }
        if (speed < maxSpeed)
        {
            speed += speed * 0.2f * Time.deltaTime;
        }
        anim.SetBool("IsGameStart", true);
        direction.z = speed;
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
        anim.SetBool("IsGrounded", isGrounded);
        if (isGrounded)
        {
            //direction.y = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            StartCoroutine(Slide());
        }
        //controller.Move(direction * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane++;
            if (lane == 2) lane = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane--;
            if (lane == -2) lane = -1;
        }
       

        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lane == -1)
        {
            targetPos += Vector3.left * laneDis;

        }else if (lane == 1)
        {
            targetPos += Vector3.right * laneDis;
        }       
        //transform.position = Vector3.Lerp(transform.position, targetPos, 50 * Time.fixedDeltaTime);
        if(transform.position == targetPos)
        {
            return;
        }
        Vector3 diff = targetPos - transform.position;
        Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }
    void FixedUpdate()
    {
        if (!GameManager.isGameStart)
        {
            return;
        }
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Jump()
    {
        direction.y = jumpPower;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            audioSource.clip = dieClip;
            audioSource.Play();
            GameManager.gameOver = true;
            GameManager.audioSource.Play();
        }
    }
    private IEnumerator Slide()
    {
        isSliding = true;
        anim.SetBool("IsSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(0.7f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        anim.SetBool("IsSliding", false);
        isSliding = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        audioSource.clip = scoreClip;
        audioSource.Play();
    }
}
