using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float laneDis = 4;
    public float jumpPower;

    private CharacterController controller;
    private Vector3 direction;
    private float lane = 0;//left:-1 middle:0 right:1
    private float gravity = -20;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = speed;
        if (controller.isGrounded)
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
        //transform.position = targetPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, 50 * Time.fixedDeltaTime);
        
    }
    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Jump()
    {
        direction.y = jumpPower;
    }
}
