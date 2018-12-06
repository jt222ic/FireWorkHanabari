using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


namespace GestureRecognizer
{
	public enum GameState {
        Menu,
        Tutorial,
        Settings,
        Playing,                           // gör så att den inte ser ut som att vora gömd
        Paused,
        LifeLost,
        CountDownToStart,
        CountDownToEnd,
        GameOver }

	/// <summary>
	/// Controls the level (game in general)
	/// </summary>
	public class LevelController : MonoBehaviour
	{
		
		public SoundController soundController;
		public GameObject[] enemyPrefab;
		public GameObject player;
		public GameObject countdownTextContainer;
        public GameObject TutorialContainer;
		public GameObject hud;
		public GameObject scoreBoard;
		public GameObject pauseMenu;
		public GameObject mainMenu;
        public GameObject ShopMenu;
        public GameObject ShopMenu2;
        public GameObject HelpOption;
		public Text menuBestScoreText;
		public Text countdownText;
		public Text livesText;
		public Text scoreText;
        public Text TutorialText;
		public Text[] endScoreText;
		
		[HideInInspector]
		public float enemySpawnRate;
		public static GameState gameState = GameState.Menu;
		public static int healthCurrent;
		public static float screenTop;
		public static float screenBottom;
		public static float screenLeft;
		public static float screenRight;
		public int score;
		private int highScore;
		private float enemySpawnTimer;
		private float countdownToStartTimer;
		private float countdownToEndTimer;
		private float respawnTimerStep = 1f;
		private float countdown;
		private GameObject enemyObject;
		private GameObject enemyToDestroy;
		private List<GameObject> enemyList = new List<GameObject>();
		private List<GameObject> enemyListCopy = new List<GameObject>();
		private PlayerController playerController;
        private List<GameObject> enemyoverlapping = new List<GameObject>();


        public Transform[] spawnpoints;
        private bool EnemyConfirmedKill;
        public GameObject Effect;
        bool BackgroundisNotCheck;
        public GameObject background;
        public Material[] jan;
        private int BackgroundNumber;
        public GameObject SoundOptions;
        private bool OnClick;
        public static int AdsStart;
        public GameObject advertising;
       

