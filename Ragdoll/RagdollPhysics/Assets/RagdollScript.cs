using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    private Rigidbody[] ragdollRB;
    private Collider[] ragdollColliders;
    bool isRagdoll = false;
    public bool canCollide;
    // Start is called before the first frame update
    void Start()
    {
        ragdollRB = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        SetRagDollEnabled(isRagdoll);
    }

    public void SetRagDollEnabled(bool enabled)
    {
        foreach (Rigidbody rb in ragdollRB) {
        rb.isKinematic = !enabled;
        }
    }

    public void SetCollisionEnabled(bool enabled) {
        foreach(Collider col in ragdollColliders) {
            col.enabled = enabled;
        }
    }

    void FixedUpdate()
    {
        SetCollisionEnabled(canCollide);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Collision detected.");
            isRagdoll = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
