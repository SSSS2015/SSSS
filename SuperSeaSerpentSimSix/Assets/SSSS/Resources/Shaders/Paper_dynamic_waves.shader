// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:34762,y:32668,varname:node_3138,prsc:2|custl-5682-OUT,alpha-378-A;n:type:ShaderForge.SFN_Color,id:7241,x:32914,y:32622,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:378,x:32654,y:32787,ptovrint:False,ptlb:Texture_with_alpha,ptin:_Texture_with_alpha,varname:_Texture_with_alpha,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8698-UVOUT;n:type:ShaderForge.SFN_Time,id:9276,x:30034,y:32743,varname:node_9276,prsc:2;n:type:ShaderForge.SFN_Sin,id:8364,x:31622,y:32888,varname:node_8364,prsc:2|IN-9246-OUT;n:type:ShaderForge.SFN_Multiply,id:9246,x:30863,y:32856,varname:node_9246,prsc:2|A-1042-OUT,B-3939-OUT,C-8529-OUT;n:type:ShaderForge.SFN_Slider,id:3939,x:30331,y:33203,ptovrint:False,ptlb:TimeScale,ptin:_TimeScale,varname:_TimeScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Panner,id:8698,x:32069,y:32868,varname:node_8698,prsc:2,spu:0.1,spv:0|UVIN-9665-OUT,DIST-8364-OUT;n:type:ShaderForge.SFN_TexCoord,id:136,x:31403,y:32667,varname:node_136,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:1042,x:30435,y:32630,varname:node_1042,prsc:2|A-8750-OUT,B-9276-T;n:type:ShaderForge.SFN_Slider,id:8750,x:29894,y:32541,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:_Offset,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:474,x:33460,y:32852,varname:node_474,prsc:2|A-7241-RGB,B-378-RGB,C-1986-RGB,D-1381-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:9937,x:31982,y:35167,varname:node_9937,prsc:2;n:type:ShaderForge.SFN_ViewPosition,id:5309,x:31982,y:34996,varname:node_5309,prsc:2;n:type:ShaderForge.SFN_Distance,id:489,x:32378,y:35107,varname:node_489,prsc:2|A-5309-XYZ,B-9937-XYZ;n:type:ShaderForge.SFN_Subtract,id:8310,x:32760,y:34845,varname:node_8310,prsc:2|A-4483-OUT,B-489-OUT;n:type:ShaderForge.SFN_Subtract,id:6532,x:32839,y:35151,varname:node_6532,prsc:2|A-4483-OUT,B-4067-OUT;n:type:ShaderForge.SFN_Divide,id:436,x:33048,y:34828,varname:node_436,prsc:2|A-8310-OUT,B-6532-OUT;n:type:ShaderForge.SFN_Vector3,id:1617,x:33049,y:34461,cmnt:Fog Color,varname:node_1617,prsc:2,v1:0.7720588,v2:0.8962475,v3:1;n:type:ShaderForge.SFN_Lerp,id:3897,x:33505,y:34461,cmnt:Fog Lerp,varname:node_3897,prsc:2|A-1617-OUT,T-4280-OUT;n:type:ShaderForge.SFN_Clamp01,id:4280,x:33197,y:34687,varname:node_4280,prsc:2|IN-436-OUT;n:type:ShaderForge.SFN_Vector1,id:4067,x:32549,y:35057,cmnt:Fog Start,varname:node_4067,prsc:2,v1:26;n:type:ShaderForge.SFN_Vector1,id:4483,x:32549,y:34845,cmnt:Fog End,varname:node_4483,prsc:2,v1:37;n:type:ShaderForge.SFN_Multiply,id:640,x:31668,y:32486,varname:node_640,prsc:2|A-4221-OUT,B-136-U;n:type:ShaderForge.SFN_Slider,id:4221,x:31275,y:32514,ptovrint:False,ptlb:UV_Tiling,ptin:_UV_Tiling,varname:_UV_Tiling,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:200;n:type:ShaderForge.SFN_Append,id:9665,x:31770,y:32664,varname:node_9665,prsc:2|A-640-OUT,B-136-V;n:type:ShaderForge.SFN_Tex2d,id:1986,x:32670,y:33148,ptovrint:False,ptlb:PaperTexture,ptin:_PaperTexture,varname:_PaperTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8698-UVOUT;n:type:ShaderForge.SFN_FragmentPosition,id:7559,x:29766,y:33103,varname:node_7559,prsc:2;n:type:ShaderForge.SFN_Sin,id:6151,x:30080,y:33085,varname:node_6151,prsc:2|IN-8986-OUT;n:type:ShaderForge.SFN_RemapRange,id:8529,x:30457,y:32966,varname:node_8529,prsc:2,frmn:-1,frmx:1,tomn:0.95,tomx:1.05|IN-6151-OUT;n:type:ShaderForge.SFN_Multiply,id:8986,x:29924,y:33257,varname:node_8986,prsc:2|A-7559-X,B-5576-OUT;n:type:ShaderForge.SFN_Vector1,id:5576,x:29784,y:33420,varname:node_5576,prsc:2,v1:0.005;n:type:ShaderForge.SFN_VertexColor,id:2394,x:30784,y:33265,varname:node_2394,prsc:2;n:type:ShaderForge.SFN_Multiply,id:363,x:31573,y:33294,varname:node_363,prsc:2|A-67-OUT,B-4741-OUT;n:type:ShaderForge.SFN_Vector1,id:4741,x:31253,y:33595,varname:node_4741,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:67,x:31243,y:33226,varname:node_67,prsc:2|A-2968-OUT,B-1397-OUT;n:type:ShaderForge.SFN_Vector1,id:2968,x:30946,y:33211,varname:node_2968,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Append,id:1397,x:30974,y:33281,varname:node_1397,prsc:2|A-2394-R,B-2394-G,C-2394-B,D-2394-A;n:type:ShaderForge.SFN_Length,id:9896,x:32439,y:33536,varname:node_9896,prsc:2|IN-242-OUT;n:type:ShaderForge.SFN_ComponentMask,id:242,x:32050,y:33152,varname:node_242,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-363-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9886,x:32276,y:33903,varname:node_9886,prsc:2,cc1:2,cc2:3,cc3:-1,cc4:-1|IN-363-OUT;n:type:ShaderForge.SFN_Length,id:4371,x:32543,y:33879,varname:node_4371,prsc:2|IN-9886-OUT;n:type:ShaderForge.SFN_Add,id:1381,x:33132,y:33212,varname:node_1381,prsc:2|A-3565-OUT,B-1986-RGB;n:type:ShaderForge.SFN_RemapRange,id:6266,x:32693,y:33518,varname:node_6266,prsc:2,frmn:0,frmx:1,tomn:0.1,tomx:2.5|IN-9896-OUT;n:type:ShaderForge.SFN_Clamp01,id:3565,x:33047,y:33468,varname:node_3565,prsc:2|IN-6266-OUT;n:type:ShaderForge.SFN_Multiply,id:6873,x:32599,y:34160,varname:node_6873,prsc:2;n:type:ShaderForge.SFN_OneMinus,id:626,x:33063,y:33915,varname:node_626,prsc:2|IN-4371-OUT;n:type:ShaderForge.SFN_Blend,id:5682,x:33883,y:32971,varname:node_5682,prsc:2,blmd:2,clmp:True|SRC-295-OUT,DST-474-OUT;n:type:ShaderForge.SFN_RemapRange,id:295,x:33343,y:33936,varname:node_295,prsc:2,frmn:0,frmx:1,tomn:0.3,tomx:1|IN-626-OUT;proporder:7241-378-3939-8750-4221-1986;pass:END;sub:END;*/

