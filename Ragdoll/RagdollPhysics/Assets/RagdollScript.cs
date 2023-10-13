using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    float rigidbodyActiveDist = 5f;
    private Rigidbody[] ragdollRB;
    private Collider[] ragdollColliders;
    // Start is called before the first frame update
    void Start()
    {
        ragdollRB = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        SetRagDollDisabled();
    }

    public void SetRagDollEnabled()
    {
        foreach (Rigidbody rb in ragdollRB) {
        rb.isKinematic = !enabled;
        }
        foreach(Collider col in ragdollColliders) {
            col.enabled = enabled;
        }
    }

    public void SetRagDollDisabled() 
    {
        foreach (Rigidbody rb in ragdollRB) {
        rb.isKinematic = enabled;
        }
        foreach(Collider col in ragdollColliders) {
            col.enabled = enabled;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            SetRagDollEnabled();
            Debug.Log("Collision detected.");
        }
    }
}
