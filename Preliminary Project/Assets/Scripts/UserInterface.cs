using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
	public GameObject canvas;
	public GameObject storyCanvas;
	public GameObject startStory;
	public GameObject endStory;
	static private bool end = false;

	void Update()
	{
		if(end) {
			ShowEndStory();
			end = false;
		}
	}

	public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		HUD.SetEnable(true);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SetVolume(float volume)
	{
		SoundManager.SetVolume(volume);
	}

	public void ShowStartStory()
	{
		canvas.SetActive(false);
		storyCanvas.SetActive(true);
		startStory.SetActive(true);
		endStory.SetActive(false);
	}

	public void ShowEndStory()
	{
		canvas.SetActive(false);
		storyCanvas.SetActive(true);
		startStory.SetActive(false);
		endStory.SetActive(true);
	}

	public void DisplayMenu()
	{
		canvas.SetActive(true);
		storyCanvas.SetActive(false);
		startStory.SetActive(false);
		endStory.SetActive(false);
	}

	static public void OnGameOver()
	{
		end = true;
	}
}
