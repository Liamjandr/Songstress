using UnityEngine;
using UnityEngine.UI;
public class Scoring : MonoBehaviour
{
    public Text score;
    public int scoreNum = 0;

    public void increaseScore(int points)
    {
        scoreNum += points;
        ScoreToText();
    }

    public void ScoreToText()
    {
        score.text = scoreNum.ToString();
    }
}
