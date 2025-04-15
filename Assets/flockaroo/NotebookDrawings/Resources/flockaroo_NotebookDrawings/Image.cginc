// ----------------------
// ---- shader begin ----
// ----------------------

//##pconf StrokeAngle min=0.0 max=6.4
//##pconf StrokeBend min=-1.0 max=1.0
//##pconf StrokeNum min=0.0 max=10.0
//##pconf StrokeOffs min=0.0 max=5.0

//#define ONE_STROKE_SIDE
#define USE_NOISE

#ifdef SHADEROO
#define Res0 vec2(textureSize(iChannel0,0))
#define Res1 vec2(textureSize(iChannel1,0))
#define Res2 vec2(textureSize(iChannel2,0))
#else
#define Res0 tex1Resolution.xy
#define Res1 tex2Resolution.xy
#define Res1 tex3Resolution.xy
#endif
#define Res  iResolution.xy
#define randSamp iChannel1
#define colorSamp iChannel0

//uniform float Alpha;
uniform float CamMotion;
uniform float PencilSize;
uniform float ColorIntensity;
uniform float ColorSpread;
uniform float StrokeNum;
//uniform float PaperMode;
uniform float Lines;
uniform float LinesVert;
uniform float LinesDist;
uniform float StrokeAngle;
uniform float StrokeBend;
uniform float StrokeOffs;
uniform float StrokeSpread;
uniform float MasterFade;
uniform float Vignette;
uniform float NumSamples;
uniform float Noise;
uniform vec3 PaperTint;
//uniform float DrawMode;  changed noise function
//uniform float NoiseSize;

    
vec4 getRandX(vec2 uv)
{
    uv *= 15.718281828459045;
    vec4 seeds = vec4(0.123, 0.456, 0.789, 0.);
    seeds = fract((uv.x + 0.5718281828459045 + seeds) * ((seeds + mod(uv.x, 0.141592653589793)) * 27.61803398875 + 4.718281828459045));
    seeds = fract((uv.y + 0.5718281828459045 + seeds) * ((seeds + mod(uv.y, 0.141592653589793)) * 27.61803398875 + 4.718281828459045));
    seeds = fract((.62 + 0.5718281828459045 + seeds) * ((seeds + mod(.48, 0.141592653589793)) * 27.61803398875 + 4.718281828459045));
    return vec4i(seeds);
}

//make smooth rand with 256 values between 0 and 1
vec4 getRand(vec2 uv)
{
    vec2 uvfl = floor(uv*256.)/256.;
    vec2 uvfr = fract(uv*256.);
    return mix(mix(getRandX(uvfl+vec2(0,0)/256.),
                   getRandX(uvfl+vec2(1,0)/256.),uvfr.x),
               mix(getRandX(uvfl+vec2(0,1)/256.),
                   getRandX(uvfl+vec2(1,1)/256.),uvfr.x),
               uvfr.y);
}

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

vec4 getColHT(vec2 pos)
{
    vec4 c=getCol(pos);
    vec4 c1 = mix(vec4i(1),c,ColorIntensity);
    #ifdef USE_NOISE
 	vec4 c2 = smoothstep(ColorIntensity-.05,ColorIntensity+.05,c+getRand(pos*0.4));
 	return mix(c1,c2,Noise);
 	#else
 	return c1;
 	#endif
}

float getVal(vec2 pos)
{
    vec4 c=getCol(pos);
 	return pow(dot(c.xyz,vec3i(PencilSize)),1.0)*1.;
}

vec2 getGrad(vec2 pos, float eps)
{
    pos+=(getRand(pos*.0005).xy-.5)*StrokeSpread*Res.x/20.;
    // use precalculated gradient
    vec2 g=texture(iChannel2,clamp(pos/Res,.5/Res2,1.-.5/Res2)).xy;
    if (isnan(abs(g.x)+abs(g.y))) return vec2i(.1);
    return g;

   	vec2 d=vec2(eps,0);
    return vec2(
        getVal(pos+d.xy)-getVal(pos-d.xy),
        getVal(pos+d.yx)-getVal(pos-d.yx)
    )/eps/2.;
}

