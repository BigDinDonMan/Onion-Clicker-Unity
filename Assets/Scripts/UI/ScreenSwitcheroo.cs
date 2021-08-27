using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSwitcheroo : MonoBehaviour
{
    public ToggleButton upgradesButton;
    public ToggleButton sourcesButton;
    public GameObject upgradesScreen;
    public GameObject sourcesScreen;
    public GameObject detailsParent;
    public List<GeneratorDetails> generatorDetailsList;

    private void Awake() {
        generatorDetailsList.AddRange(detailsParent.GetComponentsInChildren<GeneratorDetails>());
    }

    void Start()
    {
        upgradesScreen.SetActive(false);
        sourcesScreen.SetActive(true);
        sourcesButton.Selected = true;
        upgradesButton.Selected = false;

        upgradesButton.button.onClick.AddListener(() => {
            sourcesButton.Selected = false;
            upgradesScreen.SetActive(true);
            sourcesScreen.SetActive(false);
        });
        sourcesButton.button.onClick.AddListener(() => {
            upgradesButton.Selected = false;
            upgradesScreen.SetActive(false);
            sourcesScreen.SetActive(true);
            generatorDetailsList.ForEach(detail => detail.UpdateButtonsEnabled());
        });
    }
}
