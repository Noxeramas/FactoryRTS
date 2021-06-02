using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuildingPlacement
{
    VALID,
    INVALID,
    FIXED
};
public class Building
{
    private BuildingPlacement _placement;
    private List<Material> _materials;
    private BuildingData _data;
    private Transform _transform;
    private int _currentHealth;
    private BuildingManager _BuildingManager;


    public Building(BuildingData data)
    {
        _data = data;
        _currentHealth = data.HP;
        
        GameObject g = GameObject.Instantiate(
            Resources.Load($"Prefabs/Buildings/{_data.Code}")
            ) as GameObject;
        _transform = g.transform;
        _BuildingManager = g.GetComponent<BuildingManager>();

        _placement = BuildingPlacement.VALID;
        _materials = new List<Material>();
        foreach (Material material in _transform.Find("Mesh").GetComponent<Renderer>().materials)
        {
            _materials.Add(new Material(material));
        }
        
        SetMaterials();
    }
    public bool HasValidPlacement { get => _placement == BuildingPlacement.VALID; }
    public bool isFixed { get => _placement == BuildingPlacement.FIXED; }

    public void SetMaterials() { SetMaterials(_placement); }
    public void SetMaterials(BuildingPlacement placement)
    {

        Material refMaterial;
        



        switch (placement)
        {
            case BuildingPlacement.VALID:
                refMaterial = Resources.Load("Materials/Valid") as Material;
                break;
            case BuildingPlacement.INVALID:
                refMaterial = Resources.Load("Materials/Invalid") as Material;
                break;
            case BuildingPlacement.FIXED:
            default:
                refMaterial = Resources.Load("Materials/solid") as Material;
                break;




        }
        _transform.Find("Mesh").GetComponent<Renderer>().material = refMaterial;
        /*
        List<Material> materials;
        if (placement == BuildingPlacement.VALID)
        {
            Material refMaterial = Resources.Load("Materials/Valid") as Material;
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++) materials.Add(refMaterial);
        }
        else if (placement == BuildingPlacement.INVALID)
        {
            Material refMaterial = Resources.Load("Materials/Invalid") as Material;
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++) materials.Add(refMaterial);
        }
        else if (placement == BuildingPlacement.FIXED) materials = _materials;
        else return;
        _transform.Find("Mesh").GetComponent<Renderer>().materials = materials.ToArray();
        */
    }
    


    public void Place()
    {
        
        _placement = BuildingPlacement.FIXED;
        SetMaterials();
        _transform.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void CheckValidPlacement()
    {
        if (_placement == BuildingPlacement.FIXED) return;
        _placement = _BuildingManager.CheckPlacement()
            ? BuildingPlacement.VALID
            : BuildingPlacement.INVALID;

    }

    public bool IsFixed { get => _placement == BuildingPlacement.FIXED; }

    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public string Code { get => _data.Code; }
    public Transform Transform { get => _transform; }
    public int HP { get => _currentHealth; set => _currentHealth = value; }
    public int MaxHP { get => _data.HP; }
    public int DataIndex
    {
        get
        {
            for(int i = 0; i < Globals.BUILDING_DATA.Length; i++)
            {
                if(Globals.BUILDING_DATA[i].Code == _data.Code)
                {
                    return i;
                }
                
            }
            return -1;
        }
    }
}
