using System.Data;
using System;
using UnityEngine;

public class HitboxComponent : MonoBehaviour
{
    public event Action<HurtboxComponent> HitHurtbox;
    public event Action HitObstacle;

    [SerializeField] private StatComponent statComponent;

    public float GetDamageAmount()
    {
        return statComponent.GetStat(StatType.ATTACK).GetCurrentValue();
    }

    public void RegisterHurtboxHit(HurtboxComponent hurtbox)
    {
        HitHurtbox?.Invoke(hurtbox);
    }

     public void RegisterObstacleHit()
    {
        HitObstacle?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D hurtboxInfo)
    {
        int obstacleLayer = LayerMask.NameToLayer("Obstacle");
        if (hurtboxInfo.gameObject.layer == obstacleLayer)
        {
            RegisterObstacleHit();
            return;
        }

        HurtboxComponent hurtbox = hurtboxInfo.GetComponent<HurtboxComponent>();

        if (hurtbox != null)
        {
            hurtbox.TakeDamge(GetDamageAmount());
            RegisterHurtboxHit(hurtbox);
        }
    }
}

