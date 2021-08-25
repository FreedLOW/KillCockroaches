using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject tarakanPrefab;

    [SerializeField] int countTarakans;

    private void Awake()
    {
        for (int i = 0; i < countTarakans; i++)
        {
            Instantiate(tarakanPrefab, transform.position, transform.rotation, transform);
        }
    }
}