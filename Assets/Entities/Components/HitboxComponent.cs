using System.Data;
using UnityEngine;

public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private StatsComponent statsComponent;
    [SerializeField] private float timeAlive = 5.0f;
    private float damageAmount = 0.0f;

    private void Awake()
    {
        SetDamageAmount(statsComponent.GetStats(StatType.ATTACK).GetCurrentValue());
    }

    private void Update()
    {
        timeAlive -= Time.deltaTime;

        if (timeAlive <= 0.0f) {
            SelfDestruct();
        }

    }

    public void SetDamageAmount(float amount)
    {
        damageAmount = amount;
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        HurtboxComponent hurtbox = hitInfo.GetComponent<HurtboxComponent>();

        if (hurtbox != null)
        {
            hurtbox.TakeDamge(damageAmount);
        }

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
