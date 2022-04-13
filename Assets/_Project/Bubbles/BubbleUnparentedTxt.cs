using UnityEngine;
using DG.Tweening;

/*
The purpose of this script is:
*/

public class BubbleUnparentedTxt : MonoBehaviour
{
    [HideInInspector] public int Points;
    [HideInInspector] public ParticleSystem Particles;


    private void OnEnable()
    {
        Particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        Particles.transform.parent = null;
        var psMain = Particles.main;
        psMain.startColor = GameManager.Instance.ColourManager.GetColour(Points);
        Particles.Play();
        transform.DOMoveY(transform.position.y + 2, 0.5f).OnComplete(GoToCorner);
    }

    void GoToCorner()
    {
        transform.DOMove(new Vector3(2.5f, 6, 0), 1).OnComplete(Deactivate);
    }

    void Deactivate()
    {
        GameManager.Instance.OnBubbleDestroy?.Invoke(Points);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
