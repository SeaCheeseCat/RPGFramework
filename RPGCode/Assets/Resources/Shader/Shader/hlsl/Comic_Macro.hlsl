#ifndef COMIC_MACRO
#define COMIC_MACRO

// Texture
#define COMIC_SAMPLE_LEVEL(tex, uv, lod)     tex.SampleLevel(sampler_linear_repeat, uv, lod)
#define COMIC_SAMPLE_2D(tex, samp, uv)       tex.Sample(samp, uv)
#define COMIC_SAMPLE_2D_ST(tex, samp, uv)    tex.Sample(samp, uv*tex##_ST.xy+tex##_ST.zw)
#endif