using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Holder")]
    [SerializeField] private Transform _prefabs_holder;
    [SerializeField] private Transform _holder;

    [Header("Prefabs")]
    [SerializeField] private List<Transform> _list_prefabs;

    [Header("Pool")]
    [SerializeField] private List<Transform> _pool;

    private void Start()
    {
        GetAllPrefabs();
        HideAllPrefabs();
    }
    private void GetAllPrefabs()
    {
        foreach(Transform prefab in _prefabs_holder.transform)
        {
            _list_prefabs.Add(prefab);
        }
    }

    private void HideAllPrefabs()
    {
        foreach(Transform prefab in _list_prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    
    public void Spawn(string obj_name, CardColor color, CardType type)
    {
        Transform transform = GetObjectFromPool(obj_name);
        if(transform == null)
        {
            Debug.Log("Can not spawn object");
            return;
        }

    }

    private Transform GetObjectFromPool(string obj_name)
    {
        foreach(Transform obj in _pool)
        {
            if(obj.name == obj_name)
            {
                return obj;
            }
        }
        return GenerateNewObj(obj_name);  
    }
    private Transform GenerateNewObj(string obj_name)
    {
        foreach(Transform prefab in _list_prefabs)
        {
            if(prefab.name == obj_name)
            {
                return prefab;
            }
        }
        return null;
    }
    private void ConfigCard(CardColor color, CardType type)
    {

    }
}
