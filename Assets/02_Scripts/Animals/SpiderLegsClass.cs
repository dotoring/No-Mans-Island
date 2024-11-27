using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpiderLegsClass : MonoBehaviour
{
    [SerializeField] SpiderClass spider;
    private void Update()
    {
        if (spider.t_state == AnimalState.Attack)
        {
            this.GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (spider.t_state == AnimalState.Attack)
            {
                spider.Hit(other.GetComponentInChildren<PlayerState>());
            }


        }
    }
}
