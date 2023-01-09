using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequence : MonoBehaviour
{
    void Awake()
    {
        GameObject.Find("Timer").GetComponent<Timer>().enabled = false;
        
    }
    void Start()
    {
        PromptScript.instance.updatePrompt("Stavi kutije na mjesto pripadajućim rasporedom. Pazi, svako ponavljanje ima različiti raspored! Imaš 30 sekundi.", 0.5f);
        StartCoroutine(timerStart());
    }

    void Update()
    {
        
    }

    IEnumerator timerStart(){
        yield return new WaitForSeconds(5f);
        GameObject.Find("Timer").GetComponent<Timer>().enabled = true;
    }
}
