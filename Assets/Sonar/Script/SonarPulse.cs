using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pulse.asset", menuName = "Sonar/SonarPulse")]
public class SonarPulse : ScriptableObject
{
    public float range = 10;
    public Color color = new Color(51/256f, 255/256f, 232/256f, 1);
}
