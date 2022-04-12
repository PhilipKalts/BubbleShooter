using System.Collections.Generic;
using UnityEngine;

/*
The purpose of this script is: to control what the Points will be
and for what colour. When we want to take the point from the Points array we'll take
the same index from the Colors one for the color of the bubble
*/

public class ColourManager : MonoBehaviour
{
    public int[] Points;
 
    public Color[] Colors;

    public Dictionary<int, Color> DictColour = new Dictionary<int, Color>();

    public Gradient Gradient;


    private void Start()
    {
        for (int i = 0; i < Points.Length; i++)
        {
            DictColour[Points[i]] = Colors[i];
        }
    }

    public Color GetColour(int points)
    {
        float percentage = (float)points / 2048;
        return Gradient.Evaluate(percentage);
    }
}
