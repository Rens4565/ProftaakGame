using UnityEngine;

public class Plot : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        Tower towerToBuild = BuiltManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("you cant afford this tower");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

       
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
}