using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameMgr : MonoBehaviour
{
    [SerializeField] GameObject[] nightAnimals;
    [SerializeField] List<GameObject> players = new List<GameObject>();
    Coroutine coroutine;

    private void Start()
    {
        StartCoroutine(FindPlayers());
    }

    IEnumerator FindPlayers()
    {
        yield return new WaitForSeconds(2f);
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(p);
        }
        yield return null;
    }

    public void SpawnAnimals()
    {
        Debug.Log("선공 동물 소환");

        foreach (var animal in nightAnimals)
        {
            Vector3 randomPoint = Random.insideUnitCircle * 5;
            randomPoint += players[0].transform.position;
            randomPoint.y = 100f;
            RaycastHit hit;
            Physics.Raycast(randomPoint, Vector3.down, out hit);

            Instantiate(animal, hit.point, Quaternion.identity);
        }
    }

    public void OnDay()
    {
        players[0].GetComponent<PlayerState>().isCold = false;
    }

    public void OnNight()
    {
        players[0].GetComponent<PlayerState>().isCold = true;
    }
}
