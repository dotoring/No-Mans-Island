using UnityEngine;

public class PlayerUICtrl : MonoBehaviour
{
    [SerializeField] GameObject controller;
    [SerializeField] GameObject canvas;

    private void Start()
    {
        //controller=GameObject.Find("XR Origin (VR)").transform.GetChild(0).GetChild(1).gameObject;
    }

    void Update()
    {
        transform.localPosition = controller.transform.localPosition + Vector3.up * 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}
