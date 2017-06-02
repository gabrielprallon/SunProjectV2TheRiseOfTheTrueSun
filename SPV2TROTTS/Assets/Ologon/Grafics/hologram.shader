// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33206,y:32715,varname:node_3138,prsc:2|emission-5118-OUT,alpha-2066-OUT;n:type:ShaderForge.SFN_Multiply,id:5118,x:32910,y:32796,varname:node_5118,prsc:2|A-2302-OUT,B-6193-OUT;n:type:ShaderForge.SFN_Add,id:6193,x:32774,y:33015,varname:node_6193,prsc:2|A-3735-OUT,B-2306-OUT;n:type:ShaderForge.SFN_Add,id:2302,x:32690,y:32743,varname:node_2302,prsc:2|A-3762-OUT,B-3735-OUT;n:type:ShaderForge.SFN_Multiply,id:3762,x:32473,y:32795,varname:node_3762,prsc:2|A-5646-OUT,B-4767-OUT;n:type:ShaderForge.SFN_Multiply,id:3735,x:32485,y:33009,varname:node_3735,prsc:2|A-7932-OUT,B-225-OUT;n:type:ShaderForge.SFN_Slider,id:2306,x:32655,y:33235,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_2306,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Slider,id:225,x:32314,y:33209,ptovrint:False,ptlb:ScanLine Opacity,ptin:_ScanLineOpacity,varname:node_225,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5330571,max:1;n:type:ShaderForge.SFN_ValueProperty,id:8462,x:32187,y:32966,ptovrint:False,ptlb:Noise Brightness,ptin:_NoiseBrightness,varname:node_8462,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:4767,x:32239,y:32733,varname:node_4767,prsc:2|A-8462-OUT,B-2974-OUT;n:type:ShaderForge.SFN_Lerp,id:2974,x:32041,y:32843,varname:node_2974,prsc:2|A-9312-OUT,B-527-OUT,T-5216-OUT;n:type:ShaderForge.SFN_Multiply,id:1232,x:32035,y:32639,varname:node_1232,prsc:2|A-7772-RGB,B-1903-OUT;n:type:ShaderForge.SFN_Vector1,id:9312,x:31810,y:32843,varname:node_9312,prsc:2,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:5216,x:31888,y:33059,ptovrint:False,ptlb:Fuzz Power,ptin:_FuzzPower,varname:node_5216,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:527,x:31795,y:32900,varname:node_527,prsc:2|A-3724-OUT,B-5216-OUT;n:type:ShaderForge.SFN_Power,id:7932,x:32092,y:33120,varname:node_7932,prsc:2|VAL-5995-OUT,EXP-5678-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5678,x:31856,y:33247,ptovrint:False,ptlb:ScanLine Exp,ptin:_ScanLineExp,varname:node_5678,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Color,id:7772,x:32035,y:32442,ptovrint:False,ptlb:Tilt Color,ptin:_TiltColor,varname:node_7772,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3,c2:0.8,c3:0.9,c4:1;n:type:ShaderForge.SFN_RemapRange,id:3724,x:31588,y:32882,varname:node_3724,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-7797-OUT;n:type:ShaderForge.SFN_Frac,id:5995,x:31718,y:33096,varname:node_5995,prsc:2|IN-4278-OUT;n:type:ShaderForge.SFN_Desaturate,id:1903,x:31795,y:32661,varname:node_1903,prsc:2|COL-4405-RGB,DES-3372-OUT;n:type:ShaderForge.SFN_Tex2d,id:4405,x:31528,y:32396,ptovrint:False,ptlb:Main texture,ptin:_Maintexture,varname:node_4405,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:489d2064ba9dae642a430fd5b549ad8d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Noise,id:7797,x:31380,y:32882,varname:node_7797,prsc:2|XY-4259-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4278,x:31495,y:33096,varname:node_4278,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-6792-OUT;n:type:ShaderForge.SFN_OneMinus,id:6792,x:31333,y:33119,varname:node_6792,prsc:2|IN-2348-OUT;n:type:ShaderForge.SFN_Add,id:2348,x:31154,y:33139,varname:node_2348,prsc:2|A-2079-OUT,B-8676-OUT;n:type:ShaderForge.SFN_Multiply,id:4259,x:31140,y:32845,varname:node_4259,prsc:2|A-1976-UVOUT,B-4075-TSL;n:type:ShaderForge.SFN_ScreenPos,id:1976,x:30925,y:32760,varname:node_1976,prsc:2,sctp:0;n:type:ShaderForge.SFN_Time,id:4075,x:30925,y:32932,varname:node_4075,prsc:2;n:type:ShaderForge.SFN_Slider,id:3372,x:31422,y:32695,ptovrint:False,ptlb:Desaturation Amount,ptin:_DesaturationAmount,varname:node_3372,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:2079,x:30878,y:33089,varname:node_2079,prsc:2|A-579-OUT,B-7419-OUT;n:type:ShaderForge.SFN_Append,id:579,x:30680,y:33017,varname:node_579,prsc:2|A-640-Y,B-640-Z;n:type:ShaderForge.SFN_FragmentPosition,id:640,x:30435,y:32916,varname:node_640,prsc:2;n:type:ShaderForge.SFN_Append,id:7419,x:30695,y:33177,varname:node_7419,prsc:2|A-1451-OUT,B-2447-OUT;n:type:ShaderForge.SFN_Vector1,id:1451,x:30491,y:33103,varname:node_1451,prsc:2,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:2447,x:30491,y:33211,ptovrint:False,ptlb:ScanLine Desnity,ptin:_ScanLineDesnity,varname:node_2447,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:8676,x:31036,y:33313,varname:node_8676,prsc:2|A-4608-OUT,B-9205-TSL;n:type:ShaderForge.SFN_Append,id:4608,x:30821,y:33331,varname:node_4608,prsc:2|A-2331-OUT,B-1325-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2331,x:30582,y:33331,ptovrint:False,ptlb:ScanLine Speed1,ptin:_ScanLineSpeed1,varname:node_2331,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_ValueProperty,id:1325,x:30644,y:33432,ptovrint:False,ptlb:ScanLine Speed,ptin:_ScanLineSpeed,varname:node_1325,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:9205,x:30836,y:33529,varname:node_9205,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5646,x:32364,y:32501,varname:node_5646,prsc:2|A-1232-OUT,B-7152-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7152,x:32209,y:32349,ptovrint:False,ptlb:Texture Brightness,ptin:_TextureBrightness,varname:node_7152,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:2066,x:32999,y:32538,varname:node_2066,prsc:2|A-8809-OUT,B-6193-OUT;n:type:ShaderForge.SFN_Blend,id:8809,x:32744,y:32326,varname:node_8809,prsc:2,blmd:10,clmp:True|SRC-6193-OUT,DST-4405-A;proporder:2306-225-8462-5216-5678-7772-4405-3372-2447-2331-1325-7152;pass:END;sub:END;*/

