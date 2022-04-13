using System;
using TMPro;
using UnityEngine;

/*
The purpose of this script is:
*/

public class PlayerPoints : MonoBehaviour
{
    #region Variables

    ///***Public***///
    public int Points { get; private set; }
    public Action OnBallHit;

    ///***Private***///
    int index;      //the index which coordinates the colour and number of points from the ColourManager script
    TextMeshPro textChild;
    Vector3 startingPos;
    public int currentPoints;

    ///***Components***///
    Transform myTransform;
    SpriteRenderer spriteRenderer;

    #endregion



    private void Awake()
    {
        myTransform = transform;
        startingPos = transform.localPosition;

        spriteRenderer = GetComponent<SpriteRenderer>();
        textChild = transform.GetChild(1).GetComponent<TextMeshPro>();

        index = UnityEngine.Random.Range(0, GameManager.Instance.ColourManager.Points.Length - 7);

        Points = GameManager.Instance.ColourManager.Points[index];
        spriteRenderer.color = GameManager.Instance.ColourManager.Colors[index];

        textChild.text = Points.ToString();
    }



    #region OnEnable/Disable

    private void OnEnable()
    {
        GameManager.Instance.OnBubbleDestroy += IncreasePoints;
    }

    private void OnDisable()
    {
        myTransform.localPosition = startingPos;
        GameManager.Instance.OnBubbleDestroy -= IncreasePoints;
    }

    void IncreasePoints(int nth)
    {
        currentPoints++;
        if (currentPoints >= GameManager.Instance.GridManager.TotalWinPoints) GameManager.Instance.OnGameWin?.Invoke();
    }

    #endregion


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6 || myTransform.position == startingPos) return;

        Bubble bubble = collision.gameObject.GetComponent<Bubble>();
        if (bubble == null) return;

        bubble.SetNewColor(Points);
        //if (bubble.Points != Points)
        //{
        //    Bubble newBubble = GameManager.Instance.BubblesPool.GetBubbleFromPool();
        //    newBubble.gameObject.SetActive(true);
        //    newBubble.transform.position = myTransform.position;
        //    newBubble.SetBubble(Points, spriteRenderer.color);
        //    //GameManager.Instance.GridManager.AllBubbles[(int)newBubble.transform.position.x, (int)newBubble.transform.position.y] = newBubble;
        //    //GameManager.Instance.GridManager.ActivateBubble((int)myTransform.position.x + 2,
        //    //                                                (int)myTransform.position.x + 2,
        //    //                                                Points,
        //    //                                                spriteRenderer.color);
        //}
        //else
        //    bubble.SetBubble(GameManager.Instance.ColourManager.Points[index + 1],
        //                     GameManager.Instance.ColourManager.Colors[index + 1],
        //                     true);

        OnBallHit?.Invoke();
        gameObject.SetActive(false);
    }



    public void SetNewValues(int arrayIndex, int newPoints, Color newColor)
    {
        index = arrayIndex;
        Points = newPoints;
        spriteRenderer.color = newColor;
        textChild.text = newPoints.ToString();
    }
}