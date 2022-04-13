using UnityEngine;

/*
The purpose of this script is:
*/

[CreateAssetMenu]
public class SOLevels : ScriptableObject
{
    [HideInInspector] public int CurrentLevel;
    public int[] Height, SpawnForDecrease;
    public bool hasAchievement;
    public void ResetLevels()
    {
        CurrentLevel = 0;
    }
}
