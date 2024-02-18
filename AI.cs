using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;
using UnityEngine.Timeline;


[RequireComponent(typeof(NavMeshAgent))]
public class TestAi : MonoBehaviour
{
    [Header("LayerMask")]
    public LayerMask playerLayer,coverLayer;
    
    [Space(10)]
    [Header("Audio")]
    public AudioSource audioSource;
    
    [Space(10)]
    [Header("Transform")]
    public Transform target;
    
    [Space(10)]
    [Header("Gameobject")]
    public GameObject barrel;
    public GameObject ragdoll;
    public GameObject weapon;
    public GameObject AmmoBox;
    
    [Space(10)]
    [Header("EnemyType")]
    public bool IsShielder;
    
    [Space(10)]
    [Header("Float Val")]
    public static float speed;
    public float Hp = 100f, Ammo = 12f, MaxAmmo= 12f;
    private float detectionArea = 30f, ShootingRange = 15f, RetreatArea = 7f, MeleeAttackRange = 5f ,rotationSpeed = 15f;
    private float damage = 10f,meleeDamage = 15f, cooldownDuration = 0.8f,meleeCooldown = 3f,chanceToHit = 0.7f,ReloadDuration = 2f;              
    
    Vector3 destination;
    NavMeshAgent agent;
    private bool canShoot = true,isInShootingRange,hasAttacked;
    public List<Transform> coverLocations;
    

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        agent.stoppingDistance = 15f;
        speed = agent.speed;
    }
    
    void Update(){
        if(Physics.CheckSphere(transform.position, detectionArea, playerLayer)){
            Chase();
            FaceTarget();
            isInShootingRange = false;
        }

        if(Physics.CheckSphere(transform.position, ShootingRange, playerLayer) && canShoot){
            Shoot();
            isInShootingRange = true;
        }

        if(Physics.CheckSphere(transform.position, RetreatArea, playerLayer) && hasAttacked){
            Retreat();
        }
        
        if(Physics.CheckSphere(transform.position, MeleeAttackRange, playerLayer) && !hasAttacked){
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        StartCoroutine(MeleeAttackCooldown());

        RaycastHit raycastHit;
        if(Physics.Raycast(barrel.transform.position, barrel.transform.forward, out raycastHit , MeleeAttackRange)){
            PlayerManger playerManger = raycastHit.transform.GetComponent<PlayerManger>();

            Debug.Log(raycastHit.transform.name);

            if(playerManger != null){
                playerManger.UpdateHealth(meleeDamage);
                playerManger.Stun();
                hasAttacked = true;
            }
        }
    }
    
    public void UpdateHealth(float damage){
        Hp -= damage;
        if(Hp <= 0){
            Death();
        }
        
    }

    public void Death(){
        Instantiate(ragdoll, transform.position, transform.rotation);
        Instantiate(weapon, transform.position, transform.rotation);
        Instantiate(AmmoBox, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Chase(){
        destination = target.position;
        agent.destination = destination;
    }

    void FaceTarget(){
        Vector3 faceTarget = target.position - transform.position;
        

        float time = rotationSpeed * Time.deltaTime;
        
        Quaternion rotation = Quaternion.LookRotation(faceTarget, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation , rotation , time);
    }

    void Shoot(){
        Sound(); 
        StartCoroutine(ShootCooldown());

        RaycastHit raycastHit;
        if(Physics.Raycast(barrel.transform.position, barrel.transform.forward, out raycastHit , ShootingRange)){
            PlayerManger playerManger = raycastHit.transform.GetComponent<PlayerManger>();
            TestAi testAi = raycastHit.transform.GetComponent<TestAi>();

            Debug.Log(raycastHit.transform.name);

            if(playerManger != null && Random.value < chanceToHit){
                playerManger.UpdateHealth(damage);
            }

            if(testAi != null && Random.value < chanceToHit){
                testAi.UpdateHealth(damage);
            }
        }
    }

    void Retreat(){
        Vector3 retreatPoint = transform.position + (transform.position - target.position);
        destination = retreatPoint;
        agent.destination = destination;
    }

    void Sound(){
        audioSource.Play();
    }

    IEnumerator MeleeAttackCooldown(){
        hasAttacked = true;
        yield return new WaitForSeconds(meleeCooldown);
        hasAttacked = false;
    }

    IEnumerator ShootCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(cooldownDuration);
        canShoot = true;
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;


        Vector3 direction = transform.TransformDirection(Vector3.forward) * ShootingRange;
        Gizmos.DrawRay(barrel.transform.position, direction);
        Gizmos.DrawWireSphere(transform.position, detectionArea);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);
        Gizmos.DrawWireSphere(transform.position, RetreatArea);
    }
}

