// This script is a Manager that controls the the flow and control of the game. It keeps
// track of player data (orb count, death count, total game time) and interfaces with
// the UI Manager. All game commands are issued through the static methods of this class

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	//This class holds a static reference to itself to ensure that there will only be
	//one in existence. This is often referred to as a "singleton" design pattern. Other
	//scripts access this one through its public static methods
	static GameManager current;

	public float deathSequenceDuration = 1f;	//How long player death takes before restarting
	private int numberScenes;					//Number of scenes in the game

	bool isGameOver;							//Is the game currently over?

	void Awake()
	{
		//If a Game Manager exists and this isn't it...
		if (current != null && current != this)
		{
			//...destroy this and exit. There can only be one Game Manager
			Destroy(gameObject);
			return;
		}

		//Set this as the current game manager
		current = this;

		//Persist this object between scene reloads
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		numberScenes = SceneManager.sceneCountInBuildSettings;
	}

	void Update()
	{
		//If the game is over, exit
		if (isGameOver)
			return;
	}

	public static bool IsGameOver()
	{
		//If there is no current Game Manager, return false
		if (current == null)
			return false;

		//Return the state of the game
		return current.isGameOver;
	}

	public static void PlayerDied()
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Invoke the RestartScene() method after a delay
		current.Invoke("RestartScene", current.deathSequenceDuration);
	}

	public static void PlayerWon()
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//The game is now over
		current.isGameOver = true;
	}

	void RestartScene()
	{
		//Reload the current scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}

	public static void PlayerReachedNextLevel()
	{
		int index = SceneManager.GetActiveScene().buildIndex + 1;

		//Check if there are more levels
		if(index >= current.numberScenes) {
			GameManager.PlayerWon();
			Debug.Log("Won");
			return;
		}

		//Loads the next scene
    	SceneManager.LoadScene(index);
	}  
}
