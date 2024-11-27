using UnityEngine;

public class DestroyNullParent : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }
}
