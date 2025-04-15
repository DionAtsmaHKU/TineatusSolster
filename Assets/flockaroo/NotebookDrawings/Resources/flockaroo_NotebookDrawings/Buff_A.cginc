#ifdef SHADEROO
#define Res0 vec2(textureSize(iChannel0,0))
#define Res1 vec2(textureSize(iChannel1,0))
#else
#define Res0 tex1Resolution.xy
#define Res1 tex2Resolution.xy
#endif
#define Res  vec2(textureSize(iChannel1,0))
uniform float PencilSize;
vec4 getCol(vec2 pos)
{
    // take aspect ratio into account - min: src image always fills screen, max: src image is always fully visible
    vec2 uv = (pos-Res*.5)*min(Res0.x/Res.x,Res0.y/Res.y)/Res0.xy+.5;
    uv=clamp(uv,.5/Res,1.-.5/Res);
    // "texture" changed to texture2D for opengl 120
    vec4 c1=texture(iChannel0,uv);
    return c1;
    vec4 e=smoothstep(vec4i(-0.05),vec4i(-0.0),vec4(uv,vec2i(1)-uv));
    c1=mix(vec4(1,1,1,0),c1,e.x*e.y*e.z*e.w);
    float d=clamp(dot(c1.xyz,vec3(0,0,0)),0.0,1.0);
    vec4 c2=vec4i(.7);
    return min(mix(c1,c2,1.8*d),.7);
}

float getVal(vec2 pos)
{
    vec4 c=getCol(pos);
 	return pow(dot(c.xyz,vec3i(PencilSize)),1.0)*1.;
}

#define FASTGRAD

#ifdef FASTGRAD
vec2 getGrad(vec2 pos, float eps)
{
    //pos+=(getRand(pos*.0005).xy-.5)*StrokeSpread*Res.x/20.;
   	vec2 d=vec2(eps,0);
   	float v0=getVal(pos);
    return vec2(
        getVal(pos+d.xy)-v0,
        getVal(pos+d.yx)-v0
    )/eps;
}
#else
vec2 getGrad(vec2 pos, float eps)
{
    //pos+=(getRand(pos*.0005).xy-.5)*StrokeSpread*Res.x/20.;
   	vec2 d=vec2(eps,0);
    return vec2(
        getVal(pos+d.xy)-getVal(pos-d.xy),
        getVal(pos+d.yx)-getVal(pos-d.yx)
    )/eps/2.;
}
#endif

void mainImage( out vec4 fragColor, vec2 fragCoord )
{
    vec2 g=getGrad(fragCoord,1.);
    fragColor = vec4(g,0,1);
}

