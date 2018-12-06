//using UnityEngine;
//using System.Collections;

//namespace GestureRecognizer
//{
//	/// <summary>
//	/// Controls an enemy behaviour
//	/// </summary>
//	public class EnemyController : MonoBehaviour
//	{
//		/// <summary>
//		/// Sprites are used to display enemy types on the screen
//		/// </summary>
//		public Sprite[] gestureSprites;

//		/// <summary>
//		/// Explosion game object
//		/// </summary>
//		public GameObject explosionObject;


//        public float timer;
//		/// <summary>
//		/// Ship itself
//		/// </summary>
//		public GameObject enemyShip;

//		/// <summary>
//		/// The sprite renderer which displays the enemy type
//		/// </summary>
//		public SpriteRenderer enemyShape;

//		/// <summary>
//		/// Collider
//		/// </summary>
//		public Collider enemyCollider;

//		/// <summary>
//		/// The enemy this behaviour is based on
//		/// </summary>
		
//		public Enemy enemy;

//        /// <summary>
//        /// Reference of the level controller
//        /// </summary>
//        [HideInInspector]
//        public LevelController levelController;

	
//		private float speed;

		
//		private Vector3 targetVector;

	
//		private ParticleSystem explosionPS;

//        public Rigidbody2D body;
//        public float rotationSpeed = 1.0f;

//        public float performTime;
//        public int AmountTime = 9;
//        public bool Alreadyperform;
      
//        public PlayerController playerController;
//        private void Start()
//		{
//			enemy = new Enemy();
//			enemyShape.sprite = gestureSprites[(int)enemy.Type];

//			targetVector = (levelController.player.transform.position - transform.position).normalized;   //Target player 
//			speed = Constants.EnemySpeed / levelController.enemySpawnRate;
//			explosionPS = explosionObject.GetComponent<ParticleSystem>();
//            playerController = FindObjectOfType<PlayerController>();
//        }
//        void Update()
//        {
//           //performTime += Time.deltaTime;
//           //if(Alreadyperform!= true && performTime > AmountTime)
//           // {
//           //     //playerController.KillPlayer();
//           //     Alreadyperform = true;
//           // }

//           // Vector2 moveDirection = body.velocity;
//           // if (moveDirection != Vector2.zero)
//           // {
//           //     float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
//           //     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//           // }
//        }
//        //private void Update()
//        //{
//        //    if (targetVector != null)
//        //    {
//        //        //var lookDir = targetVector - transform.position;
//        //        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
//        //        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
//        //        //transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationSpeed * Time.deltaTime);

//        //        float step = speed * Time.deltaTime;
//        //        //transform.position = Vector3.MoveTowards(transform.position, targetVector, step);
//        //        transform.position += targetVector * Time.deltaTime * speed;
//        //        timer += Time.deltaTime;
//        //        if (timer < 1)
//        //        {
//        //            body.velocity = transform.up * ((speed + 1) * timer);
//        //        }
//        //        if (timer >= 1 && timer < 2)
//        //        {
//        //            body.velocity = transform.up * -((speed + 1) * (timer - 1));
//        //        }
//        //        if (timer > 2)
//        //        {
//        //            timer = 0;
//        //        }

//        //    }
//        //    //transform.position += targetVector * Time.deltaTime * speed;
//        //}
//        /// <summary>
//        /// Kill the enemy, score the player if necessary
//        /// </summary>
//        /// <param name="scoreTheKill"></param>
//        public void KillEnemy(bool scoreTheKill = true)
//		{
//			levelController.ConfirmEnemyKill(gameObject, scoreTheKill);
//			explosionObject.SetActive(true);
//			enemyShip.SetActive(false);
//			enemyCollider.enabled = false;
//			StartCoroutine("CheckIfAlive");
//		}
//		IEnumerator CheckIfAlive()
//		{
//			while (true)
//			{
//				yield return new WaitForSeconds(0.5f);
//			    GameObject.Destroy(gameObject);
				
//			}
//		}
//	} 
//}