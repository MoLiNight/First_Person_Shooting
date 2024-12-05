using UnityEngine;

public class PlayerColliderFollow : MonoBehaviour
{
    public GameObject PlayerFakeCollider;

    // Update is called once per frame
    void Update()
    {
        PlayerFakeCollider.transform.position = transform.position + 0.5f * Vector3.down;
    }
}
