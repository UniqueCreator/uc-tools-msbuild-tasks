#include "default_signature.hlsli"

[numthreads(8, 8, 1)]
[RootSignature(MyRS2)]
void main( uint3 thread:SV_DispatchThreadID  )
{
    return;
}
