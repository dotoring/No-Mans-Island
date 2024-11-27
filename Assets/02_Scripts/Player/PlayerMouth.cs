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
        Debug.Log("������ ���� " + food.foodSO.fullness);
        Debug.Log("���� ���� " + food.foodSO.thirst);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            //������ ��ġ��ŭ ȸ�� �Լ���
            EatFood(other.GetComponent<FoodClass>());
            PhotonNetwork.Destroy(other.gameObject);
        }
    }
}
