using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
    }

    void Update()
    {
        if (Physics.Raycast(player.transform.position, -Vector3.up, out RaycastHit raycastHit, 1))
        {
            if (raycastHit.collider.GetComponent<Character>())
            {
                Character character = raycastHit.collider.GetComponent<Character>();
                player.GetComponent<Character>().SetIntoBattle(true);
                character.SetIntoBattle(true);
                BattleManager.Init();
            }
        }
    }
}
