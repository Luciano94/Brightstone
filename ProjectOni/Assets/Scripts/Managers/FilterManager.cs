﻿using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FilterManager : MonoBehaviour
{
    static private PostProcessVolume postVolume;

    private void Awake() {
        postVolume = gameObject.GetComponent<PostProcessVolume>();
    }


    public static void SetChromaticAberration(bool state){
        ChromaticAberration cA;
        postVolume.profile.TryGetSettings(out cA);
        cA.active = state;
    }
}
