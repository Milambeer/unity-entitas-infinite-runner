using UnityEngine;
using Entitas;
using System.Collections.Generic;

public sealed class JumpSystem : ReactiveSystem<InputEntity>
{
    public JumpSystem(Contexts contexts) : base(contexts.input) {
    }

    protected override bool Filter(InputEntity entity) {
        return entity.hasJump;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.Jump);
    }

    protected override void Execute(List<InputEntity> entities) {
        foreach (InputEntity entity in entities) {
            GameEntity playerE = entity.jump.entity;
            GameObject playerG = playerE.view.gameObject;
            playerG.GetComponent<Rigidbody>().AddForce(playerG.transform.up * 5, ForceMode.Impulse);
            // playerG.GetComponent<Rigidbody>().velocity = new Vector3(0,7,0);
            entity.Destroy();
        }
    }

}