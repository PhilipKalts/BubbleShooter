using TMPro;
using UnityEngine;

/*
The purpose of this script is:
*/

public class CanvasPoints : MonoBehaviour
{
    TextMeshProUGUI textPoints;
    int totalPoints;

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
    }
}
