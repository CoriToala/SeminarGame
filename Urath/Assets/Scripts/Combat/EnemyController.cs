using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private Unit stats;
    [SerializeField] private BoxCollider2D bodyCollider;
    [SerializeField] BattleManager battleHandler;
    [SerializeField] private CircleCollider2D attackRadius;
    private Transform target;
    [SerializeField] private float speed = 5f;
    private bool hitTarget = false;
    

    void Start()
    {
        stats = GetComponent<Unit>();
        Collider2D bodyCollider = GetComponent<Collider2D>();
        //Spawn in at a preselected point

        battleHandler = Object.FindAnyObjectByType<BattleManager>();
    }

    public void enemyDied()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(target != null && !hitTarget){
            float distance = Vector2.Distance(transform.position, target.position);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("player entered trigger colider");
        if(other.gameObject.CompareTag("Player"))
        { 
            target = other.gameObject.GetComponent<Transform>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(target != null)
        {
            target = null;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        //Debug.Log("enemy hit an object");
        if(other.gameObject.CompareTag("Player"))
        { 
           // Debug.Log("Detected PLayer");
            battleHandler.StartBattle(stats);
            hitTarget = true;
        }
    }
}
