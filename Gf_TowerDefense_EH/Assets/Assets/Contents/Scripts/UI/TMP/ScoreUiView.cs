using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUiView : MonoBehaviour
{
    //1 Make it a singleton
    public static ScoreUiView instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindAnyObjectByType<ScoreUiView>();

            return _instance;
        }
    }    

    private static ScoreUiView _instance;
    //

    //Use TextMeshPro to create a text
    public TMP_Text scoreText;
    //Score is equal to a string called "score"
   public void SetScore(string score)
   {
        scoreText.text = score;
   }

}
