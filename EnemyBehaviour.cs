using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float attackDistance = 1.5f;
    [SerializeField] float cooldownLength = 1f; // seconds
    [SerializeField] float attackDmg = 20f;
    [SerializeField] float attackRange = 2f;
    public NavMeshAgent agent;
    public Transform player;
    private Animator animator;
    private float elapsedTime = 0f;
    private enum EnemyState { Chasing, Attacking, Cooldown };
    private EnemyState state;
    void Start()
    {
        state = EnemyState.Chasing;
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == EnemyState.Chasing)
        {
            agent.isStopped = false;
            agent.destination = player.position;
            if(Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
            {
                state = EnemyState.Attacking;
            }
        }
        else if(state == EnemyState.Attacking)
        {
            agent.isStopped = true;
            // play attack animation here
            Attack();
            state = EnemyState.Cooldown; 
        }
        // Cooldown after attacks before enemy starts moving/attacking again
        else if(state == EnemyState.Cooldown)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > cooldownLength)
            {
                state = EnemyState.Chasing;
                elapsedTime = 0;
            }
        }
    }

    void Attack()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            PlayerHealthScript playerHealth = hit.transform.GetComponent<PlayerHealthScript>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDmg);
            }
        }
    }
}
