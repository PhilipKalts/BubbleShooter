using TMPro;
using UnityEngine;
using DG.Tweening;

/*
The purpose of this script is:
*/

public class CanvasPoints : MonoBehaviour
{
    TextMeshProUGUI textPoints;
    int totalPoints;
    [SerializeField] SOLevels levelSO;
    [SerializeField] RectTransform achievement;

    private void Awake()
    {
        textPoints = GetComponent<TextMeshProUGUI>();
        textPoints.text = totalPoints.ToString();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnBubbleDestroy += GetPoints;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnBubbleDestroy -= GetPoints;
    }


    void GetPoints(int points)
    {
        totalPoints += points;
        textPoints.text = totalPoints.ToString();

        if (levelSO.hasAchievement || totalPoints < 9000) return;

        levelSO.hasAchievement = true;
        achievement.DOMoveY(150, 4).OnComplete(ReturnAchievement);
    }

    void ReturnAchievement()
    {
        achievement.DOMoveY(-400, 2);
    }
}