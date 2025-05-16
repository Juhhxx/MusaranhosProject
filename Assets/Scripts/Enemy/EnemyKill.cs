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
        gameManager.BackToMenu();
    }

    public void Missed()
    {
        gameManager.AttackEnd();
    }
}
