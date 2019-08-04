using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowEffect : MonoBehaviour
{
    [SerializeField]private Vector3 offsetPos = new Vector3(-0.1f,-0.1f);
    [SerializeField]private Quaternion offsetRot = Quaternion.identity;
    [SerializeField]private Material shadowMaterial;

    private GameObject shadow;
    private SpriteRenderer _renderer;
    private SpriteRenderer shadowRenderer;


    private void Start() {
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;
        shadow.layer = gameObject.layer;

        shadow.transform.position = offsetPos;
        shadow.transform.rotation = offsetRot;

        _renderer = GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.AddComponent<SpriteRenderer>();
        shadowRenderer.sprite = _renderer.sprite;
        shadowRenderer.material = shadowMaterial;

        shadowRenderer.sortingLayerName = _renderer.sortingLayerName;
        shadowRenderer.sortingOrder = _renderer.sortingOrder - 1;
    }

    private void LateUpdate() {
        if(shadow != null){
            shadowRenderer.sprite = _renderer.sprite;
            shadow.transform.position = shadow.transform.parent.position + offsetPos;
        }else{
            Debug.Log("algo no anda bien");
        }
    }
}
