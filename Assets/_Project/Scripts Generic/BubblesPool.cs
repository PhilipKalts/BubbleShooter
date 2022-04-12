using System.Collections.Generic;
using UnityEngine;

/*
The purpose of this script is: to create an object pool with Bubble Game Objects
When the player throws a ball and a merge is't possible, a new Bubble must be created
and take the place where the player threw.

However, to avoid instantiating at run time let's do it at the start
and let the player get the Bubble when is needed
*/

public class BubblesPool : MonoBehaviour
{
    [SerializeField] Bubble bubble;
    [SerializeField] int poolDepth;

    List<Bubble> pool = new List<Bubble>();



    private void Awake()
    {
        GameObject poolObject = new GameObject("Bubbles Pool");
        poolObject.transform.parent = transform;
        for (int i = 0; i < poolDepth; i++)
        {
            GameObject newGameObject = Instantiate(bubble.gameObject);
            Bubble newBubble = newGameObject.GetComponent<Bubble>();


            pool.Add(newBubble);
            newBubble.gameObject.SetActive(false);
            newBubble.transform.parent = poolObject.transform;
        }
    }



    public Bubble GetBubbleFromPool()
    {
        foreach (Bubble bubble in pool)
        {
            if (!bubble.gameObject.activeSelf) return bubble;
        }
        GameObject newGameObject = Instantiate(bubble.gameObject);
        Bubble newBubble = newGameObject.GetComponent<Bubble>();
        pool.Add(newBubble);
        return newBubble;
    }
}