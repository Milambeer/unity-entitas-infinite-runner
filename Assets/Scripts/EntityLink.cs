using UnityEngine;
using Entitas;
using System;

public class EntityLink : MonoBehaviour {
    public GameEntity entity { get { return _entity; } }
    public IContext<GameEntity> context { get { return _context; } }

    private GameEntity _entity;
    private IContext<GameEntity> _context;

    public void Link(GameEntity entity, IContext<GameEntity> context) {
        if (_entity != null) {
            throw new Exception("EntityLink is already linked to " + _entity);
        }

        _entity = entity;
        _context = context;
        _entity.Retain(this);
    }

    public void Unlink() {
        if (_entity == null) {
            throw new Exception("EntityLink is already unlinked");
        }

        _entity.Release(this);
        _entity = null;
        _context = null;
    }
}

public static class EntityLinkExtension {
     public static EntityLink GetEntityLink(this GameObject gameObject) {
         return gameObject.GetComponent<EntityLink>();
     }

     public static EntityLink Link(this GameObject gameObject, GameEntity entity, IContext<GameEntity> context) {
         var link = gameObject.GetEntityLink();
         if (link == null) {
             link = gameObject.AddComponent<EntityLink>();
         }

         link.Link(entity, context);
         return link;
     }
    
    public static void Unlink(this GameObject gameObject) {
        gameObject.GetEntityLink().Unlink();
    }
}