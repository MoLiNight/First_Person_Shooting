using UnityEngine;
using UnityEngine.UI;

public class ScoreController
{
    private static ScoreController instance;
    
    private float currentScore = 0;

    public static ScoreController GetInstance()
    {
        if(instance == null)
        {
            instance = new ScoreController();
        }
        return instance;
    }

    public void CalculateScore(GameObject Target, GameObject Arrow)
    {
        float distanceWithPlayer = (Camera.main.transform.position - Arrow.transform.position).magnitude;

        Vector3 attackPosition = Arrow.transform.position;
        attackPosition.z = Target.transform.position.z;
        float distanceWithTargerCenter = (Target.transform.position - attackPosition).magnitude;

        currentScore += (distanceWithPlayer / 10) * (10 - distanceWithTargerCenter / 1.5f);
        currentScore -= currentScore % 1.0f;

        GameObject ScoreText = GameObject.Find("ScoreText");
        ScoreText.GetComponent<Text>().text = "Scores: " + currentScore.ToString();
    }
}
