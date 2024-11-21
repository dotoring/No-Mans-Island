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
