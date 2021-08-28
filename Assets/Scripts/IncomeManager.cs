using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class IncomeManager : MonoBehaviour
{
    public RectTransform detailsParent;

    public TextMeshProUGUI currentOnionsText;
    public TextMeshProUGUI onionsPerSecondText;
    public List<GeneratorDetails> generatorDetailsList;

    // Start is called before the first frame update
    void Start()
    {
        generatorDetailsList.AddRange(detailsParent.GetComponentsInChildren<GeneratorDetails>());
        PlayerDetails.instance.OnOnionsChanged += this.UpdateDisplay;
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        var totalFrameIncome = generatorDetailsList.Sum(detail => detail.generatorAmount * detail.incomePerGenerator * Time.deltaTime) * PlayerDetails.instance.GlobalIncomeMultiplier;
        PlayerDetails.instance.ChangeOnions(totalFrameIncome);
        generatorDetailsList.ForEach(detail => detail.UpdateButtonsEnabled());
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)) {
            PlayerDetails.instance.ChangeOnions(100);
            generatorDetailsList.ForEach(detail => detail.UpdateButtonsEnabled());
        }
#endif
    }

    public void UpdateDisplay() {
        var currentOnionsStr = $"{PlayerDetails.instance.Onions:0} onions";
        var onionsPerSecondStr = $"{generatorDetailsList.Sum(genDetails => genDetails.generatorAmount * genDetails.incomePerGenerator):0.0} onions per second";
        onionsPerSecondText.text = onionsPerSecondStr;
        currentOnionsText.text = currentOnionsStr;
    }
}
