using UnityEngine;

public class InstatiateScorpion : MonoBehaviour
{
    [SerializeField] GameObject scorpion;
    [SerializeField] Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(scorpion, spawnPoint.position, Quaternion.identity);
    }
}
