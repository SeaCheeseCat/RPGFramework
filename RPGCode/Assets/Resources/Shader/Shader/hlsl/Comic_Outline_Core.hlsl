#ifndef COMIC_OUTLINE_CORE
#define COMIC_OUTLINE_CORE

// include
#include "UnityCG.cginc"
#include "../hlsl/Comic_Function.hlsl"
#include "../hlsl/Comic_Macro.hlsl"

// Main
UNITY_DECLARE_TEX2D(_MainTex); uniform float4 _MainTex_ST;



uniform sampler sampler_linear_repeat;



// Outline
uniform bool _UseOutline;
uniform float4 _OutlineColor;
uniform float _OutlineWidth;
Texture2D<float4> _OutlineMaskTex; uniform float4 _OutlineMaskTex_ST;


struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD;
    float3 normal : NORMAL;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 pos : SV_POSITION;
    float4 color : COLOR;
    
};

// 頂点シェーダー
#include "../hlsl/Comic_Outline_Vert.hlsl"

// フラグメントシェーダー
#include "../hlsl/Comic_Outline_Frag.hlsl"

#endif