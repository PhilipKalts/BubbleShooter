using UnityEngine;

/*
The purpose of this script is:
*/

public class Ceiling : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPoints playerPoints = collision.GetComponent<PlayerPoints>();

        if (playerPoints == null) return;
        playerPoints.gameObject.SetActive(false);
        playerPoints.OnBallHit?.Invoke();
    }


}
