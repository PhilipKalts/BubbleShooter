using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
The purpose of this script is:
*/

public class MainMenu : MonoBehaviour
{
    public Image imageFade;
    public SOLevels levelSO;

    public void Play()
    {
        levelSO.CurrentLevel = 0;
        levelSO.hasAchievement = false;
        imageFade.DOFade(1, 1).OnComplete(GoToScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReplayWin()
    {
        if (levelSO.CurrentLevel + 1 < levelSO.Height.Length) levelSO.CurrentLevel++;
        imageFade.DOFade(1, 1).OnComplete(GoToScene);
    }

    public void ReplayLost()
    {
        imageFade.DOFade(1, 1).OnComplete(GoToScene);
    }
    
    void GoToScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
