using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public float movementSpeed = 5f;
    public float stopDistance = 0.1f;

    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition.position;
     
    }

    // Update is called once per frame
    void Update()
    {
        direction = endPosition.position - transform.position;
        float distance = direction.sqrMagnitude;
        direction.Normalize();      // direction = direction.normalized;

        Debug.DrawLine(transform.position, transform.position+direction * 5, Color.red);

        if (distance > stopDistance) {
            transform.Translate(direction * Time.deltaTime * movementSpeed);
        }

    }
}
