Shader "Comic Shader/Transparent"
{
    Properties
    {
        // Setting
        [Toggle]_VRChatMode ("VRChatMode", int) = 1

        // 影の色を優先する設定
        [Toggle]_ShadowPriority ("Shadow Priority", int) = 1
        
        // Main
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        // Decal
        [Toggle]_UseDecal ("Use Decal", int) = 0
        [Toggle]_DecalColorPriority ("Decal Color Priority", int) = 1
        [NoScaleOffset]_DecalTex ("Decal Texture", 2D) = "black" {}
        _DecalMask ("Decal Mask Texture", 2D) = "black" {}
        _DecalMaskPower ("Decal Mask Power", Range(0.0, 1.0)) = 0.0
        // DecalEdge
        [Toggle]_UseDecalEdge ("Use DecalEdge", int) = 0
        _DecalEdgeColor ("DecalEdge Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _DecalEdgePower ("DecalEdge Power", Range(0.001, 1.0)) = 0.5
        _DecalEdgeRange ("DecalEdge Range", Range(0.001, 0.5)) = 0.03
        _DecalEdgeBlur ("DecalEdge Blur", Range(0.001, 0.3)) = 0.001
        // DecalEmission
        [Toggle]_UseDecalEmission ("Use DecalEmission", int) = 0
        [HDR]_DecalEmissionHDRColor ("DecalEmissionHDRColor", Color) = (1.0, 1.0, 1.0, 1.0)
        _DecalEmissionPower ("DecalEmission Power", Range(0.0, 2.0)) = 0.0
        // DecalFlicker
        [Enum(Off, 0, SAW, 1, ABS_SIN, 2)]
        _DecalFlicker ("Flicker", int) = 0
        _DecalFlickerSpeed ("Flicker Speed", Range(0.1, 5.0)) = 1.0

        // Light
        [Enum(Right, 0, Left, 1, Up, 2)]
        _DirectionalLightDir("DirectionalLight Dir", int) = 0
        _DirectionalLightPower ("Directional Light Power", Range(0.001, 1.0)) = 0.5
        _PointLightPower("Point Light Power", Range(0.0, 1.0)) = 0.1
        
        // Shading
        [NoScaleOffset]_ShadowMask ("Shadow Mask", 2D) = "white" {}
        _ShadowPower("Shadow Power", Range(0.0, 1.0)) = 1.0

        // Line
        [Toggle]_UseLine ("Use Line", int) = 1
        _LineAmount ("Line Amount", int) = 50
        _LineAngle ("Line Angle", float) = 0.0

        // Toon
        [IntRange]_Cuts ("Toon Cuts", Range(1, 10)) = 3
        _CutsWidth ("Cut Width", Range(0.05, 1.0)) = 0.05

        // Normal
        [Normal]_BumpMap ("Normal Map", 2D) = "bump"{}
        _BumpScale ("Normal Scale", Range(0, 1)) = 1.0

        // Outline
        [Toggle]_UseOutline ("Use Outline", int) = 0
        [NoScaleOffset]_OutlineMaskTex ("OutLine Mask Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0.0, 0.0, 0.0, 1.0)
        _OutlineWidth ("Outline Width", Range(0.0, 1.0)) = 0.5
        
        // RimLight
        [Toggle]_UseRimLight ("Use RimLight", int) = 0
        [NoScaleOffset]_RimMask ("RimLight Mask", 2D) = "white" {}
        _RimColor ("RimLight Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _RimPower ("RimLight Power", Range(-5.0, 1.0)) = -1.0
    
        // Specular
        [Toggle]_UseSpecular ("Use Specular", int ) = 0
        [NoScaleOffset]_SpecularMask ("Specular Mask", 2D) = "white" {}
        _SpecularColor ("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _SpecularPower ("Specular Power", Range(1.0, 100)) = 10
        _Smoothness ("Smoothness", Range(1.0, 10)) = 1.0

        // Metallic
        [Toggle]_UseMetallic ("Use Metallic", int) = 0
        [NoScaleOffset]_MetallicMask ("Metallic Mask", 2D) = "white" {}
        _Metallic ("Metallic", Range(0.0, 1.0)) = 0.0

        // MatCap
        [Toggle]_UseMatCap ("Use MatCap", int) = 0
        [NoScaleOffset]_MatCapTex ("MatCap Texture", 2D) = "black" {}
        _MatCapColor ("MatCap Color", Color) = (1.0, 1.0, 1.0, 1.0)
        [NoScaleOffset]_MatCapMask ("MatCap Mask", 2D) = "white" {}
        _MatCapPower ("MatCap Power", Range(0.0, 2.0)) = 1.0

        // Emission
        [Toggle]_UseEmission ("Use Emission", int) = 0
        _EmissionTex ("Emission Texture", 2D) = "black" {}
        [HDR]_EmissionHDRColor ("Emission Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _EmissionMask ("Emission Mask", 2D) = "white" {}
        _EmissionPower ("Emission Power",Range(0.0, 2.0)) = 0.0
        // Flicker
        [Enum(Off, 0, SAW, 1, ABS_SIN, 2)]
        _Flicker ("Flicker", int) = 0
        _FlickerSpeed ("Flicker Speed", Range(0.1, 5.0)) = 1.0
        [Toggle]_UseEmissionAnimation ("Use EmissionAnimation", int) = 0
        _EmissionAnimationSpeed ("EmissionAnimation Speed", Range(0.0, 10.0)) = 0.0
        _EmissionScrollX ("Emission ScrollX", Range(-1.0, 1.0)) = 0.0
        _EmissionScrollY ("Emission ScrollY", Range(-1.0, 1.0)) = 0.0

        // OtherSettings
        [Enum(Off, 0, Back, 2, Front, 1)]
        _Culling ("Culling", int) = 2
    }


    SubShader
    {
        Tags
        {
            "IgnoreProjector" = "False"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        LOD 0

        Pass
        {
            Name "ForwardBase"
            Tags
            {
                "LightMode" = "ForwardBase"
            }

            Cull [_Culling]
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite ON

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 4.5

            #define FB
            #define TRANSPARENT
            #include "../Shader/hlsl/Comic_Core.hlsl"

            ENDHLSL
        }

        // Outline
        Pass
        {
            Name "Outline"
            Tags
            {
                "LightMode" = "ForwardBase"
            }

            Cull Front

            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5

            #define TRANSPARENT

            #include "../Shader/hlsl/Comic_Outline_Core.hlsl"

            ENDHLSL
        }

        // ForwardAdd
        Pass
        {
            Name "ForwardAdd"
            Tags
            {
                "LightMode"  = "ForwardAdd"
            }

            Cull [_Culling]
            Blend SrcAlpha One
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma target 4.5

            #define FA
            #define TRANSPARENT

            #include "../Shader/hlsl/Comic_Core.hlsl"

            ENDHLSL
        }

        // ShadowCaster
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster

            #pragma multi_compile_instancing
            #pragma target 4.5

            #define TRANSPARENT

            #include "../Shader/hlsl/Comic_ShadowCaster.hlsl"

            ENDHLSL
        }
    }
    CustomEditor "ComicShaderGUI"
}
