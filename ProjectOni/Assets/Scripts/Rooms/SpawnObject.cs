using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]private GameObject tile; 

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(tile, transform.position, Quaternion.identity).transform.parent = transform;
    }
}
