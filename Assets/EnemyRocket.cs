using UnityEngine;
using System.Collections;
using System;

namespace GestureRecognizer
{
    public class EnemyRocket : MonoBehaviour
    {
        [HideInInspector]
        public LevelController levelController;
        [HideInInspector]
        public Enemy enemy = new Enemy();
        Rigidbody2D myrigid;
        public float Startforce = 30f;
        public Sprite[] gestureSprites;
        public SpriteRenderer enemyShape;
        public Collider2D enemyCollider;
        //private Vector3 targetVector;
       public EventHandler Destruction;


           
        public GameObject explosionObject;
        private ParticleSystem particle;
        public GameObject enemycore;
        public float rotationSpeed = 0.5f;
        public bool dead;
        public float performTime = 2f;
        public float AmountTime = 2f;
        public bool Alreadyperform;
        public PlayerController playerController;
        public AudioSource Rocketwhistle;
        public AudioSource RocketExplosion;
        
        void Start()
        {
            enemyShape.sprite = gestureSprites[(int)enemy.Type];
            particle = explosionObject.GetComponent<ParticleSystem>();
            myrigid = GetComponent<Rigidbody2D>();
            myrigid.AddForce(transform.up * Startforce, ForceMode2D.Impulse);
            playerController = FindObjectOfType<PlayerController>();
            this.name = enemy.TypeString;
            Rocketwhistle.Play();
            StartCoroutine("Killplayer");
        }

       
        void Update()
        {  
            //StartCoroutine("fallNow");
            Vector2 moveDirection = myrigid.velocity;
            if (moveDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            if (dead)
            {
                transform.rotation = Quaternion.identity;
            }
        }
        IEnumerator Killplayer()
        {
            yield return new WaitForSeconds(4.5f);
            if (Alreadyperform != true)
            {
                playerController.KillPlayer();
                Alreadyperform = true;
            }
        }
        /// <param name="scoreTheKill"></param>
        public void KillEnemy(bool scoreTheKill = true)
        {
            RocketExplosion.Play();
            dead = true;
            levelController.ConfirmEnemyKill(gameObject, scoreTheKill);
            explosionObject.SetActive(true);
            enemycore.SetActive(false);
            enemyCollider.enabled = false;
            StartCoroutine("CheckIfAlive");
        }
        IEnumerator CheckIfAlive()
        {
                yield return new WaitForSeconds(0.42f);
                GameObject.Destroy(gameObject);
        }
    }
}

