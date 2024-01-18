#ifndef COMIC_CORE
#define COMIC_CORE

#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "Lighting.cginc"
#include "../hlsl/Comic_Function.hlsl"
#include "../hlsl/Comic_Macro.hlsl"

// Setting
uniform bool _VRChatMode;
uniform bool _ShadowPriority;

// Main
UNITY_DECLARE_TEX2D(_MainTex); uniform float4 _MainTex_ST;
uniform float4 _Color;
// Decal
uniform bool _UseDecal;
uniform bool _DecalColorPriority;
Texture2D _DecalTex; uniform float4 _DecalTex_ST;
Texture2D _DecalMask; uniform float4 _DecalMask_ST;
uniform float _DecalMaskPower;
uniform bool _UseDecalEdge;
uniform float _DecalEdgePower;
uniform float3 _DecalEdgeColor;
uniform float _DecalEdgeRange;
uniform float _DecalEdgeBlur;
// DecalEmission
uniform bool _UseDecalEmission;
uniform float4 _DecalEmissionHDRColor;
uniform float _DecalEmissionPower;
// DecalFlicker
uniform int _DecalFlicker;
uniform float _DecalFlickerSpeed;

// Light
uniform int _DirectionalLightDir;
uniform float _DirectionalLightPower;
uniform float _PointLightPower;

// Line
uniform bool _UseLine;
uniform uint _LineAmount;
uniform float _LineAngle;

// Shading
Texture2D _ShadowMask;
uniform bool _ShadowMaskInversion;
uniform float _ShadowPower;

// Toon
uniform uint _Cuts;
uniform float _CutsWidth;

// Normal
Texture2D _BumpMap; uniform float4 _BumpMap_ST; 
uniform float _BumpScale;

// RimLight
uniform bool _UseRimLight;
Texture2D _RimMask; uniform float4 _RimMask_ST;
uniform float3 _RimColor;
uniform float _RimPower;

// Specular
uniform bool _UseSpecular;
Texture2D _SpecularMask; uniform float4 _SpecularMask_ST;
uniform float3 _SpecularColor;
uniform float _SpecularPower;
uniform float _Smoothness;

// Metallic
uniform bool _UseMetallic;
Texture2D _MetallicMask; uniform float4 _MetallicMask_ST;
uniform float _Metallic;

// MatCap
uniform bool _UseMatCap;
uniform sampler2D _MatCapTex;
uniform float3 _MatCapColor;
Texture2D _MatCapMask;  uniform float4 _MatCapMask_ST; 
uniform float _MatCapPower;

// Emission
uniform bool _UseEmission;
UNITY_DECLARE_TEX2D(_EmissionTex); uniform float4 _EmissionTex_ST;
uniform float4 _EmissionHDRColor;
Texture2D _EmissionMask; uniform float4 _EmissionMask_ST;
uniform float _EmissionPower;
// Flicker
uniform int _Flicker;
uniform float _FlickerSpeed;

// EmissinAnimation
uniform bool _UseEmissionAnimation;
uniform float _EmissionAnimationSpeed;
uniform float _EmissionScrollX;
uniform float _EmissionScrollY;

// OtherSettings
#ifdef CUTOUT
    uniform float _Cutoff;
#endif

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD;
    float3 normal : NORMAL;
    float4 tangent : TANGENT;
    float3 color : COLOR;
};

struct v2f
{
    // uv.xy : テクスチャ座標(0.0 ~ 1.0)
    // uv.zw : 縞模様生成用
    float4 uv : TEXCOORD0;
    float4 pos : SV_POSITION;
    float3 color : COLOR;
    float3 normal : NORMAL;
    float3 lightDirWS : TEXCOORD1;
    float3 posWS : TEXCOORD2;
    float3 normalWS : TEXCOORD3;
    float3 tspace0 : TEXCOORD4;
    float3 tspace1 : TEXCOORD5;
    float3 tspace2 : TEXCOORD6;
    UNITY_SHADOW_COORDS(7)
    float3 vertexLight : TEXCOORD8;
    float4 emiuv : TEXCOORD9;

    
};

// 頂点シェーダー
#include "../hlsl/Comic_Vert.hlsl"

// フラグメントシェーダー
#include "../hlsl/Comic_Frag.hlsl"

#endif