using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingAttack : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Space))
        animator.SetTrigger("ringAttack");
    }
    // public void FireAnimation(Vector3 coords)
    // {
    //   animator.SetTrigger("ringAttack");
    // }

}
