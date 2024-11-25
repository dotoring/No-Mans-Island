using UnityEngine;

public class PlayerMouth : MonoBehaviour
{
    [SerializeField] PlayerState playerState;

    void EatFood(FoodClass food)
    {
        playerState.IncreaseFullness(food.fullness);
        playerState.IncreaseThirst(food.thirst);
        playerState.TakeDamage(food.Reduce_Hp);
        Debug.Log("������ ���� " + food.fullness);
        Debug.Log("���� ���� " + food.thirst);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            //������ ��ġ��ŭ ȸ�� �Լ���
            EatFood(other.GetComponent<FoodClass>());
            Destroy(other.gameObject);
        }
    }
}
