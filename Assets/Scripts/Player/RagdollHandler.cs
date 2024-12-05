using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    public Animator animator;
    private List<Rigidbody> _rbs = new List<Rigidbody>();
    private List<Collider> _colliders = new List<Collider>();

    void Awake()
    {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            _rbs.Add(rb);

        foreach (Collider collider in GetComponentsInChildren<Collider>())
            _colliders.Add(collider);
    }

    void Start()
    {
        Toggle(false);
    }

    public void Toggle(bool ragdolled)
    {
        foreach (Rigidbody rb in _rbs)
            rb.isKinematic = !ragdolled;

        foreach (Collider collider in _colliders)
            collider.enabled = ragdolled;

        animator.enabled = !ragdolled;
    }
}
