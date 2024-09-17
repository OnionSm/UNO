using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    public Transform Spawn(string obj_name)
    {
        Transform transform = GetObjectFromPool(obj_name);
        if(transform == null)
        {
            Debug.Log("Can not spawn object");
            return null;
        }
        return transform;
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
    private void ConfigCard(Transform card, CardColor color, CardType type, Sprite card_image)
    {
        BaseCard basecard = card.GetComponent<BaseCard>();

        if (basecard != null)
        {
            basecard.Color = color;
            basecard.Type = type;
        }

        Image image = card.GetComponent<Image>();
        if(image != null)
        {
            image.sprite = card_image;
        }
    }
}
