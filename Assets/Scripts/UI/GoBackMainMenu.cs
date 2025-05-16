using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GoBackMainMenu : MonoBehaviour
    {
        public void GoBack()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}