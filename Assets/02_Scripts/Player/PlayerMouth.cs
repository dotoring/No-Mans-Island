using UnityEngine;
using Photon.Pun;

public class PlayerMouth : MonoBehaviour
{
    [SerializeField] PlayerState playerState;

    void EatFood(FoodClass food)
    {
        playerState.IncreaseFullness(food.foodSO.fullness);
        playerState.IncreaseThirst(food.foodSO.thirst);
        playerState.TakeDamage(food.foodSO.Reduce_Hp);
        Debug.Log("포만도 증가 " + food.foodSO.fullness);
        Debug.Log("수분 섭취 " + food.foodSO.thirst);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            //음식의 수치만큼 회복 함수들
            EatFood(other.GetComponent<FoodClass>());
            PhotonNetwork.Destroy(other.gameObject);
        }
    }
}
