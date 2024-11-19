using UnityEngine;

public class RoastClass : MonoBehaviour
{
    public int fullness_roast;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullness_roast = 5;
    }


    public void GetFullness_Roast(int PlayerFullness)
    {
        PlayerFullness += fullness_roast;
        print($"포만감이 {fullness_roast} 만큼 올랐습니다.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player Mouse")
        {
            GetFullness_Roast(0);

            Destroy(this.gameObject);
        }
    }
}
