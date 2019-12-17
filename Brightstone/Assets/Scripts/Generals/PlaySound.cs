using UnityEngine;

public class PlaySound : MonoBehaviour{
    public void Play(){
        SoundManager.Instance.MenuItemClick();
    }
}
