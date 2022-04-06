using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRippleSystem : MonoBehaviour
{
    private List<GameObject> _pooledRipples = new List<GameObject>();
    public GameObject RippleSystemPrefab;

    public GameObject AssignRippleSystem(Transform parentTrans, Vector3 position)
    {
        GameObject rippleSystem = GetPooled();
        if (rippleSystem == null)
        {
            rippleSystem = Instantiate(RippleSystemPrefab, parentTrans).gameObject;
            rippleSystem.transform.position = position;
            _pooledRipples.Add(rippleSystem);
        }
        else
        {
            rippleSystem.transform.position = position;
            rippleSystem.transform.parent = parentTrans;
            rippleSystem.SetActive(true);
        }
        return rippleSystem;
    }
    public void RecycleRippleSystem(GameObject ripple)
    {
        ripple.SetActive(false);
    }
    private GameObject GetPooled()
    {
        if(_pooledRipples.Count == 0)
            return null;
        
        foreach (GameObject pooledRipple in _pooledRipples)
        {
            if (!pooledRipple.activeInHierarchy)
                return pooledRipple;
        }

        return null;
    }
}
