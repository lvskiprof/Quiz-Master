using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	[SerializeField]
	Image timerImage;
	[SerializeField]
	Canvas quizCanvas;
	[SerializeField]
	float timeToAnswer = 30f;
	[SerializeField]
	float timeToShowAnswer = 10f;
	float timerValue;
	float startingTimerValue;
	bool isAnsweringQuestion = true;
	bool timerRunning = true;
	Quiz quiz;

	/***
	*		Awake is called before Start() for any class.
	***/
	private void Awake()
	{
		quiz = quizCanvas.GetComponent<Quiz>();
	}   // Awake()

	/***
	*		Start is called before the first frame update.
	***/
	void Start()
	{
		timerValue = startingTimerValue = timeToAnswer;
	}   // Start()

	/***
	*		Update is called once per frame.
	***/
	void Update()
    {
		UpdateTimer();
    }   // Update()

	/***
	*		StartTimer is called from the GameManager script when the we restart the current
	*	section or start a new section.
	***/
	public void StartTimer()
	{
		timerRunning = true;
	}	// StartTimer()

	/***
	*		CancelTimer is called from the Quiz script when the we try to fetch a question
	*	and there are no more left, so we turn off the timer and hide the image.
	***/
	public void CancelTimer()
	{
		timerRunning = false;
	}   // CancelTimer()

	/***
	*		UpdateTimer is called to update the timer image based on how many seconds are left.
	***/
	void UpdateTimer()
	{
		if (timerRunning)
		{	// Only need to do this if the timer is running
			timerValue -= Time.deltaTime;
			if (timerValue <= 0f || quiz.AreButtonsEnabled() != isAnsweringQuestion)
			{
				if (isAnsweringQuestion)
				{   // Show the correct answer and start the timer for how long to show it
					if (timerValue <= 0f)
						quiz.TimerExpired();	// Only Report timer expired if <= 0

					timerValue = timeToShowAnswer;
				}   // if
				else
				{   // We need to get the next currentQuestion and start the timer to answer it
					timerValue = timeToAnswer;
					quiz.GetNextQuestion();
				}   // else

				isAnsweringQuestion = quiz.AreButtonsEnabled(); // The state has changed
				startingTimerValue = timerValue;
			}   // if
		}   // if
		else
		{	// The time is no longer running so this will set timeImage.fillAmount to 0 to hide the image
			timerValue = 0f;
		}   // else

		timerImage.fillAmount = timerValue / startingTimerValue;
		if (timerImage.fillAmount < 0)
			timerImage.fillAmount = 0f;
	}   // UpdateTimer()
}   // class Timer