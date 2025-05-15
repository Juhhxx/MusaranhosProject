using System;
using Player;
using Player.Equipment;
using UnityEngine;
using Compass = Player.Equipment.Compass;

namespace Misc
{
    public class GameManager : MonoBehaviour
    {
        private Compass compass;
        private Lantern lantern;
        private PlayerMovement player;
        private PlayerController playerController;
        private PlayerInventory playerInventory;
        private PlayerLetterReader playerLetterReader;
        private EnemyMovement enemy;
        private int dangerLevel;
        private UiManager uiManager;

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
            lantern = FindFirstObjectByType<Lantern>();
            uiManager = FindFirstObjectByType<UiManager>();
            
            //enemy.OnBlind += OnEnemyBlinded;
            //enemy.OnLostChase += OnEnemyLostChase;
            player.OnScoutMove += OnPlayerScoutMove;
            playerController.OnPause += OnPause;
            playerLetterReader.OnLettersToggle += OnLettersToggle;
            playerLetterReader.OnReadingLetterChanged += OnReadingLetterChanged;
            playerInventory.OnItemAdded += OnItemAdded;
            playerInventory.OnItemRemoved += OnItemRemoved;
            playerInventory.OnLetterAdded += OnLetterAdded;
            lantern.OnCrank += OnNewSound;
            player.OnNoise += OnNewSound;
            
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

        private void OnNewSound(object sender, EventArgs e)
        {
            NewSound(player.transform.position);
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

        private void OnPause(object sender, EventArgs e)
        {
            StopPlayTime(true);   
            uiManager.Pause();
        }

        public void Unpause()
        {
            StopPlayTime(false);
            playerController.ResumeGame();
        }
    }
}