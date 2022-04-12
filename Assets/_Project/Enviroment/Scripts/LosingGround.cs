using UnityEngine;

/*
The purpose of this script is:
*/

public class LosingGround : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bubble bubble = collision.GetComponent<Bubble>();
        if (bubble == null) return;

        GameManager.Instance.OnGameLost?.Invoke();
    }


}
