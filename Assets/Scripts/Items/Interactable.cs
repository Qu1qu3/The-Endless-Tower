using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string description="Siuuu";
    public virtual void Interact() { }
    public string GetDescription() { return description; }
    public void SetDescription(string s) { description=s; }
}
