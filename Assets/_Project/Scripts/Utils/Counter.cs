using UnityEngine;

public class Counter : MonoBehaviour
{
    public void Count()
    {
        float scale = 1f;
        for (int i = 0; i < 100; i++)
        {
            scale += .1f / Mathf.Pow(Mathf.Max(1f, i), .8f);
            print(scale);
        }
    }
}
