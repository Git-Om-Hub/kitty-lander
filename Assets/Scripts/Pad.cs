using UnityEngine;

public class Pad : MonoBehaviour
{
    public static Pad Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
       // Hide();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
