using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCast : MonoBehaviour
{
    public float skillDuration = 1f;
    public float Amplifier = 10f;

    public GameObject UltimateSpellPrefab; //Use if intend to use on a spellcaster ultimate, otherwise leave blank.

    public void SpellCast(GameObject prefab, Transform origin) {
        GameObject spellClone = (GameObject)Instantiate(prefab, origin.position, Quaternion.identity);
    }

    public void CastSkill()
    {
        StartCoroutine(CastSkillCoroutine());
    }

    IEnumerator CastSkillCoroutine()
    {
        switch (GetComponent<UnitAttributes>().UnitName)
        {
            case "Crusader Knight":
                GetComponent<Animator>().SetTrigger("Skill");
                yield return new WaitForSeconds(skillDuration);
                GetComponent<UnitAttributes>().currentMana = 0;
                DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage * Amplifier);
            break;

            case "Demon":
                GetComponent<Animator>().SetTrigger("Skill");
                yield return new WaitForSeconds(skillDuration);
                GetComponent<UnitAttributes>().currentMana = 0;
                //DoDamage.SpellDamage(this.transform, SpellPrefab);
                DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage * Amplifier);
            break;

            case "Heimrall":
                GetComponent<Animator>().SetTrigger("Skill");
                yield return new WaitForSeconds(skillDuration);
                GetComponent<UnitAttributes>().currentMana = 0;
                DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage * Amplifier);
            break;

            case "OlgBolg":
                GetComponent<Animator>().SetTrigger("Skill");
                yield return new WaitForSeconds(skillDuration);
                GetComponent<UnitAttributes>().currentMana = 0;
                DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage * Amplifier);
            break;

            case "Medusa":
                GetComponent<Animator>().SetTrigger("Skill");
                yield return new WaitForSeconds(skillDuration);
                GetComponent<UnitAttributes>().currentMana = 0;
                DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage * Amplifier);
            break;

            case "Dragon":

                GetComponent<Animator>().SetTrigger("Skill");
                yield return new WaitForSeconds(skillDuration);
                GetComponent<UnitAttributes>().currentMana = 0;
                DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage * Amplifier);
                break;

            case "Minotaur":

                GetComponent<Animator>().SetTrigger("Skill");
                yield return new WaitForSeconds(skillDuration);
                GetComponent<UnitAttributes>().currentMana = 0;
                DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage * Amplifier);
                break;

            case "Mage":

                GetComponent<Animator>().SetTrigger("Skill");
                yield return new WaitForSeconds(skillDuration);
                GetComponent<UnitAttributes>().currentMana = 0;
                DoDamage.Damage(GetComponent<SearchForEnemy>().target, GetComponent<UnitAttributes>().damage * Amplifier);
                break;
        }
    }
}
