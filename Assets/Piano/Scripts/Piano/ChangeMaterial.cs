using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    //public Material OriginMaterial;
    //public Material HintMaterial;
    //public Material PressedMaterial;
    public Color OriginColor;
    public Color HintColor;
    public Color PressedColor;

    [Range(0f, 1f)]
    public float MaterialLerpSpeed = 0.1f;

    private Material selfMaterial;
    private IEnumerator coroutine;

    private void OnEnable()
    {
        selfMaterial = GetComponent<MeshRenderer>().material;
    }
    public void ChangeToTarget()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = LerpMaterial(HintColor);
        StartCoroutine(coroutine);
    }
    public void ChangeToNormal()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = LerpMaterial(OriginColor);
        StartCoroutine(coroutine);
    }
    public void ChangeToPressed()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        selfMaterial.color = PressedColor;
        
    }

    IEnumerator LerpMaterial(Color targerMaterial)
    {
        Color originColor = selfMaterial.color;
        for (float i = 0f; i < 1f; i += Time.deltaTime * MaterialLerpSpeed)
        {
            selfMaterial.color = Color.Lerp(originColor, targerMaterial, i);
            yield return null;
        }
    }
}
