using UnityEngine;
using UnityEditor;
using System.Security.Cryptography;
using Unity.VisualScripting;
using System.Text;

public class MenuScript
{
    [MenuItem("Tools/Assign Tile Material")]
    public static void AssignTileMaterial()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("TileMat");

        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<Renderer>().material = material;
        }
    }

    [MenuItem("Tools/Assign Tile Script")]
    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            tile.AddComponent<Tile>();
        }
    }

    [MenuItem("Tools/Assign Rigidbody Script")]
    public static void AssignRigidbodyScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            tile.AddComponent<Rigidbody>();
            Rigidbody rigidbody = tile.GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.angularDrag = 0;
            rigidbody.useGravity = false;
        }
    }

    [MenuItem("Tools/Delete Rigidbody Script")]
    public static void DeleteRigidbodyScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            Rigidbody rigidbody = tile.GetComponent<Rigidbody>();
            Object.DestroyImmediate(rigidbody);
        }
    }

    [MenuItem("Tools/List GameObjects and Scripts")]
    static void ListGameObjectsAndScripts()
    {
        StringBuilder sb = new StringBuilder();
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            sb.AppendLine($"GameObject: {obj.name}");

            foreach (var component in obj.GetComponents<MonoBehaviour>())
            {
                sb.AppendLine($"    Script: {component.GetType().Name}");
            }
        }

        string path = "Assets/GameObjectScriptList.txt";
        System.IO.File.WriteAllText(path, sb.ToString());
        Debug.Log($"List saved to {path}");
    }
}
