using UnityEngine;
using DG.Tweening;

/*
The purpose of this script is: to generate the grid in which
the bubbles will spawn
*/


public class GridManager : MonoBehaviour
{
    public Bubble[,] AllBubbles;


    public int width, height;
    public int TotalWinPoints;
    [SerializeField] Tile tilePrefab;
    [SerializeField] Bubble bubblePrefab;
    [SerializeField] SOLevels levelSO;


    private void Start()
    {
        height = levelSO.Height[levelSO.CurrentLevel];
        TotalWinPoints = width * height;
        AllBubbles = new Bubble[width, height];
        GenerateGrid();
        transform.position = new Vector3(-2, 3, 0);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnDecreaseBubbles += DecreaseBubbles;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnDecreaseBubbles -= DecreaseBubbles;
    }

    void GenerateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            GameObject gameObject = new GameObject($"Line {y}");
            for (int x = 0; x < width; x++)
            {
                GameObject spawnedTile = Instantiate(bubblePrefab.gameObject, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Bubble {x} {y}";
                spawnedTile.transform.parent = gameObject.transform;
                AllBubbles[x, y] = spawnedTile.GetComponent<Bubble>();
                //if (y < 3) spawnedTile.SetActive(false);
            }
            
            
            gameObject.transform.parent = transform;
            Vector2 newPos = transform.position;

            if (y % 2 == 0)
                newPos.x = 0.2f * 1;
            else
                newPos.x = 0.2f * -1;
            
            
            gameObject.transform.position = newPos;
        }
    }

    void DecreaseBubbles()
    {
        transform.DOMoveY(transform.position.y - 1, 0.2f);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (AllBubbles[x, y].gameObject.activeSelf) return;
            }
        }
        GameManager.Instance.OnGameWin?.Invoke();
    }


    public void ActivateBubble(int posX, int posY, int points, Color newColor)
    {
        AllBubbles[posX, posY].gameObject.SetActive(true);
        AllBubbles[posX, posY].SetBubble(points, newColor);
    }

}
