using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public Text roundNumber;
    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }
    
    IEnumerator AnimateText()
    {
        roundNumber.text = "0";
        int round = 0;
        yield return new WaitForSeconds(0.7f);
        while (round < PlayerState.rounds)
        {
            round++;
            roundNumber.text = round.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
