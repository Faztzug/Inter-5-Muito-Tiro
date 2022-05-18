using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealth : Health
{
    [SerializeField] private Slider bar;
    [SerializeField] private PostProcessVolume damageEffect;
    private float damageTime = 0;
    [SerializeField] float effectTimeMultplier = 10;
    [SerializeField] float effectGainMultplier = 2f;
    [SerializeField] float effectDownMultplier = 0.5f;
    private GameState state;
    public bool dead;
    private Animator anim;
    

    public override void Start()
    {
        base.Start();
        bar.maxValue = maxHealth;
        UpdateHealth();
        state = GetComponent<GameState>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(state.GodMode) health = maxHealth;

        if(damageEffect.weight > 0)
        {
            if(damageTime < 0)
            {
                damageEffect.weight -= 1f * Time.deltaTime * effectDownMultplier;
                damageTime = 0;
            }
            else
            {
                damageTime -= 1f * Time.deltaTime;
            }
        }

        if(state.playerDead) damageEffect.weight += Time.deltaTime;
        
    }

    public override void UpdateHealth(float value = 0, Item item = null)
    {
        if(health < maxHealth && item != null) item.DestroyItem();
         
        base.UpdateHealth(value);
        bar.value = health;

        var hpPorcentage = Mathf.Abs(health / maxHealth);
        var chgPorcentage = Mathf.Abs(value / maxHealth);

        if(value < 0)
        {
            damageEffect.weight += chgPorcentage * effectGainMultplier;
            damageTime += chgPorcentage * effectTimeMultplier;
            anim.SetTrigger("Damage");
        }
        else if(value > 0)
        {
            damageEffect.weight -= chgPorcentage * effectGainMultplier;
            damageTime -= chgPorcentage * effectTimeMultplier;
        }
    }

    public override void DestroyCharacter()
    {
        if(state.playerDead) return;
        anim.SetTrigger("Die");
        state.playerDead = true;
        foreach (var item in GetComponentsInChildren<Collider>())
        {
            item.enabled = false;
        }
        foreach (var item in GetComponentsInChildren<MonoBehaviour>())
        {
            if(item == this) continue;
            item.enabled = false;
        }
        //this.gameObject.SetActive(false);
    }
}
