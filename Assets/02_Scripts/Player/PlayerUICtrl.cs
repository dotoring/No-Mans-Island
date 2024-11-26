using UnityEngine;

public class PlayerUICtrl : MonoBehaviour
{
    [SerializeField] GameObject controller;
    [SerializeField] GameObject canvas;

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
