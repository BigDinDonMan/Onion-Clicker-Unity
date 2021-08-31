using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainOnionButton : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem buttonParticleSystem; //launch single burst on click
    private Animator onionAnimator; //reduce size and increase it back on click
    private Camera gameCamera;

    public float rotationSpeed;

    void Awake()
    {
        gameCamera = Camera.main;
        onionAnimator = GetComponent<Animator>();
        buttonParticleSystem.transform.position = this.transform.position;
    }

    void Update()
    {
        this.transform.Rotate(0f, 0f, Time.deltaTime * rotationSpeed);
    }

    public void PlayShrinkAnimation() {
        onionAnimator.SetTrigger("startShrinking");
        buttonParticleSystem.Play();
    }
}
