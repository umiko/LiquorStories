using UnityEngine;

public class Knife : MonoBehaviour
{
    private void Start()
    {
        SliceRegistry.Register(this.transform);
    }
}
