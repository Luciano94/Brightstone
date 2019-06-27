using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour{

    public static DamagePopup Create(Vector3 pos, int dmgAmount, float size, Color color){
        GameObject dmgPopTransform = Instantiate(UIManager.Instance.TextPopupPrefab, 
                                                            pos, Quaternion.identity);

        DamagePopup ret = dmgPopTransform.GetComponent<DamagePopup>();
        ret.Setup(dmgAmount, size, color);

        return ret;
    }

    //[SerializeField]private float speed = 20.0f;
    //[SerializeField]private float dissappearSpeed = 3.0f;
    //[SerializeField]private float decreaseSpeed = 5.0f;
    [SerializeField]private TextMeshPro textMesh;
    [SerializeField]private float lifeTime = 0.5f;
    private Vector3 dir;
    private float initSize = 8.0f;
    private Color textColor;

    public void Setup(int dmgAmount, float size, Color color){
        textMesh.SetText(dmgAmount.ToString());
        textColor = color;
        initSize = size;
        textMesh.color = textColor;
        textMesh.fontSize = initSize;
        transform.position += new Vector3(Random.Range(-0.5f,1f), Random.Range(-0.5f,1f), 0);
    }

    private void Update() {
        lifeTime -= Time.deltaTime;

        if(lifeTime < 0){
            Destroy(gameObject);
        }
    }

}

        /* speed -= decreaseSpeed * Time.deltaTime;
        dir = Vector3.up * speed;
        transform.position += dir * Time.deltaTime;

        textMesh.fontSize -= dissappearSpeed * Time.deltaTime;*/