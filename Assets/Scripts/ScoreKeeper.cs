using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    /***
    *		Start is called before the first frame update.
    ***
    void Start()
    {
        
    }   // Start()

    /***
	*		Update is called once per frame.
	***
    void Update()
    {
        
    }   // Update()

	/***
	*		GetQuestionsSeen is called to find out how many questions have been seen.
	***/

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }   // IncrementQuestionsSeen()

    /***
	*		IncrementQuestionsSeen is called when a new question is displayed.
	***/

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }   // IncrementQuestionsSeen()

    /***
	*		IncrementCorrectAnswers is called when a question is answered correctly.
	***/
    public void IncrementCorrectAnswers()
	{
        correctAnswers++;
    }   // IncrementCorrectAnswers()

    /***
	*		IncrementCorrectAnswers is called when a question is answered correctly.
	***/
    public int CalculateScore()
	{
        if (questionsSeen == 0)
            return 0;   // Prevent divide by 0 before questionsSeen is incremented
        else
            return (correctAnswers * 100) / questionsSeen;
    }   // CalculateScore()

    /***
	*		ResetScore is called when we are restarting the section or going to a new section.
	***/
    public void ResetScore()
	{
        correctAnswers = questionsSeen = 0;
    }   // ResetScore()
}   // class ScoreKeeper