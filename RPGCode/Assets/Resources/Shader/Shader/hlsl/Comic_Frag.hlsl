#ifndef COMIC_FRAG
#define COMIC_FRAG

float4 frag(v2f i) : SV_Target
{
    // =====初期化=====
    float4 lastColor = float4(0.0, 0.0, 0.0, 1.0);
    float4 col = 0.0;

    // =====ViewDir=====
    float3 ViewDir = normalize(_WorldSpaceCameraPos - i.posWS);

    // =====LightDir=====
    float lightColorTotal = _LightColor0.r + _LightColor0.g + _LightColor0.b;

    float3 LightDir = i.lightDirWS;
    // ディレクショナルライトが無かったら疑似的にDLのベクトルを生成。
    if(lightColorTotal == 0.0)
    {
        if(_DirectionalLightDir == 0)   LightDir = normalize(float3(1.0, 1.0, 0.0));
        if(_DirectionalLightDir == 1)   LightDir = normalize(float3(-1.0, 1.0, 0.0));
        if(_DirectionalLightDir == 2)   LightDir = normalize(float3(0.0, 1.0, 0.0));
    }

    // =====HalfDir=====
    float3 HalfDir = normalize(_WorldSpaceLightPos0.xyz + ViewDir);

    // =====Albedo=====
    
    // ===AlbedoBase===
    float3 albedo = UNITY_SAMPLE_TEX2D(_MainTex, i.uv.xy).rgb * _Color.rgb;

    // ===Decal===
    float4 decalColor = 0.0;
    float decalEdgeFlag = 1.0;
    if(_UseDecal)
    {
        // ===DecalBase===
        decalColor = COMIC_SAMPLE_2D_ST(_DecalTex, sampler_MainTex, i.uv.xy);

        // ===DecalMask===
        float decalMask = COMIC_SAMPLE_2D_ST(_DecalMask, sampler_MainTex, i.uv.xy).r;

        decalMask = saturate(decalMask + (1.0 - 2.0 * _DecalMaskPower));

        // ===Mask & alpha Composition===   
        decalColor.a = decalColor.a * decalMask;
        

        // ===DecalEdge===
        if(_UseDecalEdge)
        {
            float decalEdge = smoothstep(_DecalEdgeRange + _DecalEdgeBlur, _DecalEdgeRange, saturate(decalColor.a - _DecalEdgePower));
            float3 decalEdgeColor = float3(decalEdge * _DecalEdgeColor);
            decalColor.rgb = decalColor.rgb * (1.0 - decalEdge) + decalEdgeColor * decalEdge;
            decalEdgeFlag = step(_DecalEdgePower, decalColor.a);
        }

        // ===DecalColor===
        #ifdef FB
            albedo = albedo * (1.0 - decalColor.a * decalEdgeFlag) + (decalColor * decalColor.a * decalEdgeFlag);
        #endif

        #ifdef FA
            if(_DecalColorPriority)
            {
                albedo = albedo * (1.0 - decalColor.a * decalEdgeFlag) + decalColor * decalColor.a * decalEdgeFlag;
            }
        #endif
    }
    
    // アルベド格納
    col.rgb = albedo;

    // アルファ設定
    #if defined(TRANSPARENT) || defined(CUTOUT)
        col.a = UNITY_SAMPLE_TEX2D(_MainTex, i.uv.xy).a * _Color.a;
    #endif

    #if defined(CUTOUT)
        clip(col.a - _Cutoff);
    #endif
    
    
    // =====Normal=====
    float4 normalMapTex = COMIC_SAMPLE_2D_ST(_BumpMap, sampler_MainTex, i.uv.xy);
    float3 normalMap = UnpackScaleNormal(normalMapTex, _BumpScale);
    normalMap = normalize(normalMap);
    
    float3 Normal;
    Normal.x = dot(i.tspace0, normalMap);
    Normal.y = dot(i.tspace1, normalMap);
    Normal.z = dot(i.tspace2, normalMap);


    // =====Diffuse=====
    float NdotL = dot(Normal, LightDir);
    float NdotL_ZeroOne = NdotL * 0.5 + 0.5;
    float diffuse = NdotL_ZeroOne;


    // =====ToonDiffuse=====
    diffuse = diffuse / _CutsWidth;
    float toonDiffuse = ceil(diffuse);
    toonDiffuse = toonDiffuse / _Cuts;
    toonDiffuse = saturate(toonDiffuse);

    // shadowMask作成
    float shadowMask = 1.0;
    if(_UseLine)
    {
        shadowMask = COMIC_SAMPLE_2D(_ShadowMask, sampler_MainTex, i.uv.xy);
        shadowMask = 1.0 - shadowMask;
    }
    

    #ifdef FB
        // toonDiffuseと加算してマスクを有効化
        toonDiffuse = saturate(toonDiffuse + shadowMask);

        // ===MakeLine===
        float lineMask = MakeLine(i.uv.zw, _LineAmount, toonDiffuse, _LineAngle);
        toonDiffuse = lerp(lineMask, toonDiffuse, saturate(toonDiffuse - lineMask));

        // ===ShadowPower===
        toonDiffuse = saturate(toonDiffuse + (1.0 - _ShadowPower));
    #endif

    // =====Ambient=====
    float3 ambient = 0.0;
    #ifdef FB
        ambient = ShadeSH9(float4(Normal, 1.0)) * _Color.rgb;
    #endif
    
    // =====Attenuation=====
    UNITY_LIGHT_ATTENUATION(atten, i, i.posWS)

    // =====MainLight=====
    float3 mainLightColor = 0.0;
    #ifdef FB
        mainLightColor = _LightColor0 * _DirectionalLightPower;
    #endif

    #ifdef FA
        mainLightColor = _LightColor0 * _PointLightPower * atten;
        mainLightColor = saturate(mainLightColor);
    #endif

    // ====LightComposition====
    #ifdef FB
        float directionalLightFlag = 1.0;
        // DLが存在すれば1.0、なければ0.0
        directionalLightFlag = abs(sign(lightColorTotal));
        i.vertexLight = i.vertexLight * directionalLightFlag;

        if(_ShadowPriority)
        {
            // 影を優先する
            col.rgb *= mainLightColor + ambient;
            col.rgb += i.vertexLight;
        }
        else
        {
            col.rgb *= (toonDiffuse * mainLightColor) + i.vertexLight + (toonDiffuse * ambient);
        }
    #endif

    #ifdef FA
        if(_ShadowPriority)
        {
            col.rgb *= mainLightColor;
        }
        else
        {
            col.rgb *= toonDiffuse * mainLightColor;
        }
    #endif

    // =====RimLight=====
    float3 rimLight = 0.0;
    if(_UseRimLight)
    {
        // ===RimLightBase===
        float rim = (1.0 - abs(dot(Normal, ViewDir)));

        // ===RimLightMask===
        float rimMask = COMIC_SAMPLE_2D_ST(_RimMask, sampler_MainTex, i.uv.xy);
        rim *= rimMask;

        // ===RimLightColor===
        rim = saturate(myPow(rim, _RimPower));

        #ifdef FB 
            rimLight = rim * _RimColor;
        #endif

        #ifdef FA 
            rimLight = rim * _RimColor * atten;
        #endif
    }
    
    // =====Specular=====
    float3 specular = 0.0;
    if(_UseSpecular)
    {
        // ===SpecularBase===
        float3 ReflectionDir = normalize(-LightDir + 2.0 * Normal * NdotL);
        specular = pow(saturate(dot(ReflectionDir, ViewDir)), _SpecularPower * _Smoothness);
        specular = saturate(specular * _Smoothness);
        
        // ===SpecularMask===
        float specularMask = COMIC_SAMPLE_2D_ST(_SpecularMask, sampler_MainTex, i.uv.xy);
        specular *= specularMask;

        // ===SpecularColor===
        #ifdef FB 
            specular *= _SpecularColor;
        #endif

        #ifdef FA 
            specular *= mainLightColor;
        #endif
    }

    // =====Metallic======
    float3 reflection = 0.0;
    if(_UseMetallic)
    {
        // https://light11.hatenadiary.com/entry/2018/07/08/212014
        float3 ReflectionDir = reflect(-ViewDir, Normal);
        #if UNITY_SPECCUBE_BOX_PROJECTION
            if(unity_SpecCube0_ProbePosition.w > 0)
            {
                float3 magnitudes = ((ReflectionDir > 0 ? unity_SpecCube0_BoxMax.xyz : unity_SpecCube0_BoxMin.xyz) - i.posWS) / ReflectionDir;
                float magnitude = min(min(magnitudes.x, magnitudes.y), magnitudes.z);
                ReflectionDir = ReflectionDir * magnitude + (i.posWS - unity_SpecCube0_ProbePosition);
            }
        #endif
        
        float lod = (10.0 - _Smoothness) * 0.5;

        float4 skyData0 = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, ReflectionDir, lod);
        skyData0.rgb = DecodeHDR(skyData0, unity_SpecCube0_HDR);

        float4 skyData1 = UNITY_SAMPLE_TEXCUBE_SAMPLER_LOD(unity_SpecCube1, unity_SpecCube0, ReflectionDir, lod);
        skyData1.rgb = DecodeHDR(skyData1, unity_SpecCube1_HDR);

        reflection = lerp(skyData0, skyData1, unity_SpecCube0_BoxMin.w);
    }

    // =====MatCap=====
    float3 materialCapture = 0.0;
    if(_UseMatCap)
    {
        // ===MatCapUV===
        float3 matCapUV = mul((float3x3)UNITY_MATRIX_V, i.normalWS);
        matCapUV = matCapUV * 0.5 + 0.5;

        // ===MatCapBase===
        materialCapture = tex2D(_MatCapTex, matCapUV) * _MatCapPower;

        // ===MatCapMask===
        float matCapMask = COMIC_SAMPLE_2D_ST(_MatCapMask, sampler_MainTex, i.uv.xy);
        materialCapture *= matCapMask;
        
        // ===MatCapColor===
        #ifdef FB 
            // FBならMatCapColorと乗算
            materialCapture = materialCapture * _MatCapColor;
        #endif

        #ifdef FA 
            // FAならライトカラーと乗算
            materialCapture = materialCapture * mainLightColor;
        #endif
    }
    
    // =====Emission=====
    float3 emission = 0.0;
    if(_UseEmission)
    {
        // ===EmissionBase===
        float3 emissionTexColor = UNITY_SAMPLE_TEX2D(_EmissionTex, i.emiuv.xy).rgb;
        
        // ===EmissionMask===
        float emissionMask = COMIC_SAMPLE_2D_ST(_EmissionMask, sampler_EmissionTex, i.uv.xy);

        // ===EmissionColor===
        emissionTexColor = saturate(emissionTexColor * emissionMask - 0.05);

        emissionTexColor *= _EmissionHDRColor.rgb * _EmissionHDRColor.a;

        // ===EmissionFlicker===
        float flicker = 0.0;
        if(_Flicker == 0)   flicker = 0.0;
        if(_Flicker == 1)   flicker = frac(_Time.y * _FlickerSpeed);
        if(_Flicker == 2)   flicker = abs(sin(_Time.y * _FlickerSpeed));

        emissionTexColor = emissionTexColor - emissionTexColor * flicker;
        
        #ifdef FB 
            emission = emissionTexColor * _EmissionPower;
        #endif

        #ifdef FA 
            // FAなら減衰率を乗算
            emission = emissionTexColor * _EmissionPower * atten;
        #endif
    }

    // =====DecalEmission=====

    float3 decalEmission = 0.0;
    if(_UseDecalEmission && _UseDecal)
    {
        // ===DecalEmissionColor===
        float3 decalEmissionColor = decalColor.rgb * decalColor.a * decalEdgeFlag;
        decalEmissionColor *= _DecalEmissionHDRColor;

        // ===EmissionFlicker===
        float flicker = 0.0;
        if(_DecalFlicker == 0)   flicker = 0.0;
        if(_DecalFlicker == 1)   flicker = frac(_Time.y * _FlickerSpeed);
        if(_DecalFlicker == 2)   flicker = abs(sin(_Time.y * _FlickerSpeed));

        decalEmissionColor = decalEmissionColor - decalEmissionColor * flicker;
        
        #ifdef FB
            decalEmission = decalEmissionColor * _DecalEmissionPower;
        #endif

        #ifdef FA
            decalEmission = decalEmissionColor * _DecalEmissionPower * atten;
        #endif
    }

    // =====合成=====

    // Metallic合成
    #ifdef FB
        if(_UseMetallic)
        {
            float metallicMask = COMIC_SAMPLE_2D_ST(_MetallicMask, sampler_MainTex, i.uv.xy);
            col.rgb = lerp(col.rgb, reflection * col.rgb, (_Metallic * metallicMask) + 0.000001);
        }
    #endif

    // スペキュラー合成
    col.rgb += specular;

    // MatCap合成
    col.rgb += materialCapture;

    // Emission合成
    col.rgb += emission;
    col.rgb += decalEmission;

    // 影優先モードで実行
    #ifdef FB
        if(_ShadowPriority)
        {
            col.rgb *= toonDiffuse;
        }
    #endif

    #ifdef FA 
        if(_ShadowPriority)
        {
            col.rgb *= toonDiffuse;
        }
    #endif

    // リムライト合成
    col.rgb += rimLight;

    lastColor.rgb = col.rgb;
    #if defined(TRANSPARENT) || defined(CUTOUT)
        lastColor.a = col.a;
    #endif

    return lastColor;
}
#endif