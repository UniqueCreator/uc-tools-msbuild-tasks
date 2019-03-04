#include "default_signature.hlsli"

[RootSignature( MyRS1 ) ]
float4 main() : SV_Target0
{
    return float4(1.0f, 0.0f, 0.0f, 1.0f);
}
