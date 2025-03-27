using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // We singletonning
    
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_optionsMenu;
    [SerializeField] private GameObject m_keybindsMenu;
    [SerializeField] private GameObject m_accessibilityMenu;
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private FirstSelectedSystem firstSelectedSystem;
    [SerializeField] private GameObject resumePauseButton;
    
    [SerializeField] private SceneAsset m_gameScene;
    
    [SerializeField] private InputActionAsset m_inputActionAsset;
    [SerializeField] private InputActionReference m_pauseAction;

    // Settings
    public float FpsMouseSensitivity { get; set; } = 1.0f;
    public float PuzzleMouseSensitivity { get; set; } = 1.0f;
    public float PlayerSpeed { get; set; } = 100.0f;
    public float EnemySpeed { get; set; } = 100.0f;

    public bool DialogueAnimation { get; set; } = true;
    public float DialogueAnimationSpeed { get; set; } = 1.0f;

    public bool WindowedMode { get; set; } = false;
    
    public bool IsPlaying { get; private set; } = false;

    public GameManager()
    {
        Instance = this;
    }
    
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        m_inputActionAsset.Enable();
        m_inputActionAsset.FindActionMap("Player").Enable();
        
        m_pauseAction.action.Enable();
        m_pauseAction.action.started += _ => TogglePause();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(m_gameScene.name);
        ResumeGame();
    }

    public void LaunchGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ResumeGame();
    }

    public void TogglePause()
    {
        if (IsPlaying)
            PauseGame();
        else
            ResumeGame();
    }
    
    public void PauseGame()
    {
        IsPlaying = false;
        Time.timeScale = 0;
        m_pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        firstSelectedSystem.SetSelected(resumePauseButton);
    }

    public void ResumeGame()
    {
        IsPlaying = true;
        HideAllMenus();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HideAllMenus()
    {
        m_mainMenu.SetActive(false);
        m_optionsMenu.SetActive(false);
        m_keybindsMenu.SetActive(false);
        m_accessibilityMenu.SetActive(false);
        m_pauseMenu.SetActive(false);
    }
    
    public void GoToMainMenu()
    {
        HideAllMenus();
        m_mainMenu.SetActive(true);
    }
    
    public void GoToOptionsMenu()
    {
        HideAllMenus();
        m_optionsMenu.SetActive(true);
    }

    public void GoToAccessibilityMenu()
    {
        HideAllMenus();
        m_accessibilityMenu.SetActive(true);
    }

    public void GoToKeybindsMenu()
    {
        HideAllMenus();
        m_keybindsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
