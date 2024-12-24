using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

partial struct UnitMoverSystem : ISystem {

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        foreach ((
            RefRW<LocalTransform> localTransform, 
            RefRO<UnitMover> unitMover,
            RefRW<PhysicsVelocity> physicsVelocity) 
            in SystemAPI.Query<
                RefRW<LocalTransform>, 
                RefRO<UnitMover>,
                RefRW<PhysicsVelocity>>()) {

            float3 moveDirection = unitMover.ValueRO.targetPosition - localTransform.ValueRO.Position;
            moveDirection = math.normalize(moveDirection);

            localTransform.ValueRW.Rotation =
                math.slerp(localTransform.ValueRO.Rotation, 
                            quaternion.LookRotation(moveDirection, math.up()), 
                            SystemAPI.Time.DeltaTime * unitMover.ValueRO.rotationSpeed);

            physicsVelocity.ValueRW.Linear = moveDirection * unitMover.ValueRO.moveSpeed;
            physicsVelocity.ValueRW.Angular = float3.zero;
        }
    }

}
