using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private float moveSpeed = 3f;

    private Vector2 inputVector;
    private Vector3 lastMoveDirection;


    private void Update()
    {
        HandleMovement();
    }



    private void HandleMovement()
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        inputVector = ReturnInputVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != lastMoveDirection)
        {
            lastMoveDirection = moveDir;
        }
    }

    private Vector2 ReturnInputVector()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            input.x += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.x += -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input.y += 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.y += -1;
        }

        return input;
    }

}
