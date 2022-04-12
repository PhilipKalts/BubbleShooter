using System.Collections;
using TMPro;
using UnityEngine;

/*
The purpose of this script is:
*/

public class NextBall : MonoBehaviour
{
    public int Points { get; private set; }
    public int SpawnsForDecrease;

    int index;
    GameObject player;
    TextMeshPro textChild;

    PlayerPoints playerPoints;
    SpriteRenderer spriteRenderer;
    int numberOfSpawns;


    private void Awake()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerPoints = player.GetComponent<PlayerPoints>();
        textChild = transform.GetChild(0).GetComponent<TextMeshPro>();
        GetValues();
    }

    private void GetValues()
    {
        index = Random.Range(0, GameManager.Instance.ColourManager.Points.Length - 7);

        Points = Random.Range(5, 14);
        spriteRenderer.color = GameManager.Instance.ColourManager.Colors[index];

        textChild.text = Points.ToString();
    }



    private void OnEnable()
    {
        playerPoints.OnBallHit += PlayerHit;
        GameManager.Instance.OnGameLost += GameOver;
        GameManager.Instance.OnGameWin += GameOver;
    }

    private void OnDisable()
    {
        playerPoints.OnBallHit -= PlayerHit;
        GameManager.Instance.OnGameLost -= GameOver;
        GameManager.Instance.OnGameWin -= GameOver;
    }

    void GameOver()
    {
        gameObject.SetActive(false);
    }

    void PlayerHit()
    {
        StartCoroutine(ResetPlayer());
    }

    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(0.2f);

        numberOfSpawns++;
        if (numberOfSpawns >= SpawnsForDecrease)
        {
            numberOfSpawns = 0;
            GameManager.Instance.OnDecreaseBubbles();
        }
        player.SetActive(true);
        playerPoints.SetNewValues(index, Points, GameManager.Instance.ColourManager.GetColour(Points));
        GetValues();
    }

}
