using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] GameObject rightController;
    [SerializeField] GameObject canvas;

    void Update()
    {
        transform.localPosition = rightController.transform.localPosition + Vector3.up * 0.2f;
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
