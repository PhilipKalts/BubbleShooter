using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/*
The purpose of this script is:
*/

public class MainGameUI : MonoBehaviour
{
    [SerializeField] Image startingImage;
    [SerializeField] GameObject player, nextBall;


    private void Awake()
    {
        startingImage.DOFade(0, 1).OnComplete(ActivatePlayer);
    }

    void ActivatePlayer()
    {
        player.SetActive(true);
        nextBall.SetActive(true);
    }

}
