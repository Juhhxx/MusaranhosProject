using System;
using Player;
using Player.Equipment;
using UnityEngine;
using UnityEngine.SceneManagement;
using Compass = Player.Equipment.Compass;

namespace Misc
{
    public class GameManager : MonoBehaviour
    {
        private Compass compass;
        private Lantern lantern;
        private PlayerMovement player;
        private PlayerController playerController;
        private PlayerInteraction playerInteraction;
        private PlayerInventory playerInventory;
        private PlayerLetterReader playerLetterReader;
        private int dangerLevel;
        private UiManager uiManager;
        private EnemyController enemyController;
        [SerializeField] private Animator jumpscareAnimator; 

        public event EventHandler OnScoutMove;
        
        public int DangerLevel
        {
            get { return dangerLevel; }
            private set
            {
                dangerLevel = value;
                enemyController.SetDangerLevel(value);
            }
        }

        private void Start()
        {
            compass = FindFirstObjectByType<Compass>();
            player = FindFirstObjectByType<PlayerMovement>();
            enemyController = FindFirstObjectByType<EnemyController>();
            playerLetterReader = FindFirstObjectByType<PlayerLetterReader>();
            playerInventory = FindFirstObjectByType<PlayerInventory>();
            playerInteraction = FindFirstObjectByType<PlayerInteraction>();
            playerController = FindFirstObjectByType<PlayerController>();
            lantern = FindFirstObjectByType<Lantern>();
            uiManager = FindFirstObjectByType<UiManager>();
            player.OnShiv += OnShiv;
            player.OnDeath += OnDeath;
            player.OnScoutMove += ScoutMove;
            playerInteraction.OnInteract += OnNewSound;
            playerController.OnPause += OnPause;
            playerLetterReader.OnLettersToggle += OnLettersToggle;
            playerLetterReader.OnReadingLetterChanged += OnReadingLetterChanged;
            playerInventory.OnItemAdded += OnItemAdded;
            playerInventory.OnItemRemoved += OnItemRemoved;
            playerInventory.OnLetterAdded += OnLetterAdded;
            lantern.OnCrank += OnNewSound;
            lantern.OnFlash += OnFlash;
            player.OnNoise += OnNewSound;
            enemyController.OnAttack += OnAttack;
            
            SetCompassTarget(GameObject.FindWithTag("Exit").transform);
        }
        
        private void SetCompassTarget(Transform target)
        {
            compass.SetNeedleTarget(target);
        }

        private void OnEnemyLostChase(object sender, EventArgs e)
        {
            player.StartScout();
        }

        private void OnLettersToggle(object sender, EventArgs e)
        {
            var reading = playerLetterReader.IsReadingLetters;
            playerController.StopMovement(reading);
            playerController.StopActions(reading, !reading);
            uiManager.ToggleLetters(reading);
        }

        private void OnReadingLetterChanged(object sender, EventArgs e)
        {
            uiManager.ChangeLetters();
        }

        private void OnItemAdded(object sender, EventArgs e)
        {
            uiManager.PickedUpItem(playerInventory.NewItem);
        }

        private void OnItemRemoved(object sender, EventArgs e)
        {
            //Call UI
        }

        private void OnLetterAdded(object sender, EventArgs e)
        {
            uiManager.PickedUpLetter();
        }

        private void OnNewSound(object sender, EventArgs e)
        {
            enemyController.HearSound(true);
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

        private void OnAttack(object sender, EventArgs e)
        {
            playerController.GetAttacked();
            player.GetAttacked();
            
        }

        private void OnFlash(object sender, EventArgs e)
        {
            player.StartChase();
        }

        private void OnShiv(object sender, EventArgs e)
        {
            jumpscareAnimator.SetTrigger("Shiv");
        }

        public void AttackEnd()
        {
            playerController.EscapedAttack();
            enemyController.Shived(true);
        }

        private void OnDeath(object sender, EventArgs e)
        {
            jumpscareAnimator.SetTrigger("Death");
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void ScoutMove(object sender, EventArgs e)
        {
            OnScoutMove?.Invoke(this, EventArgs.Empty);
        }

        public void UpDangerLevel()
        {
            DangerLevel++;
        }
    }
}