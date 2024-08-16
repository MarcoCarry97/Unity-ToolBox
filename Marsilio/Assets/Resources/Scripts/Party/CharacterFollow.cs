using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Control;
using Unity.VisualScripting.Dependencies.NCalc;
using ToolBox.Control.Explorer2D;
using ToolBox.Animations;

public class CharacterFollow : MonoBehaviour
{
    [SerializeField]
    private CharacterController2D controller;

    [SerializeField]
    private CharacterController2D following;

    public float distance;

    public AnimatorController3D animator;

    public CharacterController2D Following
    {
        get
        {
            return following;
        }
        set
        {
            following = value;
        }
    }

    private void Start()
    {
        controller = this.GetComponent<CharacterController2D>();
        //GameObject mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter");
        //main=mainCharacter.GetComponent<ToolBox.Control.CharacterController>();
        animator = this.GetComponent<AnimatorController3D>();
    }



    private void Update()
    {
        if(following!=null)
        {
            float speed = controller.Speed = following.Speed;
            Vector3 direction = Vector3.zero;
            float dist = Vector3.Distance(this.transform.position, following.transform.position);
            if (dist > distance)
            {
                direction = (following.transform.position - this.transform.position).normalized;
            }
            controller.Move(direction);
            animator.SetDirection(direction);
        }
    }

    private (bool,RaycastHit) DoRayCast()
    {
        RaycastHit hit;
        Vector2 vec = following.transform.position - this.transform.position;
        bool check = Physics.Raycast(transform.position, vec, out hit);
        return (check, hit);
    }

    /*private void OnDrawGizmos()
    {
        (bool,RaycastHit) res=DoRayCast();
        Gizmos.color = res.Item1 ? Color.green : Color.red;
        Vector3 vec = main.transform.position - this.transform.position;
        Debug.DrawRay(transform.position, vec, Color.white);

    }*/
}
