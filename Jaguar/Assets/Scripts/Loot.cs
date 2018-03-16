using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    int lootItems, maxItems, bullets, baits;
    float timeForLooting;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopCoroutine(Looting());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Looting());
        }
    }
    

    IEnumerator Looting()
    {
        yield return new WaitForSeconds(timeForLooting);
        lootItems = Random.Range(0, maxItems);
        bullets = Random.Range(0, lootItems);
        baits = Random.Range(0, lootItems - bullets);
        Inventory.sharedInstance.NewLoot(bullets, baits);
    }
    
}
