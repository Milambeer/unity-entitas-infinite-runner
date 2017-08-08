using UnityEngine;

public class CollisionEmitter : MonoBehaviour {
    void OnCollisionEnter(Collision collision) {
        var link = gameObject.GetEntityLink();
        var targetLink = collision.gameObject.GetEntityLink();
        Contexts.sharedInstance.input.CreateEntity().AddCollision(link.entity, targetLink.entity);
    }
}