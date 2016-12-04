using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickToPlay : MonoBehaviour
{
	public static ClickToPlay instance;

    public bool pCanPause = true;
    public bool escCanQuit = true;
	public bool escGivesQuitMenu = true;
	public bool disablePlayerSelectButtonForMenu = true;
    public bool rCanRestart = true;
    public bool paused = false;
	public GameObject escScreen;
    public GameObject pauseScreen;
    public Image theSlidesCanvas;

    public bool usingSlides = false;
    public Sprite[] slides;

	[Tooltip("This button is selected when you go to Esc menu")] public Button defaultButton;
	[Tooltip("Screens to turn off when going back to game from Esc menu, like options")] public GameObject[] screensToDisableOnResume;

    public int whichSlideToShow = 0;

    void Awake()
    {
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
        Cursor.visible = true;
    }

    void Start()
    {
        if (usingSlides)
            NextSlide(0);
    }

    void Update()
    {
        if (pCanPause && Input.GetKeyDown(KeyCode.P))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;
                if (pauseScreen != null)
                    pauseScreen.SetActive(true);
            }
            else if (paused)
            {
                Time.timeScale = 1;
                paused = false;
                if (pauseScreen != null)
                    pauseScreen.SetActive(false);
            }

        }

        if (escCanQuit && Input.GetKeyDown(KeyCode.Escape))
            QuitGame();

        if (rCanRestart && Input.GetKeyDown(KeyCode.R))
        {
			RestartLevel();
		}

		if(escGivesQuitMenu && !paused && Input.GetButtonDown("Cancel"))
		{
			EscMenu();
		}
		else if(escGivesQuitMenu && paused && Input.GetButtonDown("Cancel"))
		{
			ResumeFromEscMenu();
		}
    }

    public void StartGame(int whichLevel)
    {
        Application.LoadLevel(whichLevel);
    }

	public void EscMenu()
	{
		escScreen.SetActive(true);
		defaultButton.Select ();
		Time.timeScale = 0;
		paused = true;
	}

	public void ResumeFromEscMenu()
	{
		foreach (GameObject screen in screensToDisableOnResume)
		{
			screen.SetActive(false);
		}
		Time.timeScale = 1;
		paused = false;
	}

	public void RestartLevel()
	{
		ResumeFromEscMenu ();
		Application.LoadLevel (Application.loadedLevel);
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextSlide(int fwdOrBackInt)
    {
        whichSlideToShow += fwdOrBackInt;

        if (whichSlideToShow < 0)
            whichSlideToShow = 0;

        else if (whichSlideToShow == slides.Length)
            whichSlideToShow -= 1;

        theSlidesCanvas.sprite = slides[whichSlideToShow];
    }

    public void DestroyMusicManager()
    {
        Destroy(GameObject.FindGameObjectWithTag("MusicManager"));
    }
}
