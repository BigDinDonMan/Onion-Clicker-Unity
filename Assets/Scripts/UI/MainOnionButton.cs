using System.Collections.Generic;
using UnityEngine;

public class MainOnionButton : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem buttonParticleSystem; //launch single burst on click
    private List<ParticleSystem> particleSystems;
    private Animator onionAnimator; //reduce size and increase it back on click
    private Camera gameCamera;

    public Texture2D cursorTexture;

    public float rotationSpeed;

    void Awake()
    {
        gameCamera = Camera.main;
        onionAnimator = GetComponent<Animator>();
        buttonParticleSystem.transform.position = this.transform.position;
        particleSystems = new List<ParticleSystem>() { buttonParticleSystem };
        particleSystems.AddRange(buttonParticleSystem.gameObject.GetComponentsInChildren<ParticleSystem>());
    }

    void Update()
    {
        this.transform.Rotate(0f, 0f, Time.deltaTime * rotationSpeed);
    }

    public void PlayShrinkAnimation() {
        onionAnimator.SetTrigger("startShrinking");
        particleSystems.ForEach(ps => ps.Play());
    }

    public void OnMouseEnter() {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
