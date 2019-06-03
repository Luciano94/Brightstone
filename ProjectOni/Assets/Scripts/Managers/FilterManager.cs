using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FilterManager : MonoBehaviour
{
    static private PostProcessVolume postVolume;
    static Vignette cV;
    static float minSmoothValue;

    private void Awake() {
        postVolume = gameObject.GetComponent<PostProcessVolume>();
        postVolume.profile.TryGetSettings(out cV);

        minSmoothValue = cV.smoothness.value;
    }


    public static void SetChromaticAberration(bool state){
        ChromaticAberration cA;
        postVolume.profile.TryGetSettings(out cA);
        cA.active = state;
    }

    public static void SetColorSaturation(int value){
        ColorGrading cG;
        postVolume.profile.TryGetSettings(out cG);
        cG.saturation.value = value;
    }

    public static void SetActiveVignette(bool state){
        cV.enabled.value = state;
    }

    public static void SetVignetteSmoothness(float value){
        cV.smoothness.value = minSmoothValue + value;
    }
}
