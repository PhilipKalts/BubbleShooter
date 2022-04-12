using System.Collections;
using TMPro;
using UnityEngine;

/*
The purpose of this script is:
*/

public class Bubble : MonoBehaviour
{
    public int Points { get; private set; }

    TextMeshPro textChild;
    SpriteRenderer spriteRenderer;

    [HideInInspector] public int[] closeX, closeY;
    int index;

    BoxCollider2D boxCollider;

    private void OnEnable()
    {
        int posX = (int)transform.position.x;
        int posY = (int)transform.position.y;

        closeX = new int[3];
        closeX[0] = posX;
        closeX[1] = posX + 1;
        closeX[2] = posX - 1;

        closeY = new int[3];
        closeY[0] = posY;
        closeY[1] = posY + 1;
        closeY[2] = posY - 1;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textChild = transform.GetChild(0).GetComponent<TextMeshPro>();


        index = Random.Range(0, GameManager.Instance.ColourManager.Points.Length - 7);

        Points = GameManager.Instance.ColourManager.Points[index];
        textChild.text = Points.ToString();

        spriteRenderer.color = GameManager.Instance.ColourManager.Colors[index];

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    public void SetBubble(int newPoints, Color newColor, bool shouldMerge = false)
    {
        Points = newPoints;
        textChild.text = Points.ToString();
        spriteRenderer.color = newColor;

        if (newPoints == 2048) MaxOut();

        if (shouldMerge) Merge();
    }


    void MaxOut()
    {
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }


    void Merge()
    {
        for (int x = 0; x < closeX.Length; x++)
        {
            for (int y = 0; y < closeY.Length; y++)
            {
                if (!(x == 0 && y ==0))
                {
                    if (closeX[x] >= 0 && closeY[y] >= 0 && 
                        closeX[x] < GameManager.Instance.GridManager.width && 
                        closeY[y] < GameManager.Instance.GridManager.height)
                    {
                        Bubble closeBubbles = GameManager.Instance.GridManager.AllBubbles[closeX[x], closeY[y]];
                        if (closeBubbles != null)
                        {
                            if (closeBubbles.gameObject.activeSelf && closeBubbles.Points == Points)
                            {
                                closeBubbles.SetBubble(Points * 2,
                                                        GameManager.Instance.ColourManager.DictColour[Points * 2]);
                                gameObject.SetActive(false);
                            }
                        }
                    }
                    //if (GameManager.Instance.GridManager.AllBubbles.GetLength(0) > closeX[x] &&
                    //    GameManager.Instance.GridManager.AllBubbles.GetLength(0) > closeY[y])
                    //{
                    //    Bubble closeBubbles = GameManager.Instance.GridManager.AllBubbles[closeX[x], closeY[y]];
                    //    if (closeBubbles.Points == Points)
                    //    {
                    //        Debug.Log("I came" + closeBubbles.name);
                    //        closeBubbles.SetBubble(GameManager.Instance.ColourManager.Points[index + 1],
                    //                                GameManager.Instance.ColourManager.Colors[index + 1]);
                    //        gameObject.SetActive(false);
                    //        return;
                    //    }
                    //}
                }
            }
        }
    }


    public void SetNewColor(int newPoints)
    {
        Points *= newPoints;
        textChild.text = Points.ToString();
        if (Points >= 2048)
        {
            GameManager.Instance.OnBubbleDestroy(Points);
            boxCollider.enabled = true;
            Invoke("Deactivacte", 0.3f);
        }
        // Rule of 3
        // 2048 -> 1
        // newPoints -?
        float percentage = (float)Points / 2048;
        spriteRenderer.color = GameManager.Instance.ColourManager.Gradient.Evaluate(percentage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bubble bubble = collision.GetComponent<Bubble>();

        if (bubble == null) return;

        bubble.GetDestroyedByBubble();
    }

    public void GetDestroyedByBubble()
    {
        GameManager.Instance.OnBubbleDestroy(Points);
        gameObject.SetActive(false);
    }

    void Deactivacte()
    {
        gameObject.SetActive(false);
    }

}