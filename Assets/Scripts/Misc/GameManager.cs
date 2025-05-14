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
            
            //enemy.OnBlind() += OnEnemyBlinded;
            //enemy.OnLostChase() += OnEnemyLostChase;
            //player.OnScoutMove() += OnPlayerScoutMove;
        }
        
        private void SetCompassTarget(Transform target)
        {
            compass.SetNeedleTarget(target);
        }
        
        private void OnEnemyBlinded()
        {
            //player.StartChase();
        }

        private void OnEnemyLostChase()
        {
            //player.StartScout();
        }

        private void OnPlayerScoutMove()
        {
            //enemy.Move();
        }
    }
}