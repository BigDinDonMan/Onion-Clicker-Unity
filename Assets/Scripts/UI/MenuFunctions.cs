using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
    public void Exit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(exitCode: 0);
#endif
    }
}
