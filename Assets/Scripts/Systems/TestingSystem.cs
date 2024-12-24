using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

partial struct TestingSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ((
            RefRW<LocalTransform> localTransform,
            RefRO<UnitMover> unitMover,
            RefRW<PhysicsVelocity> physicsVelocity)
            in SystemAPI.Query<
                RefRW<LocalTransform>,
                RefRO<UnitMover>,
                RefRW<PhysicsVelocity>>().WithPresent<Selected>())
        {
        }
    }
}
