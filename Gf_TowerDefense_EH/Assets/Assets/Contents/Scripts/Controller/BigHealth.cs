using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHealth : MonoBehaviour
{
    public int health = 4;
    void Update()
    {
        if(health <= 0)
        {
            Object.Destroy(gameObject);
            LevelController.instance.AddCoins();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("got hit!");
            health -= 2;
        }
    }
}
