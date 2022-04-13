using UnityEngine;

/*
The purpose of this script is:
*/

public class LevelFinished : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroupWin, canvasGroupLose;
    private void OnEnable()
    {
        GameManager.Instance.OnGameWin += Win;
        GameManager.Instance.OnGameLost += Lose;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameWin -= Win;
        GameManager.Instance.OnGameLost -= Lose;
    }

    void Win()
    {
        canvasGroupWin.gameObject.SetActive(true);
        canvasGroupWin.alpha = 1;
        canvasGroupWin.interactable = true;
    }
    void Lose()
    {
        canvasGroupLose.gameObject.SetActive(true);
        canvasGroupLose.alpha = 1;
        canvasGroupLose.interactable = true;
    }
}