using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{

    [Header("ability 1")]
    public Image AbilityImage1;
    public KeyCode abilityKey;
    public float abilityCD = 5;

    private bool isAb1CD = false;
    private float currentAbilityCD;
    // Start is called before the first frame update
    void Start()
    {
        AbilityImage1.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        abilityinput();

        AbilityCooldown(ref currentAbilityCD,abilityCD, ref isAb1CD, AbilityImage1 );
        
    }

    private void abilityinput(){
        if (Input.GetKeyDown(abilityKey) && !isAb1CD){
            isAb1CD = true;
            currentAbilityCD = abilityCD;
        }
    }


    private void AbilityCooldown(ref float currentCooldown, float maxCD, ref bool isCooldown, Image skillImage){

        if(isCooldown){
            currentCooldown -= Time.deltaTime;
            if(currentCooldown <= 0f){
                isCooldown = false;
                currentCooldown = 0f;

                if(skillImage != null){
                    skillImage.fillAmount = 0f;

                }
            }else{
                if(skillImage != null){
                    skillImage.fillAmount = currentCooldown/maxCD;
                }
            }
        }
    }
}
