using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class ShipCtrl : MonoBehaviour
{
    [SerializeField] float speed;
    public Transform departPoint;
    public Transform arrivePoint;
    public bool isCome { get; set; }

    private void Update()
    {
        if(isCome)
        {
            transform.position = Vector3.MoveTowards(transform.position, arrivePoint.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, departPoint.position, speed * Time.deltaTime);
        }

        if(!isCome)
        {
            if(transform.position == departPoint.position)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void asdf()
    {
        int a = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
