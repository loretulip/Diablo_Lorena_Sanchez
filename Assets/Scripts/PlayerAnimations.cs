using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] 
    private Player main;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //main = PlayerAnimations this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EjecutarAtaque()
    {
        anim.SetBool("attacking", true);
    }
    public void PararAtaque()
    {
        anim.SetBool("attacking", false);

    }
}
