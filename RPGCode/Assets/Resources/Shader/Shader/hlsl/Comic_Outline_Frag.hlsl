#ifndef COMIC_OUTLINE_FRAG
#define COMIC_OUTLINE_FRAG

float4 frag(v2f i) : SV_Target
{
    float4 col = float4(0.0, 0.0, 0.0, 1.0);
    col.rgb = i.color.rgb;

    #if defined(TRANSPARENT)
        col.a = i.color.a;
    #endif

    if(!_UseOutline)
    {
        col -= 1.005;
        clip(col);
    }

    return float4(col);
}

#endif