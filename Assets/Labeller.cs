using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Labeller : MonoBehaviour
{
    TextMeshPro label;
    public Vector2Int cords = new Vector2Int();
    GridManager gridManager;

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();
        label.enabled = false;
        DisplayCords();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            label.enabled = true;
        }

        DisplayCords();
        transform.name = cords.ToString();

        ToggleLables();
        SetLabelColor();
    }

    void SetLabelColor()
    {
        if (gridManager == null) 
        { 
            return; 
        }

        Node node = gridManager.GetNode(cords);

        if (node == null) 
        { 
            return; 
        }

        if (!node.walkable)
        {
            label.color = blockedColor;
        }
        else if (node.path)
        {
            label.color = pathColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    private void DisplayCords()
    {
        if (!gridManager) 
        { 
            return; 
        }
        cords.x = Mathf.RoundToInt(transform.position.x / Constante.UNITY_GRID_SIZE);
        cords.y = Mathf.RoundToInt(transform.position.z / Constante.UNITY_GRID_SIZE);
        label.text = $"{cords.x};{cords.y}";
    }

    void ToggleLables()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
}