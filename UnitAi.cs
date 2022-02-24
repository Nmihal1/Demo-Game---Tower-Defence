using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAi : MonoBehaviour
{
    public Enums.UnitStates unitState;
    NavMeshAgent agent;
    Animator anim;
    public bool hasTarget = false;
    public bool reachedTarget = false;
    bool isAttacking = false;
    public GameObject FX; //Use if intend to use on a spellcaster ultimate, otherwise leave blank.
    public Transform FXorigin;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        unitState = Enums.UnitStates.Idle;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (unitState)
        {
            case Enums.UnitStates.Idle:
                if(GameManager.gameManager.gameState == Enums.GameStates.Fight)
                {
                    unitState = Enums.UnitStates.SearchForEnemy;
                }
                break;
            case Enums.UnitStates.SearchForEnemy:
                if (hasTarget )
                {
                    GotoTarget();
                }
                break;
            case Enums.UnitStates.ReachedEnemy:
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
                
                break;
            case Enums.UnitStates.Dead:
                StartCoroutine(Die());
                break;
        }
    }

    void GotoTarget()
    {
        if(Vector3.Distance(transform.position, GetComponent<SearchForEnemy>().target.transform.position) > GetComponent<UnitAttributes>().attackRange)
        {
            agent.isStopped = false;
            anim.SetBool("Walk", true);
            agent.SetDestination(GetComponent<SearchForEnemy>().target.transform.position);

        }
        else
        {
            agent.isStopped = true;
            anim.SetBool("Walk", false);
            unitState = Enums.UnitStates.ReachedEnemy;
        }

    }

    IEnumerator Attack()
    {
        if(GetComponent<SearchForEnemy>().target)
        {
                isAttacking = true;
                transform.LookAt(GetComponent<SearchForEnemy>().target.transform.position);
            if (GetComponent<UnitAttributes>().currentMana >= GetComponent<UnitAttributes>().maxMana && GetComponent<UnitAttributes>().maxMana > 0)
            {
                GetComponent<SkillCast>().CastSkill();
                yield return new WaitForSeconds(GetComponent<SkillCast>().skillDuration);
            }
            else
            {
                anim.SetTrigger("Attack");
                yield return new WaitForSeconds(.5f);
                if (GetComponent<UnitAttributes>().maxMana > 0)
                {
                GetComponent<UnitAttributes>().currentMana += GetComponent<UnitAttributes>().maxMana / 5;
                }
                switch (GetComponent<UnitAttributes>().UnitType)
                {
                    case Enums.AttackType.Melee:
                        DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage);
                    break;

                    case Enums.AttackType.Ranged:
                        GetComponent<SkillCast>().SpellCast(FX, FXorigin);
                    break;
                }
                yield return new WaitForSeconds(GetComponent<UnitAttributes>().attackCooldown - .5f);
            }
        }

        isAttacking = false;
        hasTarget = false;
        unitState = Enums.UnitStates.SearchForEnemy;
    }

    IEnumerator Die()
    {
        
         if (gameObject.tag == "Unit")
        {
            GameManager.gameManager.alliesList.Remove(this.gameObject);
        }
            

        if (gameObject.tag == "Enemy")
            GameManager.gameManager.enemiesList.Remove(this.gameObject);
        
        agent.isStopped = true;
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
        
    }
}
