using UnityEngine;
using UnityEngine.UI;

public class SliderMenu : MonoBehaviour
{
    private Button showHideButton;
    private Animator showHideAnimator;

    private void Start() {
        showHideButton = gameObject.GetComponentInChildren<Button>();
        showHideAnimator = gameObject.GetComponent<Animator>();

        showHideButton.onClick.AddListener(this.ToggleMenu);
    }

    private void ToggleMenu() {
        var isShown = showHideAnimator.GetBool("isShown");
        showHideAnimator.SetBool("isShown", !isShown);
    }
}
