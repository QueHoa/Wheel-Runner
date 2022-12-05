using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 0;//left = -1, middle = 0, right = 1
    private Vector3 oldPosition;

    public float gravity = -20;//trọng lực khi nhảy
    public float speed;
    public float laneDistance = 4;//khoảng cách giữa hai làn đường
    public float jumpPower;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = speed;        
        controller.Move(direction * Time.deltaTime);

        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 2) desiredLane = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -2) desiredLane = -1;
        }       

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        
        if (desiredLane == -1)
        { 
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 1)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 50 * Time.deltaTime);
    }
    private void Jump()
    {
        direction.y = jumpPower;
    }
}
