﻿// This script is a Manager that controls the the flow and control of the game. It keeps
// track of player data and interfaces with the UI Manager.
// All game commands are issued through the static methods of this class

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	//Singleton
	static GameManager current;

    static public PlayerShooting playerShooting;
	static public PlayerHealth playerHealth;
	public float deathSequenceDuration = 1f;	//How long player death takes before restarting
	private int numberScenes;					//Number of scenes in the game

	static bool isGameOver;						//Is the game currently over?
    static private int playerPens = 0;
	static private int playerHP = PlayerHealth.initialHealth;


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
        playerPens = 0;
		isGameOver = false;
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
		return isGameOver;
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

		//Debug.Log("Won");

		//The game is now over
		isGameOver = true;

		//Go to menu
		ReturnToMenu();
		UserInterface.OnGameOver();
	}

	void RestartScene()
	{
		//Reload the current scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}

	public static void SetPlayerShooting(PlayerShooting ps)
	{
		playerShooting = ps;
	}

	public static void SetPlayerHealth(PlayerHealth ph)
	{
		playerHealth = ph;
	}

    public static void SavePens()
    {
        playerPens = playerShooting.GetPens();
    }

    public static int GivePens()
    {        
        return playerPens;
    }

	public static void SaveHP()
    {
        playerHP = playerHealth.GetHP();
    }

    public static int GiveHP()
    {        
        return playerHP;
    }

	public static void PlayerReachedNextLevel()
	{
        SavePens();
		SaveHP();
        int index = SceneManager.GetActiveScene().buildIndex + 1;

		//Check if there are more levels
		if(index >= current.numberScenes) {
			GameManager.PlayerWon();
			return;
		}

		//Loads the next scene
		isGameOver = false;
    	SceneManager.LoadScene(index);
		HUD.SetEnable(true);
	}  

	public static void ReturnToMenu()
	{
		SceneManager.LoadScene(0); 
		HUD.SetEnable(false);
		playerHP = PlayerHealth.initialHealth;
		playerPens = 0;
	}
}
