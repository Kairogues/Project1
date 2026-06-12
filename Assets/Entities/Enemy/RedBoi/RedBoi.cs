using UnityEngine;

public class RedBoi : MonoBehaviour
{
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private LifeComponent lifeComponent;
    
    private void Start()
    {
        lifeComponent.Died += Die;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
