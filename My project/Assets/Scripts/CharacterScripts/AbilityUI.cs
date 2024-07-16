using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [Header("ability 1")]
    public Image AbilityImage1;

    void Start()
    {
        AbilityImage1.fillAmount = 0;
       
    }

    public void StartCooldown(float cooldownDuration)
    {
        StartCoroutine(CooldownCoroutine(cooldownDuration));
    }

    private IEnumerator CooldownCoroutine(float cooldownDuration)
    {
        float cooldownTimer = cooldownDuration;
        float startTime = Time.time;

        while (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            AbilityImage1.fillAmount = cooldownTimer / cooldownDuration;
            yield return null;
        }

        AbilityImage1.fillAmount = 0f;
    }
}
