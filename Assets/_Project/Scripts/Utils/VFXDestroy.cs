using UnityEngine;

public class VFXDestroy : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 2f);
    }
}
