using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject interfaceInventory;

    public List<Equipment> Equipments { get; private set; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            interfaceInventory.SetActive(!interfaceInventory.activeSelf);
        }
    }

    public void AddEquipment(Equipment equipment)
    {
        Equipments.Add(equipment);
    }

    public void DeleteEquipment(Equipment equipment)
    {
        Equipments.Remove(equipment);
    }
}
