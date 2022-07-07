using System;
using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
    public event EventHandler<OnColliderEventArgs> OnCollider;
    public class OnColliderEventArgs : EventArgs
    {
        public Collider2D col;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnCollider?.Invoke(this, new OnColliderEventArgs {col = col});
    }
}
