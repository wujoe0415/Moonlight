using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayMoonlight : MonoBehaviour
{
    [Header("Trigger Settings")]
    public KeyCode CorrespondingKey;
    public bool isPressed = false;
    //public List<PressKeys> Presskeys = new List<PressKeys>();
    [Range(0f, 30f)]
    public float DisableAfterPressing;

    [Header("Post Processing Settings")]
    [Range(0f, 30f)]
    public float PostStillTime = 3f;
    public PostImpact Impact;

    
    private bool hasPressed = false;
    private AudioSource _moonlightFraction;
    private List<PressKeys> _presskeys = new List<PressKeys>();

    private void OnEnable()
    {
        _moonlightFraction = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(CorrespondingKey) && !hasPressed)
        {
            isPressed = true;
            foreach(PressKeys presskey in _presskeys)
            {
                presskey.PressKey();
            }
            _moonlightFraction.Play();
        }
        if (Input.GetKeyUp(CorrespondingKey) && !hasPressed)
        {
            foreach (PressKeys presskey in _presskeys)
            {
                presskey.ReleaseKey();
            }
            isPressed = false;
            hasPressed = true;
            Invoke("Disable", DisableAfterPressing);
            Impact.TriggerImpact(PostStillTime);
        }
    }
    private void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public void SetPresskeys(List<GameObject> presskeys)
    {
        foreach (GameObject presskey in presskeys)
            _presskeys.Add(presskey.GetComponent<PressKeys>());
    }
}
