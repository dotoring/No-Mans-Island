using System.Collections;
using UnityEngine;
using System;

public class EscapeMgr : MonoBehaviour
{
    [SerializeField] GameObject shipPref;
    [SerializeField] Transform shipSpawnPoint;
    [SerializeField] Transform shipArrivePoint;
    [SerializeField] GameObject escapeArea;
    GameObject ship;

    public void ComeShip()
    {
        if (ship == null)
        {
            ship = Instantiate(shipPref, shipSpawnPoint.position, shipSpawnPoint.rotation);
            ShipCtrl shipCtrl = ship.GetComponent<ShipCtrl>();
            shipCtrl.departPoint = shipSpawnPoint;
            shipCtrl.arrivePoint = shipArrivePoint;
            shipCtrl.isCome = true;
        }
        else
        {
            ship.SetActive(true);
            ship.GetComponent<ShipCtrl>().isCome = true;

        }
    }

    public void LeaveShip()
    {
        if (ship != null)
        {
            ship.GetComponent<ShipCtrl>().isCome = false;
        }
    }
}
