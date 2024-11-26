using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Scriptable Objects/Food")]
public class Food : ScriptableObject
{
    public int fullness;
    public int thirst;
    public int Reduce_Hp;
    public float cookTime;
}
