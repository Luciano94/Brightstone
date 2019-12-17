using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowEffect : MonoBehaviour
{
    [SerializeField]private Vector3 offsetPos = new Vector3(-0.1f,-0.1f);
    [SerializeField]private Quaternion offsetRot = Quaternion.identity;
    [SerializeField]private Vector3 defaultScale = new Vector3(1,2,1);
    [SerializeField]private Material shadowMaterial;

    private GameObject shadow;
    private SpriteRenderer _renderer;
    public Sprite shadowSprite;
    private SpriteRenderer shadowRenderer;


    private void Start() {
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;
        shadow.layer = gameObject.layer;
        shadow.transform.localScale = defaultScale;

        shadow.transform.position = offsetPos;
        shadow.transform.rotation = offsetRot;

        _renderer = GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.AddComponent<SpriteRenderer>();
        if(shadowSprite != null){
            shadowRenderer.sprite = shadowSprite;
        }else{
            shadowRenderer.sprite = _renderer.sprite;
        }
        shadowRenderer.material = shadowMaterial;

        shadowRenderer.sortingLayerName = _renderer.sortingLayerName;
        shadowRenderer.sortingOrder = _renderer.sortingOrder - 1;
    }

    private void LateUpdate() {
        if(shadow != null){
            if(shadowSprite != null){
                shadowRenderer.sprite = shadowSprite;
            }else{
                shadowRenderer.sprite = _renderer.sprite;
            }
            shadow.transform.position = shadow.transform.parent.position + offsetPos;
            shadowRenderer.sortingOrder = _renderer.sortingOrder - 1;
        }else{
            Debug.Log("algo no anda bien");
        }
    }

    public void onDeath(){
        //shadowRenderer.color = Color.clear; 
    }
}