Shader "Shader Forge/hologram" {
    Properties {
        _Opacity ("Opacity", Range(0, 1)) = 0.5
        _ScanLineOpacity ("ScanLine Opacity", Range(0, 1)) = 0.5330571
        _NoiseBrightness ("Noise Brightness", Float ) = 2
        _FuzzPower ("Fuzz Power", Float ) = 0.5
        _ScanLineExp ("ScanLine Exp", Float ) = 10
        _TiltColor ("Tilt Color", Color) = (0.3,0.8,0.9,1)
        _Maintexture ("Main texture", 2D) = "white" {}
        _DesaturationAmount ("Desaturation Amount", Range(0, 1)) = 1
        _ScanLineDesnity ("ScanLine Desnity", Float ) = 1
        _ScanLineSpeed1 ("ScanLine Speed1", Float ) = 3
        _ScanLineSpeed ("ScanLine Speed", Float ) = 1
        _TextureBrightness ("Texture Brightness", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _Opacity;
            uniform float _ScanLineOpacity;
            uniform float _NoiseBrightness;
            uniform float _FuzzPower;
            uniform float _ScanLineExp;
            uniform float4 _TiltColor;
            uniform sampler2D _Maintexture; uniform float4 _Maintexture_ST;
            uniform float _DesaturationAmount;
            uniform float _ScanLineDesnity;
            uniform float _ScanLineSpeed1;
            uniform float _ScanLineSpeed;
            uniform float _TextureBrightness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
////// Lighting:
////// Emissive:
                float4 _Maintexture_var = tex2D(_Maintexture,TRANSFORM_TEX(i.uv0, _Maintexture));
                float4 node_4075 = _Time + _TimeEditor;
                float2 node_4259 = (i.screenPos.rg*node_4075.r);
                float2 node_7797_skew = node_4259 + 0.2127+node_4259.x*0.3713*node_4259.y;
                float2 node_7797_rnd = 4.789*sin(489.123*(node_7797_skew));
                float node_7797 = frac(node_7797_rnd.x*node_7797_rnd.y*(1+node_7797_skew.x));
                float4 node_9205 = _Time + _TimeEditor;
                float node_3735 = (pow(frac((1.0 - ((float2(i.posWorld.g,i.posWorld.b)*float2(1.0,_ScanLineDesnity))+(float2(_ScanLineSpeed1,_ScanLineSpeed)*node_9205.r))).r),_ScanLineExp)*_ScanLineOpacity);
                float node_6193 = (node_3735+_Opacity);
                float3 emissive = (((((_TiltColor.rgb*lerp(_Maintexture_var.rgb,dot(_Maintexture_var.rgb,float3(0.3,0.59,0.11)),_DesaturationAmount))*_TextureBrightness)*(_NoiseBrightness*lerp(1.0,((node_7797*2.0+-1.0)*_FuzzPower),_FuzzPower)))+node_3735)*node_6193);
                float3 finalColor = emissive;
                return fixed4(finalColor,(saturate(( _Maintexture_var.a > 0.5 ? (1.0-(1.0-2.0*(_Maintexture_var.a-0.5))*(1.0-node_6193)) : (2.0*_Maintexture_var.a*node_6193) ))+node_6193));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
