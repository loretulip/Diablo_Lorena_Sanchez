using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerVisualSystem : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int visualId;
    [SerializeField] private Player mainScript;
    [SerializeField] private NavMeshAgent agent;

    private Animator anim;
    private void Awake()
    {
        if(gM.IdCharacterSelected == visualId)
        {
            anim = GetComponent<Animator>();
            mainScript.VisualSystem = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);
    }

    //Por evento
    private void LanzarAtaque()
    {
        mainScript.Atacar();
    }

    public void EjecutarAnimacionMuerte()
    {
        anim.SetTrigger("death");
    }

    public void StartAttacking()
    {
        anim.SetBool("attacking", true);
    }

    public void StopAttacking()
    {
        anim.SetBool("attacking", false);
    }
}
