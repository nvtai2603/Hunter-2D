using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {
            Vector3 newPos = player.position;
            newPos.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed);
        }
    }
}
