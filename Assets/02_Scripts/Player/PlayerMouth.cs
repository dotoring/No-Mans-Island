using UnityEngine;

public class PlayerMouth : MonoBehaviour
{
    [SerializeField] PlayerState playerState;

    void EatFood(FoodClass food)
    {
        playerState.IncreaseFullness(food.fullness);
        playerState.IncreaseThirst(food.thirst);
        playerState.TakeDamage(food.Reduce_Hp);
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
