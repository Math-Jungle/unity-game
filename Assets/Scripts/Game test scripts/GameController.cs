using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour // Ensure the class is derived from MonoBehaviour
{
    public Text additionProblemText;
    public Button[] appleTreeButtons;
    public Text[] appleTreeButtonTexts;
    public PandaController pandaController;
    public BasketController basketController;

    private int correctAnswer;

    void Start()
    {
        GenerateAdditionProblem();
    }

    void GenerateAdditionProblem()
    {
        int number1 = Random.Range(1, 10);
        int number2 = Random.Range(1, 10);
        correctAnswer = number1 + number2;
        additionProblemText.text = $"{number1} + {number2} = ?";

        int correctButtonIndex = Random.Range(0, appleTreeButtons.Length);
        for (int i = 0; i < appleTreeButtons.Length; i++)
        {
            appleTreeButtons[i].onClick.RemoveAllListeners();
            if (i == correctButtonIndex)
            {
                appleTreeButtonTexts[i].text = correctAnswer.ToString();
                appleTreeButtons[i].onClick.AddListener(CorrectAnswer);
            }
            else
            {
                int incorrectAnswer = Random.Range(1, 20);
                while (incorrectAnswer == correctAnswer)
                {
                    incorrectAnswer = Random.Range(1, 20);
                }
                appleTreeButtonTexts[i].text = incorrectAnswer.ToString();
                appleTreeButtons[i].onClick.AddListener(IncorrectAnswer);
            }
        }
    }

    void CorrectAnswer()
    {
        Debug.Log("Correct Answer!");
        pandaController.CollectApple();
        basketController.AddApple();
        GenerateAdditionProblem();
    }

    void IncorrectAnswer()
    {
        Debug.Log("Incorrect Answer. Try Again.");
        pandaController.PuzzledReaction();
    }
}
