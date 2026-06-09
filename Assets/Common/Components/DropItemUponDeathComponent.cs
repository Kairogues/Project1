using UnityEngine;
using System.Collections.Generic;

public class DropItemUponDeathComponent : MonoBehaviour
{
    private const float DROP_DISTANCE = 1.0F;
    [SerializeField] private List<GameObject> itemsToDrop;
    [SerializeField] private LifeComponent lifeComponent;

    private void Start()
    {
        lifeComponent.Died += DropItems;
    }

    private void DropItems()
    {
        foreach (GameObject item in itemsToDrop)
        {
            float randomDisplacementX = UnityEngine.Random.Range(-DROP_DISTANCE, DROP_DISTANCE);
            float randomDisplacementY = UnityEngine.Random.Range(-DROP_DISTANCE, DROP_DISTANCE);

            Vector3 spawnPosition = new Vector3(
                transform.position.x + randomDisplacementX, 
                transform.position.y + randomDisplacementY, 
                0f
            );

            Instantiate(item, spawnPosition, transform.rotation);
        }
    }
}
