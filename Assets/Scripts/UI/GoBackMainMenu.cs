using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GoBackMainMenu : MonoBehaviour
    {
        private bool waiting = false;

        private void Update()
        {
            if(waiting && Input.anyKeyDown) Menu();
            
        }

        public void GoBack()
        {
            waiting = false;
        }

        private void Menu()
        {
                SceneManager.LoadScene("MainMenu");
        }
    }
}