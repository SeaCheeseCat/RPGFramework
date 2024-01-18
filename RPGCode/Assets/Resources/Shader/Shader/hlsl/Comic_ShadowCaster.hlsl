#ifndef COMIC_SHADOWCASTER
#define COMIC_SHADOWCASTER

// include
#include "UnityCG.cginc"
#include "../hlsl/Comic_Function.hlsl"
#include "../hlsl/Comic_Macro.hlsl"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
    float2 uv : TEXCOORD0;
    V2F_SHADOW_CASTER;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

v2f vert (appdata v)
{
    v2f o;

    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_TRANSFER_INSTANCE_ID(v, o);
    
    o.pos = UnityObjectToClipPos(v.vertex);

    o.uv = v.uv;
    TRANSFER_SHADOW_CASTER(o);
    return o;
}

float4 frag(v2f i) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(i);
    SHADOW_CASTER_FRAGMENT(i);
}

#endif