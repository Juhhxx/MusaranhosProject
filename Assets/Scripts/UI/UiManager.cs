using System.Collections;
using Map;
using Misc;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private bool inGame;
    [Header("Menu Elements")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject confirmMenu;
    
    [Header("GUI ELements")]
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private CanvasGroup tutorialAlpha;
    [SerializeField] private TMP_Text itemPopupText;
    [SerializeField] private CanvasGroup itemPopupAlpha;
    [SerializeField] private TMP_Text letterPopupText;
    [SerializeField] private CanvasGroup letterPopupAlpha;
    [SerializeField] private float popupDuration;
    [SerializeField] private Image letterImage;
    [SerializeField] private GameObject gameOverUI;

    [Header("Settings Elements")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider gammaSlider;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private Slider voicesVolume;
    [SerializeField] private Slider musicVolume;
    
    private PlayerMovement playerMovement;
    private PlayerLetterReader playerLetterReader;
    private LiftGammaGain gammaController;
    private GameManager gameManager;
    
    private int FullScreen
    {
        get => PlayerPrefs.GetInt("FullScreen", 1);
        set
        {
            PlayerPrefs.SetInt("FullScreen", value);
            Screen.fullScreen = FullScreen == 1;
            //fullScreenIcon.sprite = FullScreen == 1 ? enabledToggleSprite : disabledToggleSprite;
        }
    }

    private float Sensitivity
    {
        get => PlayerPrefs.GetFloat("Sensitivity", 5);
        set
        {
            PlayerPrefs.SetFloat("Sensitivity", value);
            playerMovement.SetSensitivity(Sensitivity);
        }
    }

    private float Gamma
    {
        get => PlayerPrefs.GetFloat("Gamma", 0.78f);
        set
        {
            PlayerPrefs.SetFloat("Gamma", value);
            if(gammaController != null) gammaController.gamma.Override(new Vector4(value,value,value,1f));
        }
    }

    private float MasterVolume
    {
        get => PlayerPrefs.GetFloat("MasterVolume", -20f);
        set
        {
            PlayerPrefs.SetFloat("MasterVolume", value);
            // update mixer
        }
    }

    private float MusicVolume
    {
        get => PlayerPrefs.GetFloat("MusicVolume", -20f);
        set
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            //update mixer
        }
    }

    private float SfxVolume
    {
        get => PlayerPrefs.GetFloat("SfxVolume", -20f);
        set
        {
            PlayerPrefs.SetFloat("SfxVolume", value);
            //Update Mixer
        }
    }

    private void Awake()
    {
        Screen.fullScreen = FullScreen == 1;
    }

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerLetterReader = FindFirstObjectByType<PlayerLetterReader>();
        var postProcessing = FindFirstObjectByType<Volume>();
        if (postProcessing != null)
        {
            if (!postProcessing.profile.TryGet(out gammaController)) return;
        }
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void StartGameButton()
    {
        ChangeScene(1);
    }

    public void ExitGameButton()
    {
        OpenConfirmMenu();
    }

    public void OpenConfirmMenu()
    {
        confirmMenu.SetActive(true);
    }

    public void CloseConfirmMenu()
    {
        confirmMenu.SetActive(false);
    }

    public void ConfirmButton()
    {
        if(inGame) ChangeScene(0);
        else Application.Quit();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        gameManager.Unpause();
        pauseMenu.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        settingsMenu.gameObject.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        settingsMenu.gameObject.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsMenu.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsMenu.gameObject.SetActive(false);
    }

    public void ToggleFullScreen()
    {
        FullScreen = 1 - FullScreen;
    }

    public void PickedUpItem(Item item)
    {
        itemPopupText.text = itemPopupText.text.Replace("%", item.ToString());
        StartCoroutine(Popup(itemPopupAlpha, popupDuration));
    }

    public void PickedUpLetter()
    {
        StartCoroutine(Popup(letterPopupAlpha, popupDuration));
    }

    public void GammaUpdated()
    {
        Gamma = gammaSlider.value;
    }

    public void SensitivityUpdated()
    {
        Sensitivity = sensitivitySlider.value;
    }

    public void MasterVolumeUpdated()
    {
        MasterVolume = masterVolume.value;
    }

    public void MusicVolumeUpdated()
    {
        MusicVolume = musicVolume.value;
    }

    public void SfxVolumeUpdated()
    {
        SfxVolume = sfxVolume.value;
    }

    public void ChangeScene(int index)
    {
         SceneManager.LoadScene(index);
    }

    public IEnumerator Popup(CanvasGroup text, float duration)
    {
        var time = duration;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            text.alpha = time;
            yield return null;
        }
    }

    public void ToggleLetters(bool state)
    {
        letterImage.enabled = state;
        if(state) ChangeLetters();
    }

    public void ChangeLetters()
    {
        letterImage.sprite = playerLetterReader.Letters[playerLetterReader.CurrentLetter].Image;
    }
    
}