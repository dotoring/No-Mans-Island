using UnityEngine;

public class PlayerDieTest : MonoBehaviour
{
    private void Start()
    {
        GetComponent<PlayerState>().OnDie += (_) => Test();
    }

    void Test()
    {
        Debug.Log("Á×À½");
    }
}
