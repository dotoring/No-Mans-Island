using UnityEngine;
using Photon.Pun;

public class PlayerMouth : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    [SerializeField] PhotonView pv;

    void EatFood(FoodClass food)
    {
        playerState.IncreaseFullness(food.foodSO.fullness);
        playerState.IncreaseThirst(food.foodSO.thirst);
        playerState.TakeDamage(food.foodSO.Reduce_Hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            if(pv.IsMine)
            {
                //음식의 수치만큼 회복 함수들
                EatFood(other.GetComponent<FoodClass>());
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }
}
