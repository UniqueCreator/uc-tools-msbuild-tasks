#include "default_signature.hlsli"

struct interpolants
{
    float4 position     : SV_POSITION0;
    float2 uv           : texcoord0;
};

struct input
{
    float3 position : position;
    float2 uv       : texcoord0;
};

[RootSignature( MyRS1 ) ]
interpolants main( input i )
{
    interpolants r;
    r.uv                    = float2(0,0);
    r.position              = float4(0,0,0,1);
    return r;
}





