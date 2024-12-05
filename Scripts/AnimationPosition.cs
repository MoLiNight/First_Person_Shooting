using UnityEngine;

public class AnimationPosition : MonoBehaviour
{
    [HideInInspector] public Vector3 positon;
    private Vector3 startPosition;

    void Start()
    {
        this.startPosition = this.transform.position;
    }

    void Update()
    {
        Vector3 newPosition = this.startPosition + this.positon;
        if (newPosition != this.startPosition)
            this.transform.position = newPosition;
    }
}
