#ifndef COMIC_VERT
#define COMIC_VERT

v2f vert (appdata v)
{
    v2f o;
    // =====初期化=====
    UNITY_INITIALIZE_OUTPUT(v2f, o);

    o.pos = UnityObjectToClipPos(v.vertex);
    o.posWS = mul(unity_ObjectToWorld, v.vertex);
    o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
    

    if(_VRChatMode)
    {
        o.uv.zw = o.posWS;
    }
    else
    {
        float4 clipSpacePos = mul(UNITY_MATRIX_VP, float4(o.posWS, 1.0));
        float3 ndcSpacePos = clipSpacePos.xyz / clipSpacePos.w;
        float2 windowSpacePos = ((ndcSpacePos.xy + 1.0) / 2.0);
        #if UNITY_UV_STARTS_AT_TOP
            windowSpacePos.y = 1-windowSpacePos.y;
        #endif
        o.uv.zw = windowSpacePos;
    }
    


    // =====Normal=====
    o.normal = v.normal;
    o.normalWS = normalize(UnityObjectToWorldNormal(v.normal));

    // =====Tangent=====
    float3 tangentWS = UnityObjectToWorldDir(v.tangent.xyz);
    float tangentSign = v.tangent.w * unity_WorldTransformParams.w;

    // =====Bitangent=====    
    float3 bitangentWS = cross(o.normalWS, tangentWS) * tangentSign;


    // =====接線空間マトリクス行列=====
    o.tspace0 = float3(tangentWS.x, bitangentWS.x, o.normalWS.x);
    o.tspace1 = float3(tangentWS.y, bitangentWS.y, o.normalWS.y);
    o.tspace2 = float3(tangentWS.z, bitangentWS.z, o.normalWS.z);

    // =====頂点カラー=====
    o.color = v.color;


    // =====Light=====
    o.lightDirWS =  _WorldSpaceLightPos0.w == 0 ?
                    _WorldSpaceLightPos0.xyz :
                    _WorldSpaceLightPos0.xyz - o.posWS;
    o.lightDirWS = normalize(o.lightDirWS);

    // =====VertexLight=====
    #ifdef FB
        #if defined(UNITY_SHOULD_SAMPLE_SH) && defined(VERTEXLIGHT_ON)
            o.vertexLight += Shade4PointLights(
                unity_4LightPosX0,
                unity_4LightPosY0,
                unity_4LightPosZ0,
                unity_LightColor[0].rgb,
                unity_LightColor[1].rgb,
                unity_LightColor[2].rgb,
                unity_LightColor[3].rgb,
                unity_4LightAtten0,
                o.posWS,
                o.normalWS);
        #endif
    #endif

    // ====EmissionUV====

    // ===EmissionUVBase===
    o.emiuv.xy = (v.uv.xy * _MainTex_ST.xy) + _EmissionTex_ST.zw;
    o.emiuv.zw = float2(0.0, 0.0);

    // ===EmissionUVScroll===

    // emissionAni.xy : animationSpeed per second
    // emissionAni.zw : emissionTexture Size
    float4 emissionAni = float4(0.0, 0.0, 1.0, 1.0);

    emissionAni.zw = float2(_EmissionTex_ST.x, _EmissionTex_ST.y);

    if(_UseEmissionAnimation)
    {
        emissionAni.xy += _EmissionAnimationSpeed * _Time.y * emissionAni.zw;
    }

    o.emiuv.xy = o.emiuv.xy * emissionAni.zw;
    o.emiuv.xy += float2(_EmissionScrollX, _EmissionScrollY) * emissionAni;

    return o;
}

#endif