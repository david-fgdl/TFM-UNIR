void DistancePos_half(in flaot3 playerPos, in flaot3 worldPos, in float radius, in float3 primaryTexture, in float secondaryTexture, out float3 Out)
{
    if (distance(playerPos.xyz, worldPos.xyz) > radius) {
        Out = primaryTexture;
    } else if (distance(playerPos.xyz, worldPos.xyz) > radius - 0.2) {
        Out = float4(1, 1, 1, 1); // Color blanco
    } else {
        Out = secondaryTexture;
    }
}