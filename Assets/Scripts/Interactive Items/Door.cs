using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveItem {

    [SerializeField]
    private GameObject innerDoor;
    public Animator anim;
    public GameController controller;

    override public void Interact() {
        //Open door & end game
        anim.Play("doorAnimation");
    }
}
