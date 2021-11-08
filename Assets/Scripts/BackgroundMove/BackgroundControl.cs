using System;
using UnityEngine;

namespace BackgroundMove
{
    public class BackgroundControl : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRendererComponent = null;

        [SerializeField] private float speed = 0;

        [SerializeField] private Rigidbody2D targetRigidBody2D = null;

        private void MoveTextureBackground(int direction)
        {
            Material material = meshRendererComponent.material;
            float x = material.mainTextureOffset.x + Time.deltaTime * speed * direction;
            const float y = 0;
            material.mainTextureOffset = new Vector2(x, y);
        }
        
        private void LateUpdate()
        {
            float x = targetRigidBody2D.velocity.x;
            if (x > 0) MoveTextureBackground(1);
            if (x < 0) MoveTextureBackground(-1);
        }
    }
}
