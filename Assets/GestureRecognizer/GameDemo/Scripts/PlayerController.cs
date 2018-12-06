using UnityEngine;

namespace GestureRecognizer
{
	public enum PlayerState { Alive, Dead, Blinking }

	/// <summary>
	/// Controls the player behaviour
	/// </summary>
	public class PlayerController : MonoBehaviour
	{
		public GameObject laserPrefab;
		public GameObject explosionPrefab;
		public GameObject engines;
		public MeshRenderer shipRenderer;
		public LevelController levelController;
		public GestureBehaviour gestureBehaviour;
		private PlayerState playerState;
		private float angularSpeed = 20f;
		private float angle;
		private float deathTimer;
		private float blinkTimer;
		private int numberOfBlinksCurrent;
		private GameObject laserObject;
		private Transform target;
		private Vector3 targetVector;
		private Quaternion q;
        
		void Awake()
		{
			GestureBehaviour.OnGestureRecognition += OnRecognizeShape;
        }
        void Update()
        {
            if (LevelController.gameState == GameState.Playing || LevelController.gameState == GameState.LifeLost)
            {
                if (playerState != PlayerState.Dead)
                {
                    if (target != null)
                    {
                        targetVector = target.position - transform.position;
                        angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
                        q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * angularSpeed);
                    }

                    if (playerState == PlayerState.Blinking)
                {
                    blinkTimer += Time.deltaTime;

                    if (blinkTimer >= Constants.PlayerBlinkTimeout)
                    {
                        blinkTimer = 0;
                        numberOfBlinksCurrent++;
                        Blink(!shipRenderer.enabled);

                        if (numberOfBlinksCurrent == Constants.PlayerMaxBlinks)
                        {
                            numberOfBlinksCurrent = 0;
                            playerState = PlayerState.Alive;
                            Blink(true);
                        }
                    }
                }

            }
            else
            {
                deathTimer += Time.deltaTime;

                if (deathTimer >= Constants.PlayerDeathTimeout)
                {
                    deathTimer = 0;
                    playerState = PlayerState.Blinking;
                    levelController.RespawnPlayer();
                }
            }
        }
    }


    public void Fire(GameObject target)
        {

            if (playerState != PlayerState.Dead)
            {
                this.target = target.transform;

                laserObject = Instantiate(laserPrefab, this.transform.position, Quaternion.identity) as GameObject;
                laserObject.GetComponent<LaserController>().target = this.target;
                laserObject.name = Strings.Laser;
            }

        }
        public void NullTarget()
		{
			target = null;
		}
		public void StartPlayer()
		{
			playerState = PlayerState.Alive;
			deathTimer = Constants.ZeroDefault;
			blinkTimer = Constants.ZeroDefault;
			numberOfBlinksCurrent = Constants.ZeroDefault;
			Blink(true);
		}
		public void EndPlayer()
		{
			Blink(false);
		}
		void OnRecognizeShape(Gesture g, Result r)
		{
			gestureBehaviour.ClearGesture();
            levelController.Fire(r.Name);
        }
		void OnTriggerEnter(Collider other)
		{
			if (other.name == Strings.Enemy && playerState == PlayerState.Alive)
			{
				KillPlayer();
				other.GetComponent<EnemyRocket>().KillEnemy(false);
			}
		}
		public void KillPlayer()
		{
            if(playerState != PlayerState.Dead)
            {
                levelController.ConfirmPlayerKill();
                Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
                playerState = PlayerState.Dead;
                Blink(false);
            }
		}
		void Blink(bool isOn)
		{
			shipRenderer.enabled = isOn;
			engines.SetActive(isOn);
		}
	} 
}