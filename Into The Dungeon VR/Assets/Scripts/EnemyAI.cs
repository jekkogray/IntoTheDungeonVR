using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public bool move = true;
    public bool checkEnemies = false;
    public GameObject enemiesChecker;
    public int scoreValue = 5;
    [Range(0, 1000)] public int enemyHealth = 20;
    [Range(0, 100)] public int enemyDamage = 5;
    public GameObject headset;
    public int maxEnemyHealth;
    public AudioSource audioSource;
    public GameObject bullet;
    public bool bulletsEnabled = true;
    public float bulletSpeed = 50;
    public AudioClip projectileAudioClip;
    public AudioClip deathAudioClip;
    public GameObject playerObject;
    public NavMeshAgent agent;
    private Transform playerTransformation;

    public LayerMask whatIsGround, whatIsPlayer;


    // Patroling for walk point 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Range(0, 10)] public float timeBetweenAttacks = 4f;
    bool alreadyAttacked;

    //States
    [Range(0, 20)] public float sightRange = 20;
    [Range(0, 20)] public float attackRange = 5;
    public bool playerInSightRange, playerInAttackRange;
    private void Start()
    {
        maxEnemyHealth = enemyHealth;
    }
    private void Awake()
    {
        playerTransformation = playerObject.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Patrolling()
    {
        if (move)
        {
            if (!walkPointSet) GenerateWalkPoint();
            if (walkPointSet) agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // Reached the destination
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
    }

    private void GenerateWalkPoint()
    {
        // Initialize a random coordinate on the (X,Z) axis for the enemy AI to move into.
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        // Provide a new walkPoint for the character
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);


        // Check if it is actually on the ground using a raycast
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void ChasePlayer()
    {
        transform.LookAt(headset.transform);
        if (move)
            agent.SetDestination(playerTransformation.position);
    }
    private void AttackPlayer()
    {
        if (move)
        {
            agent.SetDestination(playerTransformation.position / 2);
            transform.LookAt(headset.transform);
        }
        if (!alreadyAttacked && bulletsEnabled)
        {
            // Attack here
            GameObject spawnedBullet = Instantiate(bullet, agent.transform.position, agent.transform.rotation);
            audioSource.PlayOneShot(projectileAudioClip);

            // Provide rigid body to the instantiated bullet
            if (spawnedBullet.GetComponent<Rigidbody>() != null)
            {
                spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * transform.forward;
            }
            else
            {
                spawnedBullet.AddComponent<Rigidbody>().velocity = bulletSpeed * transform.forward;
            }
            spawnedBullet.gameObject.tag = "Enemy Bullet";
            BulletBehavior bulletBehavior = spawnedBullet.GetComponent<BulletBehavior>();
            bulletBehavior.sourceSpawn = gameObject;
            bulletBehavior.bulletDamage = enemyDamage;

            // Play attack sound
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            enemyHealth -= collision.gameObject.GetComponent<BulletBehavior>().bulletDamage;
            Score.scoreValue += 5;
            if (enemyHealth <= 0)
            {
                if (checkEnemies)
                {
                    enemiesChecker.GetComponent<AreEnemiesDead>().KilledEnemy(gameObject);
                    enemiesChecker.GetComponent<AreEnemiesDead>().EnemiesDead();
                }
                Score.scoreValue += scoreValue;
                Debug.Log(gameObject.name + " Dead.");
                GameObject deathSound = new GameObject();
                deathSound.AddComponent<AudioSource>().PlayOneShot(deathAudioClip);
                gameObject.SetActive(false);
                Destroy(deathSound, deathAudioClip.length);
                // Destroy(gameObject);
                return;
            }
            Debug.Log(collision.gameObject.name);
            Debug.Log("Damage: -" + collision.gameObject.GetComponent<BulletBehavior>().bulletDamage);
            Debug.Log("Enemy Health: " + enemyHealth + "/" + maxEnemyHealth);
        }
    }
    void ResetAttack()
    {
        audioSource.PlayOneShot(projectileAudioClip);
        alreadyAttacked = false;
    }

    void Update()
    {
        // Check if there are any players around
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

}
