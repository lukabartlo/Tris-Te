using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Configuration UI Toolkit")]
    [SerializeField] private string playButtonName = "playBtn";
    [SerializeField] private string quitButtonName = "quitBtn";
    [SerializeField] private int sceneNumberToLoad = 1;

    private readonly List<PanelRenderer> _registeredRenderers = new List<PanelRenderer>();
    private Button _playButton;
    private Button _quitButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        
          
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void OnEnable()
    {
        if (Instance != this) return;

        SceneManager.sceneLoaded += OnSceneLoaded;
        RegisterPanelRenderers();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        UnregisterPanelRenderers();
        UnbindButtons();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      
        RegisterPanelRenderers();
    }

   
    private void RegisterPanelRenderers()
    {
        UnregisterPanelRenderers();

        foreach (PanelRenderer panelRenderer in FindObjectsByType<PanelRenderer>())
        {
            panelRenderer.RegisterUIReloadCallback(OnUIReload);
            _registeredRenderers.Add(panelRenderer);
        }
    }

    private void UnregisterPanelRenderers()
    {
        foreach (PanelRenderer panelRenderer in _registeredRenderers)
        {
            if (panelRenderer != null)
            {
                panelRenderer.UnregisterUIReloadCallback(OnUIReload);
            }
        }

        _registeredRenderers.Clear();
    }

    private void OnUIReload(PanelRenderer panelRenderer, VisualElement rootElement, int version)
    {
        BindButton(rootElement, playButtonName, ref _playButton, OnPlayButtonClicked);
        BindButton(rootElement, quitButtonName, ref _quitButton, OnQuitButtonClicked);
    }

    private static void BindButton(VisualElement root, string buttonName, ref Button current, Action onClick)
    {
        Button button = root.Q<Button>(buttonName);
        if (button == null) return;

        if (current != null)
        {
            current.clicked -= onClick;
        }

        current = button;
        current.clicked += onClick;
    }

    private void UnbindButtons()
    {
        if (_playButton != null)
        {
            _playButton.clicked -= OnPlayButtonClicked;
            _playButton = null;
        }

        if (_quitButton != null)
        {
            _quitButton.clicked -= OnQuitButtonClicked;
            _quitButton = null;
        }
    }

    private void OnPlayButtonClicked()
    {
        PlayGame(sceneNumberToLoad);
    }

    private void OnQuitButtonClicked()
    {
        QuitGame();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayGame(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
