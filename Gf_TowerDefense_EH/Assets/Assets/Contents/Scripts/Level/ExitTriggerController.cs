using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTriggerController : MonoBehaviour
{
    public LevelController relatedLevel;

    void Update()
    {
        OnLevelFailedCheck();
    }

    public void OnLevelFailedCheck()
    {
        if(relatedLevel.LevelFailedCheck == true)
        {
            Debug.Log("Is true");
            relatedLevel.LevelFailed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Enemy") )
            relatedLevel.EnemyEscaped(other.gameObject);
    }
}
