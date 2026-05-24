using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int totalFish;
    private int collectedFish;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        //Fish.OnFishCollected += HandleFishCollected;
        Fish.OnFishCollected += Fish_OnFishCollected;
    }

    private void OnDisable()
    {
       // Fish.OnFishCollected -= HandleFishCollected;
        Fish.OnFishCollected -= Fish_OnFishCollected;
    }
    private void Fish_OnFishCollected(object sender, System.EventArgs e)
    {
        collectedFish++;


        if (collectedFish >= totalFish)
        {
            Pad.Instance.Show();
        }
    }


    private void Start()
    {
        totalFish = FindObjectsByType<Fish>(FindObjectsSortMode.None).Length;
    }

    //private void HandleFishCollected()
    //{
    //    collectedFish++;

    //    Debug.Log(collectedFish + " / " + totalFish);

    //    if (collectedFish >= totalFish)
    //    {
    //        SpawnLandingPad();
    //    }
    //}

}