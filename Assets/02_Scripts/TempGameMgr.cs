using UnityEngine;
using UnityEngine.AI;

public class TempGameMgr : MonoBehaviour
{
    [SerializeField] GameObject[] nightAnimals;
    [SerializeField] GameObject player;

    public void SpawnAnimals()
    {
        Debug.Log("선공 동물 소환");

        foreach (var animal in nightAnimals)
        {
            Vector3 randomPoint = Random.insideUnitCircle * 5;
            randomPoint += player.transform.position;
            randomPoint.y = 100f;
            RaycastHit hit;
            Physics.Raycast(randomPoint, Vector3.down, out hit);

            Instantiate(animal, hit.point, Quaternion.identity);
        }
    }
}
