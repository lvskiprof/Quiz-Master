using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Scriptable Objects/Quiz Question")]
public class QuestionSO : ScriptableObject
{
	[TextArea(1,6)]		// Allow a text area in the Unity Editor from a min of 1 line, up to 6 lines (it will scroll if past 6 lines)
	[SerializeField]
	string question = "Enter new question text here";
	[SerializeField]
	string[] answers;	// Number of elements is set in Unity Editor when this is initialized
	[Range(0,3)]
	[SerializeField]
	int correctAnswerIndex = 0; // Set to the number of the correct answer (0 to 3) in the Unity Editor

	/***
	*		GetQuestion returns the currentQuestion string for this class instance to the caller.
	***/
	public string GetQuestion()
	{
		return question;
	}   // string GetQuestion()

	/***
	*		GetAnswer returns the currentQuestion string for a specific answer for this class instance to the caller.
	***/
	public string GetAnswer(int index)
	{
		if (index < 0 || index >= answers.Length)
			return "";
		else
			return answers[index];
	}   // string GetAnswer(int index)

	/***
	*		GetCorrectAnswer returns the currentQuestion string for this class instance to the caller.
	***/
	public int GetCorrectAnswerIndex()
	{
		return correctAnswerIndex;
	}   // int GetCorrectAnswerIndex()

	/***
	*		GetNumberOfAnswers returns the number of answers in the answers array.  This is set in the Unity Edtior.
	***/
	public int GetNumberOfAnswers()
	{
		return answers.Length;
	}   // GetNumberOfAnswers()
}   // class QuestionSO