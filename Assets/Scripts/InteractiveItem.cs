using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveItem : MonoBehaviour {

    public string name;

    public abstract void Interact();
}
