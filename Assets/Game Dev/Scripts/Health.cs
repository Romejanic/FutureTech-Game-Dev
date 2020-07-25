using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Game Dev Workshop/Health")]
public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public EventTrigger.TriggerEvent onDieEvent;
    public EventTrigger.TriggerEvent onHurtEvent;
    public EventTrigger.TriggerEvent onHealEvent;

    private float health;
    private bool dead = false;

    void Start()
    {
        this.health = this.maxHealth;
    }

    public void Hurt(float damage)
    {
        if (this.dead)
            return;
        if(damage > 0f) {
            this.onHurtEvent.Invoke(new BaseEventData(EventSystem.current));
        }
        this.health -= damage;
        if(this.health < 0f) {
            this.health = 0f;
            this.Die();
        }
    }

    public void Heal(float amount)
    {
        this.Hurt(-amount);
        this.onHealEvent.Invoke(new BaseEventData(EventSystem.current));
    }

    public void Die()
    {
        this.dead = true;
        this.onDieEvent.Invoke(new BaseEventData(EventSystem.current));
    }

    public float GetCurrentHealth()
    {
        return this.health;
    }

    public bool IsDead()
    {
        return this.dead;
    }
        
}
