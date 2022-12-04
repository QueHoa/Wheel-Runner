using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 0;//left = -1, middle = 0, right = 1
    public float speed;
    public float laneDistance = 4;//khoảng cách giữa hai làn đường
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = speed;
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
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);//nhân vật di chuyển
    }
}
