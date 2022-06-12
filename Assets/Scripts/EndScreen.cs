using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI finalScoreText;

    ScoreKeeper scoreKeeper;

    /***
    *		Awake is called before the Start() methods for all classes.
    ***/
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }   // Awake()

    /***
    *		Start is called before the first frame update.
    ***
    void Start()
    {
    }   // Start()

    /***
	*		Update is called once per frame.
	***/
    void Update()
    {
        finalScoreText.text = "Congratulations on completing this section!\nYou scored " + scoreKeeper.CalculateScore() + "%";
    }   // Update()
}   // class EndScreen