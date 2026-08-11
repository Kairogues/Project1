using UnityEngine;

public class Projectile : Entity
{
    public override void OnSpawn()
    {
        base.OnSpawn();
        // timer = lifeTime;
    }

    private void Update()
    {
        // transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        
        // timer -= Time.deltaTime;
        // if (timer <= 0f)
        // {
        //    ReleaseToPool(); // Tự chết khi hết time
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        ReleaseToPool(); // Tự chết khi chạm mục tiêu
    }
}
