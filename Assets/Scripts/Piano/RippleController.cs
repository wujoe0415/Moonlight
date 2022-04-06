using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleController : MonoBehaviour
{
    public ParticleSystem RippleConvergence;
    public ParticleSystem RippleDivergence;
    [Range(0f, 10f)]
    public float ConvergeDuration;
    [Range(0f, 10f)]
    public float DivergeDuration;

    private ChangeMaterial _change;

    private IEnumerator converge;
    private IEnumerator diverge;

    private void Start()
    {
        RippleConvergence.Pause();
        RippleDivergence.Pause();
    }
    private void OnEnable()
    {
        Invoke("RippleConverge", 0f);
    }

    public void RippleConverge()
    {
        if (converge != null)
            return;
        converge = StartConverge();
        StartCoroutine(converge);
    }
    public void RippleDiverge()
    {
        if (diverge != null)
            return;
        diverge = StartDiverge();
        StartCoroutine(diverge);
    }

    IEnumerator StartConverge()
    {
        RippleConvergence.Clear();
        RippleConvergence.Simulate(0f, true, true);
        RippleConvergence.Play();
        yield return new WaitForSeconds(ConvergeDuration);

        RippleConvergence.Pause();
        converge = null;
        yield return null;
    }
    IEnumerator StartDiverge()
    {
        RippleDivergence.Clear();
        RippleDivergence.Simulate(0f, true, true);
        RippleDivergence.Play();
        yield return new WaitForSeconds(DivergeDuration);

        RippleDivergence.Pause();
        diverge = null;
        yield return null;

        if (GameObject.Find("piano-keys").GetComponent<GetRippleSystem>())
            GameObject.Find("piano-keys").GetComponent<GetRippleSystem>().RecycleRippleSystem(this.gameObject);
        else
            this.gameObject.SetActive(false);
    }
}