Shader "SeaSerpent/PaperDynamictWaves" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _Texture_with_alpha ("Texture_with_alpha", 2D) = "white" {}
        _TimeScale ("TimeScale", Range(0, 2)) = 0
        _Offset ("Offset", Range(0, 1)) = 0
        _UV_Tiling ("UV_Tiling", Range(0, 200)) = 1
        _PaperTexture ("PaperTexture", 2D) = "white" {}
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
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _Texture_with_alpha; uniform float4 _Texture_with_alpha_ST;
            uniform float _TimeScale;
            uniform float _Offset;
            uniform float _UV_Tiling;
            uniform sampler2D _PaperTexture; uniform float4 _PaperTexture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
                float4 node_363 = (((-0.5)+float4(i.vertexColor.r,i.vertexColor.g,i.vertexColor.b,i.vertexColor.a))*2.0);
                float4 node_9276 = _Time + _TimeEditor;
                float2 node_8698 = (float2((_UV_Tiling*i.uv0.r),i.uv0.g)+sin(((_Offset+node_9276.g)*_TimeScale*(sin((i.posWorld.r*0.005))*0.04999998+1.0)))*float2(0.1,0));
                float4 _Texture_with_alpha_var = tex2D(_Texture_with_alpha,TRANSFORM_TEX(node_8698, _Texture_with_alpha));
                float4 _PaperTexture_var = tex2D(_PaperTexture,TRANSFORM_TEX(node_8698, _PaperTexture));
                float3 finalColor = saturate((1.0-((1.0-(_Color.rgb*_Texture_with_alpha_var.rgb*_PaperTexture_var.rgb*(saturate((length(node_363.rg)*2.4+0.1))+_PaperTexture_var.rgb)))/((1.0 - length(node_363.ba))*0.7+0.3))));
                return fixed4(finalColor,_Texture_with_alpha_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
