#ifndef COMIC_FUNCTION
#define COMIC_FUNCTION

// 回転行列
float2x2 rotate(float angle)
{
    return float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
}

// ラインを書く
float MakeLine(float2 uv, uint lineNum, float lineWidth, float rot)
{
    float outCol = 0.0;
    // 0.017453 = PI / 180
    uv = mul(uv, rotate(rot * 0.017453));  
    float2 fruv = frac(uv * lineNum) * 2.0 - 1.0;
    outCol = step(0, abs(fruv.y) - (1.0 - lineWidth));
    return outCol;
}

// https://light11.hatenadiary.com/entry/2020/01/17/001035
float myPow(float bottom, float n)
{
    return bottom - (bottom - bottom * bottom) * -n;
}
#endif