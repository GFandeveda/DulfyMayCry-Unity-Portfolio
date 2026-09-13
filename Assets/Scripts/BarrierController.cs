using System;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider barrierCollider;
    [SerializeField] private MeshRenderer barrierRenderer;

    public Action OnBarrierClosed;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (barrierCollider == null)
            barrierCollider = GetComponent<Collider>();

        if (barrierRenderer == null)
            barrierRenderer = GetComponent<MeshRenderer>();

        // Começa invisível e sem colisão
        barrierRenderer.enabled = false;
        barrierCollider.enabled = false;
    }

    public void CloseBarrier()
    {
        // Torna a barreira visível e sólida
        barrierRenderer.enabled = true;
        barrierCollider.enabled = true;

        animator.SetTrigger("Close");
    }

    public void OpenBarrier()
    {
        Debug.Log("OpenBarrier chamado!");
        animator.SetTrigger("Open");
    }
    // Animation Event no último frame da animação BarrierClose
    public void BarrierClosed()
    {
        Debug.Log("BarrierClosed chamado!");

        OnBarrierClosed?.Invoke();
    }

    // Animation Event no último frame da animação BarrierOpen
    public void DisableBarrier()
    {
        Debug.Log("DisableBarrier chamado!");

        barrierCollider.enabled = false;
        barrierRenderer.enabled = false;
    }
}