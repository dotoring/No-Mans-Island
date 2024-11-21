using UnityEngine;

public class PlayerMouth : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        //transform.root.GetComponent<PlayerController>();
    }

    void EatFood(FoodClass food)
    {
        //player.IncreaseFullness(food.fullness);
        //player.IncreaseThirst(food.thirst);
        Debug.Log("포만도 증가 " + food.fullness);
        Debug.Log("수분 섭취 " + food.thirst);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            //음식의 수치만큼 회복 함수들
            EatFood(other.GetComponent<FoodClass>());
            Destroy(other.gameObject);
        }
    }
}
