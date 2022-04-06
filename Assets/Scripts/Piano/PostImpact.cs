using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostImpact : MonoBehaviour
{
    [Range(0f, 10f)]
    public float DivergeSpeed = 3f;
    [Range(0f, 10f)]
    public float ConvergeSpeed = 3f;
    [Range(0f, 100f)]
    public float ImpactAreaRadius = 10f;
    [Range(0f, 10f)]
    public float StillTime = 2f;
    public SphereCollider ImpactCollider;

    private float _originRadius;
    private IEnumerator coroutine;

    private void Start()
    {
        _originRadius = ImpactCollider.radius;
    }
    public void TriggerImpact(float stillTime)
    {
        StillTime = stillTime;
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = Impact();
        StartCoroutine(coroutine);
    }
    IEnumerator Impact()
    {
        //Converge
        for (float i = ImpactCollider.radius; i < ImpactAreaRadius; i += DivergeSpeed * Time.deltaTime)
        {
            ImpactCollider.radius = i;
            yield return null;
        }
        yield return new WaitForSeconds(StillTime); //Still
        //Diverge
        for (float i = ImpactCollider.radius; i > _originRadius; i -= ConvergeSpeed * Time.deltaTime)
        {
            ImpactCollider.radius = i;
            yield return null;
        }
    }
}
