using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LeaderBoardScoreManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

   public void SubmitScore() 
    {
        if (int.TryParse(inputScore.text, out int score)) 
        {
            submitScoreEvent.Invoke(inputName.text, score);
        } 
        else 
        {
            Debug.LogError("Invalid score input");
        }
    }
}
