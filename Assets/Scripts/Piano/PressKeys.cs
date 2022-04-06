using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(/*typeof(AudioSource), */typeof(ChangeMaterial))]
public class PressKeys : MonoBehaviour
{
    public Vector3 RotateOrigin = new Vector3(-5.05f, -11.22f, 50.53f);
    [Range(0f, 10f)]
    public float RotateAngle = 3f;

    private GameObject _ripple;
    //private AudioSource _keyAudio;
    private ChangeMaterial _changeMaterial;

    private void Start()
    {
        //_keyAudio = GetComponent<AudioSource>();
        _changeMaterial = GetComponent<ChangeMaterial>();
    }
    
    public void PressKey()
    {
        _changeMaterial.ChangeToPressed();
        StartRotate();
    }
    public void ReleaseKey()
    {
        RecoverRotate();
        _changeMaterial.ChangeToNormal();
        if (transform.childCount > 0 && transform.GetChild(0).gameObject.activeInHierarchy)
        {
            transform.GetChild(0).GetComponent<RippleController>().RippleDiverge();
        }
    }

    private void StartRotate()
    {
        transform.RotateAround(RotateOrigin, Vector3.right, RotateAngle);
        //_keyAudio.Play();
    }
    private void RecoverRotate()
    {
        transform.RotateAround(RotateOrigin, Vector3.right, -1 * RotateAngle);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(RotateOrigin, 1f);
    }
}
