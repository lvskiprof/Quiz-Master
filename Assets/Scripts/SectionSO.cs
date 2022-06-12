using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Section", menuName = "Scriptable Objects/Quiz Section")]
public class SectionSO : ScriptableObject
{
	[SerializeField]
	List<QuestionSO> questions;

	List<QuestionSO> copyOfQuestions = new List<QuestionSO>();

	/***
	*		GetQuestions returns a copy of the questions to the caller, preseving the original List.
	*	This allows the caller to modify the List that gets returned without changed the master List.
	***/
	public List<QuestionSO> GetQuestions()
	{
		copyOfQuestions.Clear();

		for (int i = 0; i < questions.Count; i++)
		{
			copyOfQuestions.Add(questions[i]);
		}	// for

		return copyOfQuestions;
	}   // List<QuestionSO> GetQuestions()
}   // class SectionSO