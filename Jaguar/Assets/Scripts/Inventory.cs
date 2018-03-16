using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory sharedInstance;

    public int bullets = 10;
    public int baits;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NewLoot (int bullet, int bait)
    {
        bullets = bullet + bullet;
        baits = bait + baits;
    }
}
