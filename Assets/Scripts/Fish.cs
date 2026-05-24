using UnityEngine;
using System;
public class Fish : MonoBehaviour
{
    public static event EventHandler OnFishCollected;
    public void DestroySelf()
    {
        Destroy(gameObject);
        OnFishCollected?.Invoke(this, EventArgs.Empty);
    }
}
