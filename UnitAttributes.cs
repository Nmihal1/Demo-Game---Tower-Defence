using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAttributes : MonoBehaviour
{
    public string UnitName;
    public Enums.AttackType UnitType;
    private Transform camera;
    private Transform canvas;
    public Image healthBar;
    public Image manaBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 10f;
    public float maxMana = 100f;
    public float currentMana = 0f;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;
    private void Awake()
    {
        /*canvas = transform.Find("Canvas");
        healthBar = canvas.transform.Find("HealthBar").GetComponent<Image>();
        manaBar = canvas.transform.Find("ManaBar").GetComponent<Image>();*/
        currentHealth = maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        
        
    }
    private void LateUpdate()
    {
        canvas.transform.LookAt(camera);

        healthBar.fillAmount = currentHealth / maxHealth;
        manaBar.fillAmount = currentMana / maxMana;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            GetComponent<UnitAi>().unitState = Enums.UnitStates.Dead;
        }


    }
}
