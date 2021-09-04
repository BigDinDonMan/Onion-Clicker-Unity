using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlidingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    public float disappearTimer;
    public float currentTimer;
    public float movementSpeed;
    public float lerpCoef = 0.01f;
    public float eps = 0.0001f;

    private void Awake() {
        currentTimer = disappearTimer;
        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
    }

    void Update()
    {
        if (currentTimer > 0f) {
            currentTimer -= Time.deltaTime;
        } else {
            text.alpha = Mathf.Lerp(text.alpha, 0f, lerpCoef);
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, 0f, lerpCoef));
            if (text.alpha <= eps && image.color.a <= eps) {
                Destroy(this.gameObject);
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed * Time.deltaTime, transform.position.z);
    }
}
