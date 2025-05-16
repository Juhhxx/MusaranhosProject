using System;
using Misc;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Death()
    {
        gameManager.DeathScreen();
    }

    public void Missed()
    {
        print("a");
        gameManager.AttackEnd();
    }
}
