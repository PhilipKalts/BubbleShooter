using UnityEngine;

/*
The purpose of this script is:
*/

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject bubblePrefab;


    private void Start()
    {
        GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        bubble.transform.parent = transform;
    }
}
