﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpScript : MonoBehaviour
{
    public int maxHP;
    int HP;

    private void Start()
    {
        HP = maxHP;
    }

    public void Damage(int x)
    {
        HP = HP - x;
        if (HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //anim de muerte
        if (gameObject.tag == "Jaguar")
        {
            GameManager.sharedInstance.currentStateGame = GameState.gameOver;
        }
        else
        {
            GameManager.sharedInstance.AddPoint();
            gameObject.AddComponent<Loot>();
        }
    }
}


