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

    public void Play()
    {
        imageFade.DOFade(1, 1).OnComplete(GoToScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    void GoToScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Hi()
    {
        print("Hi");
    }

}
