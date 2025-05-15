using System;
using Player;
using UnityEngine;
using Player.Equipment;
using Compass = Player.Equipment.Compass;

namespace Misc
{
    public class GameManager : MonoBehaviour
    {
        private Compass compass;
        private PlayerMovement player;
        private PlayerController playerController;
        private PlayerInventory playerInventory;
        private PlayerLetterReader playerLetterReader;
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
            playerLetterReader = FindFirstObjectByType<PlayerLetterReader>();
            playerInventory = FindFirstObjectByType<PlayerInventory>();
            playerController = FindFirstObjectByType<PlayerController>();
            
            //enemy.OnBlind += OnEnemyBlinded;
            //enemy.OnLostChase += OnEnemyLostChase;
            player.OnScoutMove += OnPlayerScoutMove;
            playerLetterReader.OnLettersToggle += OnLettersToggle;
            playerLetterReader.OnReadingLetterChanged += OnReadingLetterChanged;
            playerInventory.OnItemAdded += OnItemAdded;
            playerInventory.OnItemRemoved += OnItemRemoved;
            playerInventory.OnLetterAdded += OnLetterAdded;
            
            SetCompassTarget(GameObject.FindWithTag("Exit").transform);
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

        private void OnLettersToggle(object sender, EventArgs e)
        {
            var reading = playerLetterReader.IsReadingLetters;
            StopPlayTime(reading);
            playerController.StopMovement(reading);
            playerController.StopActions(reading, !reading);
            // Call UI
        }

        private void OnReadingLetterChanged(object sender, EventArgs e)
        {
            // Call UI
        }

        private void OnItemAdded(object sender, EventArgs e)
        {
            //Call UI
        }

        private void OnItemRemoved(object sender, EventArgs e)
        {
            //Call UI
        }

        private void OnLetterAdded(object sender, EventArgs e)
        {
            //Call UI
        }

        public void NewSound(Vector3 position)
        {
            //enemy.HeardSound(position);
        }

        public void PlayerWalking(bool value)
        {
            playerController.StopActions(!value, !value);
        }

        private void StopPlayTime(bool value)
        {
            Time.timeScale = value ? 1f : 0f;
        }
    }
}