using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //ΩÃ±€≈Ê ∞¥√º
    private static PhotonManager instance;

    private const string version = "1.0";
    [SerializeField] private string nickname = " ";





    public static PhotonManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        } 
    }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}
