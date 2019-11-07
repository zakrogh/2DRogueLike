using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	public float turnDelay = 0.1f;
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int playerFoodPoints = 300;
	[HideInInspector] public bool playersTurn = true;

	private Text levelText;
	private GameObject levelImage;
	private int level = 20;
	private List<Enemy> enemies;
	private bool enemiesMoving;
	private bool doingSetup = true;
	public RingAttack ringAttack;


	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy>();
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	// private void OnLevelWasLoaded(int index)
	// {
	// 	 level++;
	// 	 InitGame();
	// }
	// void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	// {
	// 	level++;
	// 	InitGame();
	// }
	// void OnEnable()
	// {
	// 	SceneManager.sceneLoaded += OnLevelFinishedLoading;
	// }
	// void OnDisable()
	// {
	// 	SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	// }
	//this is called only once, and the paramter tell it to be called only after the scene was loaded
//(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
static public void CallbackInitialization()
{
		//register the callback to be called everytime the scene is loaded
		SceneManager.sceneLoaded += OnSceneLoaded;
}

//This is called each time a scene is loaded.
static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
{
		instance.level++;
		instance.InitGame();
}

	void InitGame() {
		doingSetup = true;
	  ringAttack = GetComponent<RingAttack>();
		levelImage = GameObject.Find("LevelImage");
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		levelText.text = "Day " + level;
		levelImage.SetActive(true);
		Invoke("HideLevelImage", levelStartDelay);

		enemies.Clear();
		boardScript.SetupScene(level);
	}

	private void HideLevelImage()
	{
		levelImage.SetActive(false);
		doingSetup = false;
	}
	public void GameOver()
	{
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive(true);
		enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if(playersTurn || enemiesMoving || doingSetup)
			return;

		StartCoroutine(MoveEnemies());
	}
	public void AddEnemyToList(Enemy script)
	{
		enemies.Add(script);
	}

	IEnumerator MoveEnemies()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds(turnDelay);
		if(enemies.Count == 0)
		{
			yield return new WaitForSeconds(turnDelay);
		}
		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}
		playersTurn = true;
		enemiesMoving = false;
	}
}
