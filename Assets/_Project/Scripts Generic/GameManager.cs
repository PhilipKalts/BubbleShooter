using System;
using UnityEngine;

/*
The purpose of this script is: to make the main Game Manager
Here all the other Managers get referenced and can be accessed with
a single Instance
*/

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector] public GridManager GridManager;
    [HideInInspector] public BubblesPool BubblesPool;
    [HideInInspector] public ColourManager ColourManager;

    public Action<int> OnBubbleDestroy;
    public Action OnDecreaseBubbles;
    public Action OnGameLost, OnGameWin;


    private void Awake()
    {
        ///***Singleton***///
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        ///***Components***///
        GridManager = GetComponent<GridManager>();
        BubblesPool = GetComponent<BubblesPool>();
        ColourManager = GetComponent<ColourManager>();
    }


}
