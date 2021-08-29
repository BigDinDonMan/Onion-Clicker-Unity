using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSwitcheroo : MonoBehaviour
{
    public Button upgradesButton;
    public Button sourcesButton;
    public Image upgradesButtonImage;
    public Image sourcesButtonImage;
    public GameObject upgradesScreen;
    public GameObject sourcesScreen;
    public GameObject detailsParent;
    public List<GeneratorDetails> generatorDetailsList;

    private Color idleColor;

    private void Awake() {
        generatorDetailsList.AddRange(detailsParent.GetComponentsInChildren<GeneratorDetails>());
        idleColor = upgradesButton.colors.normalColor;
    }

    void Start()
    {
        upgradesButton.onClick.AddListener(() => {
            Switch(false);
            SwitchColors(sourcesButton, sourcesButtonImage, false);
            SwitchColors(upgradesButton, upgradesButtonImage, !false);
        });
        sourcesButton.onClick.AddListener(() => {
            Switch(true);
            SwitchColors(sourcesButton, sourcesButtonImage, true);
            SwitchColors(upgradesButton, upgradesButtonImage, false);
            generatorDetailsList.ForEach(d => d.UpdateButtonsEnabled());
        });
        sourcesButton.onClick.Invoke();
    }

    private void Switch(bool b) {
        upgradesScreen.SetActive(!b);
        sourcesScreen.SetActive(b);
    }

    private void SwitchColors(Button b, Image i, bool clicked) {
        var colors = b.colors;
        colors.normalColor = clicked ? colors.pressedColor : idleColor;
        b.colors = colors;
        i.color = colors.normalColor;
    }
}
