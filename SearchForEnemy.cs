using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForEnemy : MonoBehaviour
{
    
    public GameObject target = null;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.updateAllies.Invoke();
        GameManager.gameManager.updateEnemies.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<UnitAi>().hasTarget)
        {
            GetTarget();
        }
    }

    void GetTarget()
    {
        if (gameObject.tag == "Unit")
        {
            if (GameManager.gameManager.enemiesList.Count > 0)
            {
                target = GameManager.gameManager.enemiesList[0];
                foreach (GameObject enemy in GameManager.gameManager.enemiesList)
                {
                    if ((Vector3.Distance(this.transform.position, enemy.transform.position) < Vector3.Distance(this.transform.position, target.transform.position))
                        && enemy.GetComponent<UnitAttributes>().currentHealth > 0)
                    {
                        target = enemy;

                    }
                    GetComponent<UnitAi>().hasTarget = true;

                }
            }
            else
            {
                GetComponent<UnitAi>().hasTarget = false;
            }
            
        
        }
        if (gameObject.tag == "Enemy")
        {
            if (GameManager.gameManager.alliesList.Count > 0)
            {
                target = GameManager.gameManager.alliesList[0];
                foreach (GameObject ally in GameManager.gameManager.alliesList)
                {
                    if ((Vector3.Distance(this.transform.position, ally.transform.position) < Vector3.Distance(this.transform.position, target.transform.position))
                        && ally.GetComponent<UnitAttributes>().currentHealth > 0)
                    {
                        target = ally;
                        
                    }
                    
                }
                GetComponent<UnitAi>().hasTarget = true;
            }
            else
            {
                GetComponent<UnitAi>().hasTarget = false;
            }

        }

    }
}
