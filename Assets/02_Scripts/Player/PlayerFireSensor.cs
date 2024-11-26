using Unity.VisualScripting;
using UnityEngine;

public class PlayerFireSensor : MonoBehaviour
{
    [SerializeField] PlayerState state;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            state.isCold = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            if(!FindAnyObjectByType<TimeManager>().IsDay)
            {
                state.isCold = true;
            }
        }
    }
}
