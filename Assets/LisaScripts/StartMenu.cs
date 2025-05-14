using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private string startScene;
    
    public void InitGame()
    {
        TimerToChange();

        SceneManager.LoadScene(startScene);
    }

    IEnumerator TimerToChange()
    {
        yield return new WaitForSeconds(2f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
