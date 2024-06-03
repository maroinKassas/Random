using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : TacticsMove
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            FindSelectableTiles();
            OnMouseOver();
        }
        else
        {
            Move();
        }
    }

    public void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.tag == "Tile")
            {
                Tile tile = raycastHit.collider.GetComponent<Tile>();

                if (tile.selectable)
                {
                    DisplayPath(tile);
                    if (Input.GetMouseButtonUp(0))
                    {
                        MoveToTile(tile);
                    }
                }
            }
        }
    }
}