#define AngleNum detail //changed to unifrom variable to allow for style variation

#define SampNum int(NumSamples)
#define PI2 6.28318530717959

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // took Alpha into bounce so that original image and effect image fit positionally
    // maybe should be called "Fade" then...!?
    vec2 pos = fragCoord+4.0*sin(iTime*1.*vec2(1,1.7))*iResolution.y/400.*CamMotion;
    vec2 pos0 = pos;
  vec3 col = vec3i(0);
  vec3 col2 = vec3i(0);
  float sum=0.;
  //implicit cast fixed
  for(int i=0;i<int(StrokeNum);i++){
    float ang=StrokeAngle+PI2/float(StrokeNum)*(float(i)+.8);
    vec2 v=vec2i(cos(ang),sin(ang));
    for(int j=0;j<16;j++){
        if (j>SampNum) break;
      vec2 dpos  = v.yx*vec2(1,-1)*float(j)*sqrt(16./float(SampNum))*iResolution.y/400.;
      vec2 dpos2 = v.xy*((float(j*j)/float(SampNum)*.5)*iResolution.y/400.*StrokeBend+StrokeOffs*2./**(rnd.x-.5)*/*iResolution.y/400.*10.);
	    vec2 g;
      float fact;
      float fact2;

    #ifdef ONE_STROKE_SIDE
      float s=-1.;
    #else
      for(float s=-1.;s<=1.;s+=2.)
    #endif
      {
        vec2 pos2=pos+s*dpos+dpos2;
        vec2 pos3=pos+(s*dpos+dpos2).yx*vec2(1,-1)*(.1+ColorSpread*2.);
        g=getGrad(pos2,.4);
        fact=dot(g,v)-.5*abs(dot(g,v.yx*vec2(1,-1)))/**(1.-getVal(pos2))*/;
        fact2=dot(normalize(g+vec2i(.0001)),v.yx*vec2(1,-1));

        fact=clamp(fact,0.,.05);
        fact2=abs(fact2);

        fact*=1.-float(j)/float(SampNum);
        col += fact;
        col2 += fact2*getColHT(pos3).xyz;
        sum+=fact2;
      }
    }
  }
  // implicit cast fixed
  col/=float(SampNum*int(StrokeNum))*.75/sqrt(iResolution.y);
  col2/=sum;
  col.x*=mix(1.,(.6+.8*getRand(pos*.7).x),Noise);
  col.x=1.-col.x;
  col.x*=col.x*col.x;

  vec2 s=sin(pos.xy/sqrt(iResolution.y * LinesDist));
  vec3 karo=vec3i(1);

  karo-=.5*Lines          *vec3(.25,.1,.1)*dot(exp(-s*s*80.),vec2(0,1));
  karo-=.5*Lines*LinesVert*vec3(.25,.1,.1)*dot(exp(-s*s*80.),vec2(1,0));
  
  float r=length(pos-iResolution.xy*.5)/iResolution.x;
  float vign=1.-Vignette*(r*r*r);
  //out vec4 fragColor has been changed to gl_FragColor

  vec4 outColor = vec4i(PaperTint*vec3i(col.x*col2*karo*vign),1);
  //vec4 orgColor = texture(colorSamp,fragCoord / iResolution.xy);
  vec4 orgColor = getCol(pos0);

  fragColor = outColor;
  //fragColor = mix(orgColor,outColor,Alpha);
  vec4 srcColor=texture(iChannel0,fragCoord/iResolution.xy);
  fragColor.w=srcColor.w;
  
  fragColor = mix(srcColor,fragColor,MasterFade);
	//gl_FragColor = vec4i(vec3i(col.x*col2*karo*vign),1);
  //fragColor=getCol(fragCoord);
}

// --------------------
// ---- shader end ----
// --------------------

