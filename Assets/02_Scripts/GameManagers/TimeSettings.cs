using UnityEngine;

[CreateAssetMenu(fileName = "TimeSettings", menuName = "Scriptable Objects/TimeSettings")]
public class TimeSettings : ScriptableObject
{
    public float timeMultiplier = 2000;
    public float startHour = 12;
    public float sunriseHour = 6;
    public float sunsetHour = 18;
}
