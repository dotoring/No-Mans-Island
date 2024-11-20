using UnityEngine;

public class PlayerMouth : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            //음식의 수치만큼 회복 함수들
            Destroy(other.gameObject);
        }
    }
}
