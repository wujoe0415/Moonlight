using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriggerKey
{
    public GameObject InteractableArea;
    public List<RippleKey> RippleKeys;
    public float Delay;
}
[System.Serializable]
public class RippleKey
{
    public GameObject Key;
    public Vector3 InitPos;
}
public class InteractionKeys : MonoBehaviour
{
    [Range(0f, 10f)]
    public float OpeningWait;
    public List<TriggerKey> Keys = new List<TriggerKey>();

    public GetRippleSystem GenerateRippleSystem;
    
    private PlayMoonlight currentKey;
    private IEnumerator playPiano;

    void OnEnable()
    {
        if (playPiano != null)
            return;

        playPiano = PlayPiano();
        StartCoroutine(playPiano);
    }

    IEnumerator PlayPiano()
    {
        while (true)
        {
            yield return new WaitForSeconds(OpeningWait);

            for (int i = 0; i < Keys.Count; i++)
            {
                List<GameObject> key = new List<GameObject>();
                foreach (RippleKey rippleKey in Keys[i].RippleKeys)
                {
                    GenerateRippleSystem.AssignRippleSystem(rippleKey.Key.transform, rippleKey.InitPos);
                    rippleKey.Key.GetComponent<ChangeMaterial>().ChangeToTarget();
                    key.Add(rippleKey.Key);
                }
                Keys[i].InteractableArea.SetActive(true);
                currentKey = Keys[i].InteractableArea.GetComponent<PlayMoonlight>();

                currentKey.SetPresskeys(key);
                while (!currentKey.isPressed)
                    yield return null;

                yield return new WaitForSeconds(Keys[i].Delay);
            }

            yield return null;
            break;
        }
        yield return null;
    }
}


