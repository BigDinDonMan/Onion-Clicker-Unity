using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStateLoader : MonoBehaviour
{
    private SavedState savedState;

    private void Awake() {
        savedState = LoadSavedStateData();
        SetUpFromLoadedState();
    }

    private SavedState LoadSavedStateData() {
        return null;
    }

    private void SetUpFromLoadedState() { 
    
    }
}
