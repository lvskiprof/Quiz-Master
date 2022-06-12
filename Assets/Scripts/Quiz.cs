using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
	[Header("Questions")]
	[SerializeField]
	TextMeshProUGUI questionText;
	QuestionSO currentQuestion;
	[Header("Answers")]
	[SerializeField]
	GameObject[] answerButtons;
	int correctAnswerIndex;
	[Header("Button Colors")]
	[SerializeField]
	Sprite defaultAnswerSprite;
	[SerializeField]
	Sprite correctAnswerSprite;
	[Header("Scoring")]
	[SerializeField]
	TextMeshProUGUI scoreText;
	ScoreKeeper scoreKeeper;
	[Header("Progress Bar")]
	[SerializeField]
	Slider progressBar;

	Timer timer;
	List<QuestionSO> questions;
	public bool isComplete = false;
	bool buttonsEnabled = true;

	/***
	*		Awake is called before Start() for any class.
	***/
	private void Awake()
	{
		timer = FindObjectOfType<Timer>();
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
		progressBar = FindObjectOfType<Slider>();
	}   // Awake()

	/***
	*		Start is called before the first frame update.
	***/
	void Start()
	{
		GetNextQuestion();
		DisplayScore();
	}   // Start()

	/***
	*		Update is called once per frame.
	***
	void Update()
	{
	}   // Update()

	/***
	*		DisplayQuestion is called when we are displaying a new currentQuestion and answers.
	*	It also enables the buttons so they can be clicked by setting buttonsEnabled to true.
	***/
	void DisplayQuestion()
	{
		questionText.text = currentQuestion.GetQuestion();
		correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();

		for (int i = 0; i < answerButtons.Length; i++)
		{	// Change the text for all the buttons
			TextMeshProUGUI buttonText;

			if (i < currentQuestion.GetNumberOfAnswers())
			{   // We have an answer to show for this button
				answerButtons[i].SetActive(true);
				buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
				buttonText.text = currentQuestion.GetAnswer(i);
			}	// if
			else
			{	// There is no answer for this button, so disable it to prevent it from showing or being clickable
				answerButtons[i].SetActive(false);
			}   // else
		}   // for

		buttonsEnabled = true;
	}   // DisplayQuestion()

	/***
	*		GetNextQuestion is called when we get the next currentQuestion and answers.
	***/
	public void GetNextQuestion()
	{
		Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

		buttonImage.sprite = defaultAnswerSprite;   // Do this before we change the Question and Answer info
		// Change Question and Answer info here
		GetRandomQuestion();
		if (currentQuestion == null)
		{   // We have finished all the questions, so turn off the timer
			timer.CancelTimer();
			isComplete = true;
		}   // if
		else
		{	// We have a new currentQuestion to display
			DisplayQuestion();
			scoreKeeper.IncrementQuestionsSeen();
			progressBar.value++;
		}   // else
	}   // GetNextQuestion()

	/***
	*		GetRandomQuestion is called to get a random question from the list and will
	*	remove the question from the list.  It the list is empty it sets currentQuestion
	*	to null.
	***/
	void GetRandomQuestion()
	{
		if (questions.Count == 0)
		{   // We have finished all the questions, so show that a null for currentQuestion
			currentQuestion = null;
		}   // if
		else
		{
			int index = Random.Range(0, questions.Count - 1);
			currentQuestion = questions[index];
			questions.RemoveAt(index);
		}   // else
	}   // GetRandomQuestion()

	/***
	*		OnAnswerSelected is called when an answer has been selected by the player.  It lets
	*	the player know if they selected the correct answer or what the correct answer should be.
	***/
	public void OnAnswerSelected(int index)
	{
		if (buttonsEnabled)
		{
			if (index == correctAnswerIndex)
			{
				questionText.text = "Correct!";
				scoreKeeper.IncrementCorrectAnswers();
			}   // if
			else
			{
				questionText.text = "Incorrect! The correct answer is:\n" + currentQuestion.GetAnswer(correctAnswerIndex);
			}   // else

			ShowAnswer();
		}   // if
	}   // OnAnswerSelected(int index)

	/***
	*		AreButtonsEnabled is called from outside to see if the state has changed.
	***/
	public bool AreButtonsEnabled()
	{
		return buttonsEnabled;
	}   // bool AreButtonsEnabled()

	/***
	*		TimerExpired is called from outside to say the timer has expired and we should show the answer and disable the buttons.
	***/
	public void TimerExpired()
	{
		ShowAnswer();
		questionText.text = "Time expired! The correct answer is:\n" + currentQuestion.GetAnswer(correctAnswerIndex);
	}   // TimerExpired()

	/***
	*		ShowAnswer is called from several places to show what the correct answer is.  It also disables the buttons.
	***/
	void ShowAnswer()
	{
		Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

		buttonImage.sprite = correctAnswerSprite;
		buttonsEnabled = false;
		DisplayScore();
	}   // ShowAnswer()

	/***
	*		DisplayScore is called from more than one place, so it was made a method.
	***/
	void DisplayScore()
	{
		scoreText.text = "Score: " + scoreKeeper.CalculateScore().ToString() + "%";
	}   // DisplayScore()

	/***
	*		SetQuestions is called when a section has been selected in GameManager at the start of each section.
	*	The list is a copy, so it can be changed however is needed here and the original stays intact.  In here
	*	you need to reset everything to be the same as if we have been loaded fresh.
	***/
	public void SetQuestions(List<QuestionSO> newQuestions)
	{
		questions = newQuestions;
		progressBar.maxValue = questions.Count;
		progressBar.value = 0;
		scoreKeeper.ResetScore();
		DisplayScore();
		timer.StartTimer();
		isComplete = false;
		buttonsEnabled = true;
	}   // SetQuestions(List<QuestionSO> newQuestions)
}   // class Quiz