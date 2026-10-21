using UnityEngine;

public class CastleBlock : MonoBehaviour
{
    public float fallThreshold = -10f;

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Destroy(gameObject);
        }
    }
}