        private void Awake()
		{
			// Set application settings
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			// Set high score and show on main menu
			if (!PlayerPrefs.HasKey(Strings.HighScore))
			{
				PlayerPrefs.SetInt(Strings.HighScore, 0);
			}

            
			highScore = PlayerPrefs.GetInt(Strings.HighScore);
			menuBestScoreText.text = PlayerPrefs.GetInt(Strings.HighScore).ToString();
			screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
			screenBottom = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
			screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
			screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
		}
		private void Start()
		{
            Effect.SetActive(true);
            playerController = player.GetComponent<PlayerController>();
		}
		private void Update()
		{
			switch (gameState)
			{
				case GameState.Menu:

					if (Input.GetKeyDown(KeyCode.Escape))
					{
						Application.Quit();
					}
					break;
				case GameState.Playing:
                    if (!enemyObject && enemyList.Count == 0)
                    {
                        float enemyspawnMileStone = 0.5f;
                        enemySpawnTimer += Time.deltaTime;
                        enemySpawnRate = Mathf.Clamp(enemySpawnRate - (Time.deltaTime * Constants.EnemySpawnRateTimeMultiplier), Constants.EnemySpawnRateMin, Constants.EnemySpawnRateMax) * enemyspawnMileStone; // startcountdown
                        if (enemySpawnTimer > enemySpawnRate)
                        {
                            enemySpawnTimer = 0;
                            SpawnEnemy();
                            if (score > 10)
                            {
                                StartCoroutine("WaititOut");
                            }
                        }
                        }
                    break;
				case GameState.Paused:

					if (Input.GetKeyDown(KeyCode.Escape))
					{
						ToggleGame();
					}
					break;

				case GameState.CountDownToStart:

					countdownToStartTimer += Time.deltaTime * Constants.CountDownMultiplier;
                    TutorialText.text = "Draw after Symbol";

                    if (countdownToStartTimer >= respawnTimerStep)
					{
						countdownToStartTimer = 0;
						countdown--;
						countdownText.text = (countdown > 0) ? countdown.ToString() : "GO!";

						if (countdown == -1)
						{
                            TutorialContainer.SetActive(false);
                            countdownTextContainer.SetActive(false);
							countdownText.text = "3";
							gameState = GameState.Playing;
                            //SpawnEnemy();
						}
					}
					break;
				case GameState.CountDownToEnd:

					countdownToEndTimer += Time.deltaTime;
					if (countdownToEndTimer >= Constants.CountDownToEndTimeout)
					{
						FinalizeGame();
					}
					break;
			}
		}
        IEnumerator WaititOut()
        {
            float mindelay = 0.1f;
            float maxdelay = 3f;

            var delay = Random.Range(mindelay, maxdelay);
            yield return new WaitForSeconds(delay);
            SpawnEnemy();
            if(score> 100)
            {
                yield return new WaitForSeconds(delay);
                SpawnEnemy();
            }
            if (score > 200)
            {
                yield return new WaitForSeconds(delay);
                SpawnEnemy();
            }
            if (score > 300)
            {
                yield return new WaitForSeconds(delay);
                SpawnEnemy();
            }
            if (score > 500)
            {
                yield return new WaitForSeconds(delay);
                SpawnEnemy();
            }
            if (score > 800)
            {
                yield return new WaitForSeconds(delay);
                SpawnEnemy();
            }
        }
        private void SpawnEnemy()
		{
            int spawnprefab = Random.Range(0, enemyPrefab.Length);
            GameObject choosen = enemyPrefab[spawnprefab];
            int spawnIndex = Random.Range(0, spawnpoints.Length);
            Transform spawnpoint = spawnpoints[spawnIndex];
            enemyObject = Instantiate(choosen, spawnpoint.position, spawnpoint.rotation) as GameObject;
            enemyObject.GetComponent<EnemyRocket>().levelController = this;
			enemyObject.name = Strings.Enemy;
            enemyList.Add(enemyObject);
          
		}

       public void AddinSpecialEnemy(GameObject unit, Transform mini)
        {
            enemyObject = Instantiate(unit, mini.position, mini.rotation) as GameObject;
            enemyObject.GetComponent<EnemyRocket>().levelController = this;
            enemyObject.name = Strings.Enemy;
            enemyList.Add(enemyObject);
        }
        private GameObject FindTarget(string enemyType)
        {
            foreach (GameObject enemy in enemyList)
            {
                if (enemy.GetComponent<EnemyRocket>().enemy.TypeString == enemyType)
                {
                    return enemy;
                }
            }
            
            return null;
        }
		public void Fire(string enemyType)
		{
           //GameObject target = FindTarget(enemyType);
           GameObject target = FindTarget(enemyType);

            if (target != null)
            {
                EnemyRocket[] enemy = FindObjectsOfType<EnemyRocket>();
                //EnemyRocket enemys = enemy[enemyList.Count -1];

                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemy[i].name == target.name)
                    {
                        enemy[i].KillEnemy();                  // EUREKAAAA FOUND THE SOLUTION TO SPECIFY OBJECT FINDING!!
                    }
                }
                  /*  enemy[enemyList.Count - 1].KillEnemy(); */   // killling the first rocket
                
                }
                
                //EnemyRocket enemys = enemy[];
                //if(target)
                //{
                //    enemys.KillEnemy();
                //}
                
            // now i made that i destroy the first one now it explode the first one
                 
                // destroy specific object in foreach
                //finding the index of each object
                // check if object is correct with the index
                // take the list and take out the object
                // specify object
                    /*     } */                                                                             // this will destroy all rockets

