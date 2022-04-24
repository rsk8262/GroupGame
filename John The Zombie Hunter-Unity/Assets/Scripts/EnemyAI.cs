using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyAI: MonoBehaviour
{
    public float attackDistance = 2f;
    public float movementSpeed = .01f;
    public int npcHP = 100;
    public int npcDamage = 5;
    public float destoryEnemyAfter = 10;
    [HideInInspector]
    public Transform playerTransform;
    [SerializeField] private Animator m_animator = null;

    private NavMeshAgent agent;
    public EnemySpawner spawner;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        agent.speed = movementSpeed;

        //Set Rigidbody to Kinematic to prevent hit register bug
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (npcHP == 0) return;

        m_animator.SetBool("Attack", false);

        agent.destination = playerTransform.position;
        Vector3 relativePos = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);

        if (Vector3.Distance(playerTransform.position, transform.root.position) > attackDistance)
            m_animator.SetFloat("MoveSpeed", movementSpeed * 1.5f);
        else
            m_animator.SetBool("Attack", true);
    }

    void EndAttack()
    {
        playerTransform.gameObject.GetComponent<PlayerManager>().ApplyDamage(npcDamage);
    }

    public void ApplyDamage(int points)
    {
        npcHP -= points;
        healthBar.SetHealth(npcHP);

        if (npcHP <= 0)
        {
            healthBar.Disable();
            m_animator.SetBool("Dead", true);
            agent.isStopped = true;
            npcHP = 0;
            Destroy(transform.root.gameObject, destoryEnemyAfter);
            transform.root.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    private void OnDestroy()
    {
        spawner.UpdateCounter();
    }
}