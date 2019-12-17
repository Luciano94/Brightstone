using UnityEngine;
using UnityEngine.UI;

public class VersionName : MonoBehaviour{
    private void Awake(){
        GetComponent<Text>().text = "v" + Application.version;
    }
}
