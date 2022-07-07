using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EEData", menuName = "EEData", order = 1100)]
public class EEData : ScriptableObject
{
    public int numOfStarts = 0;
    public int timesReqToShow;

    public List<String> eeTexts;
}
