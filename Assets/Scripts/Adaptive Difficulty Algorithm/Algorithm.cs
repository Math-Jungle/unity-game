using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AdaptiveDifficultyAlgorithm
{
    int score = 0;
    public float responseTime;
    bool isCorrect = false;

    private string nextScene;

    private void CalculateScore()
    {
        if (isCorrect)
        {
            score += 100;
            if (responseTime < 5f) // Bonus for faster response
            {
                score += 50;
            }
        }
        else
        {
            score -= 50;
        }
    }

    private void AdjustDifficulty()
    {
        if (score > 3000) // Threshhold for difficulty increase
        {
            SetDifficulty("Hard");
        }
        else
        {
            SetDifficulty("Normal");
        }
    }

    private void SetDifficulty(string level)
    {
        if (level == "Hard")
        {
            //Load the difficult game
        }
        else
        {
            //Load the normal game
        }
    }
}
