using UnityEngine;

public class PassiveRotator : MonoBehaviour
{
    public Vector3 rotationVector;

    void Update() => this.transform.Rotate(rotationVector * Time.deltaTime);
}
