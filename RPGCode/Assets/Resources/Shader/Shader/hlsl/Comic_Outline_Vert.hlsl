#ifndef COMIC_OUTLINE_VERT
#define COMIC_OUTLINE_VERT

v2f vert (appdata v)
{
    v2f o;
    
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    float4 outlineVertex = 0.0;

    // Use Outline
    if(_UseOutline)
    {
        float outlineMask = COMIC_SAMPLE_LEVEL(_OutlineMaskTex, o.uv, 0).r;
        float outlineWidth = _OutlineWidth * 0.05;

        float3 normalWS = normalize(UnityObjectToWorldNormal(v.normal));

        outlineWidth *= outlineMask;

        outlineVertex = float4(normalize(normalWS) * outlineWidth, 0.0);
    }
    
    o.pos = mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, v.vertex) + outlineVertex);

    // ====OutlineColor====
    o.color.rgb = _OutlineColor.rgb;

    #if defined(TRANSPARENT)
        o.color.a = _OutlineColor.a;
    #endif
    
    return o;
}

#endif