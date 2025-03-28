using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum GameMode
{
    Menu,
    Fps,
    Narrative,
    Puzzle
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // We singletonning
    
    // References
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_gameModesMenu;
    [SerializeField] private GameObject m_optionsMenu;
    [SerializeField] private GameObject m_keybindsMenu;
    [SerializeField] private GameObject m_accessibilityMenu;
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private FirstSelectedSystem firstSelectedSystem;
    [SerializeField] private GameObject resumePauseButton;
    
    [SerializeField] private SceneAsset m_menuScene;
    [SerializeField] private SceneAsset m_fpsScene;
    [SerializeField] private SceneAsset m_narrativeScene;
    [SerializeField] private SceneAsset m_puzzleScene;
    
    [SerializeField] private InputActionAsset m_inputActionAsset;
    [SerializeField] private InputActionReference m_pauseAction;

    [SerializeField] public EventSystem m_eventSystem;
    
    // Settings
    public float FpsMouseSensitivity { get; set; } = 1.0f;
    public float PuzzleMouseSensitivity { get; set; } = 1.0f;
    public float PlayerSpeed { get; set; } = 100.0f;
    public float EnemySpeed { get; set; } = 100.0f;

    public bool DialogueAnimation { get; set; } = true;
    public float DialogueAnimationSpeed { get; set; } = 1.0f;

    public bool WindowedMode { get; set; } = false;
    
    public bool IsPlaying { get; private set; } = false;

    // Gameplay
    private GameMode CurrentGameMode { get; set; } = GameMode.Menu;

    public int FpsEnemiesKilled { get; set; } = 0;
    
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

    public void Update()
    {
        if (CurrentGameMode == GameMode.Fps)
        {
            if (FpsEnemiesKilled >= 2)
            {
                CurrentGameMode = GameMode.Narrative;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                LaunchGame(m_narrativeScene.name);
            }
        }
    }
    
    public void PlayGame()
    {
        CurrentGameMode = GameMode.Fps;
        SceneManager.LoadScene(m_fpsScene.name);
        ResumeGame();
    }

    public void LaunchGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ResumeGame();
    }

    public void LaunchFps()
    {
        CurrentGameMode = GameMode.Fps;
        LaunchGame(m_fpsScene.name);
    }

    public void LaunchNarrative()
    {
        CurrentGameMode = GameMode.Narrative;
        LaunchGame(m_narrativeScene.name);
    }

    public void LaunchPuzzle()
    {
        CurrentGameMode = GameMode.Puzzle;
        LaunchGame(m_puzzleScene.name);
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
        if (CurrentGameMode != GameMode.Menu)
        {
            IsPlaying = false;
            Time.timeScale = 0;
            m_pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            firstSelectedSystem.SetSelected(resumePauseButton);
        }
    }

    public void ResumeGame()
    {
        IsPlaying = true;
        HideAllMenus();
        Time.timeScale = 1;
        
        if (CurrentGameMode == GameMode.Fps)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void HideAllMenus()
    {
        m_mainMenu.SetActive(false);
        m_gameModesMenu.SetActive(false);
        m_optionsMenu.SetActive(false);
        m_keybindsMenu.SetActive(false);
        m_accessibilityMenu.SetActive(false);
        m_pauseMenu.SetActive(false);
    }
    
    public void GoToMainMenu()
    {
        HideAllMenus();
        m_mainMenu.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (CurrentGameMode != GameMode.Menu)
        {
            LaunchGame(m_menuScene.name);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }

    public void GoToGameModesMenu()
    {
        HideAllMenus();
        m_gameModesMenu.SetActive(true);
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
