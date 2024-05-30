using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyChronoOnLevelLoad : MonoBehaviour
{
    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        Debug.Log(sceneName + sceneBuildIndex);


        // ====================================================== EN FONCTION DES NIVEAUX, DETRUIRE LE CHRONO D'AVANT.
        // ================================================== Tout est rang� dans l'ordre d'apparition des niveaux en jeu.
        if (sceneName == "SCN_CH01_LV02")
        {
            Destroy(GameObject.Find("### PermaUI_CH01_LV01 ###"));    // UI used for chrono
        }
        if (sceneName == "SCN_CH02_LV01")
        {
            Destroy(GameObject.Find("### PermaUI_CH01_LV02 ###"));    // UI used for chrono
        }
        if (sceneName == "SCN_CH02_LV02")
        {
            Destroy(GameObject.Find("### PermaUI_CH02_LV01 ###"));    // UI used for chrono
        }
        if (sceneName == "SCN_CH03_LV01")
        {
            Destroy(GameObject.Find("### PermaUI_CH02_LV02 ###"));    // UI used for chrono
        }
        if (sceneName == "SCN_CH04_LV01")
        {
            Destroy(GameObject.Find("### PermaUI_CH03_LV01 ###"));    // UI used for chrono
        }
        // After the 3rd chapter, the level time will not show because the character is not on stage anymore.
        // So only the global time will be shown for the player to see their progress.
        // ===============================================================================================================
    }
}
