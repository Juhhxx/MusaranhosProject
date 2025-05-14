using System;
using UnityEngine;
using Player.Equipment;
using Compass = Player.Equipment.Compass;

namespace Misc
{
    public class GameManager : MonoBehaviour
    {
        private Compass compass;
        private PlayerMovement player;
        private EnemyMovement enemy;
        private int dangerLevel;

        public int DangerLevel
        {
            get { return dangerLevel; }
            private set
            {
                dangerLevel = value;
                //enemy.setDangerLevel(value);
            }
        }

        private void Start()
        {
            compass = FindFirstObjectByType<Compass>();
            player = FindFirstObjectByType<PlayerMovement>();
            enemy = FindFirstObjectByType<EnemyMovement>();
            
            //enemy.OnBlind += OnEnemyBlinded;
            //enemy.OnLostChase += OnEnemyLostChase;
            player.OnScoutMove += OnPlayerScoutMove;
        }
        
        private void SetCompassTarget(Transform target)
        {
            compass.SetNeedleTarget(target);
        }
        
        private void OnEnemyBlinded(object sender, EventArgs e)
        {
            player.StartChase();
        }

        private void OnEnemyLostChase(object sender, EventArgs e)
        {
            player.StartScout();
        }

        private void OnPlayerScoutMove(object sender, EventArgs e)
        {
            //enemy.Move();
        }

        public void NewSound(Vector3 position)
        {
            //enemy.HeardSound(position);
        }
    }
}