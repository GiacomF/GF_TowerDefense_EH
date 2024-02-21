using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTriggerController : MonoBehaviour
{
    public LevelController relatedLevel;

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Enemy") )
            relatedLevel.EnemyEscaped(other.gameObject);
    }
}
