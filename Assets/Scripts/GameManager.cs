using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<SectionSO> sections;

    Quiz quiz;
    EndScreen endScreen;
    TextMeshProUGUI nextButtonText;
    int currentSection = 0;

    /***
	*		Awake is called before Start() for any class.
	***/
    private void Awake()
	{
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();

        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

        for (int i = 0; i < texts.Length; i++)
		{   // Look for the text of the Next Section button
            if (texts[i].CompareTag("Finish"))
			{   // Find the text for the Next Section button
                nextButtonText = texts[i];
                break;  // Exit foor loop now that we found it
			}   // if
		}   // for
    }   // Awake()

    /***
    *		Start is called before the first frame update.
    *	    This one will control how we start the game and which Canvas is enabled,
    *	plus set the questions up for the first section.
    ***/
    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        quiz.SetQuestions(sections[currentSection].GetQuestions());
    }   // Start()

    /***
	*		Update is called once per frame.
	***/
    void Update()
    {
        if (quiz.isComplete)
		{
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            if (currentSection == sections.Count - 1)
            {   // Change the text for the Next Section button now that we are in final section
                nextButtonText.text = "Exit";
            }   // if
        }   // if
    }   // Update()

    /***
    *		OnRepeatSelected will be called when the Repeat button on the end screen Canvas has
    *	been clicked by the Player.
    *	    By using SceneManager to reload everything, it will be as if we are running it from
    *	the beginning, so the quiz questions get reloaded.
    ***/
    public void OnRepeatSelected()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        quiz.SetQuestions(sections[currentSection].GetQuestions());
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }   // OnRepeatSelected()

    /***
    *		OnNextSelected will be called when the Next button on the end screen Canvas has
    *	been clicked by the Player.
    *	    By using SceneManager to reload everything, it will be as if we are running it from
    *	the beginning, so the quiz questions get reloaded.
    ***/
    public void OnNextSelected()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (currentSection == sections.Count - 1)
        {   // Exit the Quiz Master program
            if (Application.isPlaying & !Application.isEditor)
                Application.Quit(); // We may return from this, but the program will terminate at the end of the frame
#if false
            else
                UnityEditor.EditorApplication.isPlaying = false;    // Handle being in the editor, but set #if to true to use it
#endif
        }   // if
        else
        {   // Start the next section
            currentSection++;
            quiz.SetQuestions(sections[currentSection].GetQuestions());
            quiz.gameObject.SetActive(true);
            endScreen.gameObject.SetActive(false);
        }   // else
    }   // OnNextSelected()
}   // class GameManager