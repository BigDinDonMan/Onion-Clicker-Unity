using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainOnionButton : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem buttonParticleSystem; //launch single burst on click
    private Animator onionAnimator; //reduce size and increase it back on click

    public float rotationSpeed;

    void Awake()
    {
        onionAnimator = GetComponent<Animator>();
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