                    //enemy.KillEnemy();
                    //playerController.Fire(target);
                //}
            
            }
		public void ConfirmEnemyKill(GameObject enemyToDestroy, bool scoreTheKill)
		{
			enemyList.Remove(enemyToDestroy);
			playerController.NullTarget();
            if (scoreTheKill)
            {  
                EnemyConfirmedKill = true;
                 IncrementScore();
            } 
		}
		public void ConfirmPlayerKill()
		{
			enemySpawnRate = Mathf.Clamp(enemySpawnRate + Constants.EnemySpawnRateRegain, Constants.EnemySpawnRateMin, Constants.EnemySpawnRateMax);
            gameState = GameState.LifeLost;
			healthCurrent -= 1;
			KillAllEnemies();

			if (healthCurrent == 0)
			{
                //AdsStart += 1;
                //if (PlayerPrefs.HasKey("Adspurchased"))
                //{
                //    Debug.Log("0 ads");
                //    advertising.SetActive(false);
                //}
                //else if (AdsStart > 1)
                //{
                //    advertising.GetComponent<Adwork>().ADWORK();
                //    AdsStart = 0;
                //}
                EndGame();
                
            }
		}
		public void KillAllEnemies(bool scoreTheKill = false)
		{
			foreach (GameObject e in enemyList)
			{
				enemyListCopy.Add(e);
			}
			foreach (GameObject e in enemyListCopy)
			{
				e.GetComponent<EnemyRocket>().KillEnemy(scoreTheKill);
            }
			enemyListCopy.Clear();
			enemyList.Clear();
		}
		public void DestroyAllEnemies()
		{
			for (int i = enemyList.Count - 1; i == 0; i--)
			{
				enemyToDestroy = enemyList[i];
				enemyList.RemoveAt(i);
				Destroy(enemyToDestroy);
			}
		}
		public void DestroyObjects(EnemyRocket[] objectsToDestroy)
		{
			for (int i = 0; i < objectsToDestroy.Length; i++)
			{
				Destroy(objectsToDestroy[i].gameObject);
			}
		}
		public void DestroyObjects(LaserController[] objectsToDestroy)
		{
			for (int i = 0; i < objectsToDestroy.Length; i++)
			{
				Destroy(objectsToDestroy[i].gameObject);
			}
		}
		public void DestroyAllObjects()
		{
			DestroyObjects(GameObject.FindObjectsOfType<EnemyRocket>());
			DestroyObjects(GameObject.FindObjectsOfType<LaserController>());
		}
		public void RespawnPlayer()
		{
			gameState = GameState.Playing;
			UpdateLives();
            
		}
		public void IncrementScore()
		{
			score++;
			UpdateScore();
		}
		public void ToggleGame()
		{
			if (gameState == GameState.Playing)
			{
				soundController.DoMusic(SoundAction.Pause);
				gameState = GameState.Paused;
				pauseMenu.SetActive(true);
				Time.timeScale = 0;
           
			}
			else if (gameState == GameState.Paused)
			{
				soundController.DoMusic(SoundAction.Play);
				gameState = GameState.Playing;
				pauseMenu.SetActive(false);
				Time.timeScale = 1;
               
			}
		}
		public void StartGame()
		{
            SoundOptions.SetActive(false);
            Effect.SetActive(false);
            player.transform.rotation = Quaternion.identity;
			enemyList.Clear();
			enemyListCopy.Clear();
			gameState = GameState.CountDownToStart;
			healthCurrent = Constants.HealthDefault;
			score = Constants.ZeroDefault;
			countdown = Constants.CountDownDefault;
			countdownToStartTimer = Constants.ZeroDefault;
			countdownToEndTimer = Constants.ZeroDefault;
			enemySpawnTimer = Constants.ZeroDefault;
			enemySpawnRate = Constants.EnemySpawnRateMax;
			countdownTextContainer.SetActive(true);

            TutorialContainer.SetActive(true);
            hud.SetActive(true);
			mainMenu.SetActive(false);
			scoreBoard.SetActive(false);
			pauseMenu.SetActive(false);
			playerController.StartPlayer();
			Time.timeScale = 1;
		}
		private void EndGame()
		{
     
            gameState = GameState.CountDownToEnd;
            Effect.SetActive(true);
            if (score > highScore)
			{
				highScore = score;
				PlayerPrefs.SetInt(Strings.HighScore, highScore);
                PlayerPrefs.SetInt("Score", score);
            }
          

            hud.SetActive(false);
		}
		private void FinalizeGame()
		{
			DestroyAllObjects();
			countdownToEndTimer = Constants.ZeroDefault;
			gameState = GameState.GameOver;
			scoreBoard.SetActive(true);
			endScoreText[0].text = score.ToString();
		}
		public void RestartGame()
		{
            healthCurrent = 3;
            UpdateLives();
            DestroyAllObjects();
			EndGame();
            StartGame();
			soundController.DoMusic(SoundAction.Play);
		}
        public void OpenShop()
        {
            scoreBoard.SetActive(false);
            ShopMenu.SetActive(true);
            DestroyAllObjects();
            endScoreText[1].text = score.ToString();
        }
        public void OpenShop2()
        {
          
            mainMenu.SetActive(false);
            ShopMenu2.SetActive(true);
            DestroyAllObjects();
            endScoreText[1].text = score.ToString();
        }

        public void OpenSoundOptions()
        {
            if (!OnClick)
            {
                SoundOptions.SetActive(true);
                OnClick = true;
            }
            else
            {
                SoundOptions.SetActive(false);
                OnClick = false;
            }
        }
        public void OpenHelpOption()
        {
            HelpOption.SetActive(true);
            mainMenu.SetActive(false);
        }
        public void ExitHelpOption()
        {
            HelpOption.SetActive(false);
            mainMenu.SetActive(true);
        }
        public void SwitchBackground()
        {
            if (!BackgroundisNotCheck)
            {
                background.SetActive(true);
                if(BackgroundNumber == 0)
                {
                    background.GetComponent<MeshRenderer>().material = jan[BackgroundNumber];
                    BackgroundNumber++;
                }
                else if(BackgroundNumber == 1)
                {
                    background.GetComponent<MeshRenderer>().material = jan[BackgroundNumber];
                    BackgroundNumber++;
                }
                else if (BackgroundNumber == 2)
                {
                    background.GetComponent<MeshRenderer>().material = jan[BackgroundNumber];
                    BackgroundNumber++;
                }
                else if (BackgroundNumber == 3)
                {
                    background.GetComponent<MeshRenderer>().material = jan[BackgroundNumber];
                    BackgroundNumber++;
                }
                else if (BackgroundNumber == 4)
                {
                    background.GetComponent<MeshRenderer>().material = jan[BackgroundNumber];
                    BackgroundNumber++;
                }
                else if (BackgroundNumber == 5)
                {
                    background.GetComponent<MeshRenderer>().material = jan[BackgroundNumber];
                    BackgroundNumber++;
                }
                else
                {
                    BackgroundNumber = 0;
                    BackgroundisNotCheck = true;
                }
            }
            else if (BackgroundisNotCheck)
            {
                background.SetActive(false);
                BackgroundisNotCheck = false;
            }
        }
        public void QuitGame()
		{
			DestroyAllObjects();
			playerController.EndPlayer();
			gameState = GameState.Menu;
			mainMenu.SetActive(true);
			scoreBoard.SetActive(false);
			pauseMenu.SetActive(false);
			hud.SetActive(false);
			soundController.DoMusic(SoundAction.Play);
		}
		private Vector2 ScreenToWorld(Vector3 position)
		{
			Vector3 worldCoordinate = new Vector3(position.x, position.y, 10);
			return Camera.main.ScreenToWorldPoint(worldCoordinate);
		}
		private void UpdateLives()
		{
			livesText.text = healthCurrent.ToString();
		}
		private void UpdateScore()
		{
			scoreText.text = score.ToString("000");
		}
	} 
}