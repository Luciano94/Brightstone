using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour{

    public static DamagePopup Create(Vector3 pos, int dmgAmount){
        GameObject dmgPopTransform = Instantiate(UIManager.Instance.TextPopupPrefab, 
                                                            pos, Quaternion.identity);

        DamagePopup ret = dmgPopTransform.GetComponent<DamagePopup>();
        ret.Setup(dmgAmount);

        return ret;
    }

    private Color textColor;
    [SerializeField]private TextMeshPro textMesh;
    [SerializeField]private float speed = 20.0f;
    [SerializeField]private float dissappearSpeed = 3.0f;
    private Vector3 dir = Vector3.up;
    [SerializeField]private float lifeTime = 0.5f;

    public void Setup(int dmgAmount){
        textMesh.SetText(dmgAmount.ToString());
        textColor = textMesh.color;
        dir *= speed;
    }

    private void Update() {
        lifeTime -= Time.deltaTime;
        transform.position += dir * Time.deltaTime;
        if(lifeTime < 0){
            textColor.a -= dissappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a<0){
                Destroy(gameObject);
            } 
        }
    }

}
