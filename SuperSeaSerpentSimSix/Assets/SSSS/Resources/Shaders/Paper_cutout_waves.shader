// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:34108,y:32888,varname:node_3138,prsc:2|custl-3897-OUT,alpha-378-A;n:type:ShaderForge.SFN_Color,id:7241,x:32585,y:32674,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:378,x:32396,y:32831,ptovrint:False,ptlb:Texture_with_alpha,ptin:_Texture_with_alpha,varname:_Texture_with_alpha,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8698-UVOUT;n:type:ShaderForge.SFN_Time,id:9276,x:30187,y:32783,varname:node_9276,prsc:2;n:type:ShaderForge.SFN_Sin,id:8364,x:30875,y:32877,varname:node_8364,prsc:2|IN-9246-OUT;n:type:ShaderForge.SFN_Multiply,id:9246,x:30613,y:32862,varname:node_9246,prsc:2|A-1042-OUT,B-3939-OUT;n:type:ShaderForge.SFN_Slider,id:3939,x:30243,y:33062,ptovrint:False,ptlb:TimeScale,ptin:_TimeScale,varname:_TimeScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Panner,id:8698,x:32162,y:32831,varname:node_8698,prsc:2,spu:0.1,spv:0|UVIN-9665-OUT,DIST-8364-OUT;n:type:ShaderForge.SFN_TexCoord,id:136,x:31403,y:32667,varname:node_136,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:1042,x:30582,y:32523,varname:node_1042,prsc:2|A-8750-OUT,B-9276-T;n:type:ShaderForge.SFN_Slider,id:8750,x:30138,y:32538,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:_Offset,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:474,x:32977,y:32861,varname:node_474,prsc:2|A-7241-RGB,B-378-RGB,C-1986-RGB;n:type:ShaderForge.SFN_FragmentPosition,id:9937,x:32092,y:33964,varname:node_9937,prsc:2;n:type:ShaderForge.SFN_ViewPosition,id:5309,x:32092,y:33793,varname:node_5309,prsc:2;n:type:ShaderForge.SFN_Distance,id:489,x:32488,y:33904,varname:node_489,prsc:2|A-5309-XYZ,B-9937-XYZ;n:type:ShaderForge.SFN_Subtract,id:8310,x:32870,y:33642,varname:node_8310,prsc:2|A-4483-OUT,B-489-OUT;n:type:ShaderForge.SFN_Subtract,id:6532,x:32949,y:33948,varname:node_6532,prsc:2|A-4483-OUT,B-4067-OUT;n:type:ShaderForge.SFN_Divide,id:436,x:33158,y:33625,varname:node_436,prsc:2|A-8310-OUT,B-6532-OUT;n:type:ShaderForge.SFN_Vector3,id:1617,x:33307,y:33258,cmnt:Fog Color,varname:node_1617,prsc:2,v1:0.7720588,v2:0.8962475,v3:1;n:type:ShaderForge.SFN_Lerp,id:3897,x:33583,y:33305,cmnt:Fog Lerp,varname:node_3897,prsc:2|A-1617-OUT,B-474-OUT,T-4280-OUT;n:type:ShaderForge.SFN_Clamp01,id:4280,x:33307,y:33484,varname:node_4280,prsc:2|IN-436-OUT;n:type:ShaderForge.SFN_Vector1,id:4067,x:32659,y:33854,cmnt:Fog Start,varname:node_4067,prsc:2,v1:30;n:type:ShaderForge.SFN_Vector1,id:4483,x:32659,y:33642,cmnt:Fog End,varname:node_4483,prsc:2,v1:40;n:type:ShaderForge.SFN_Multiply,id:640,x:31668,y:32486,varname:node_640,prsc:2|A-4221-OUT,B-136-U;n:type:ShaderForge.SFN_Slider,id:4221,x:31275,y:32514,ptovrint:False,ptlb:UV_Tiling,ptin:_UV_Tiling,varname:_UV_Tiling,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:200;n:type:ShaderForge.SFN_Append,id:9665,x:31878,y:32654,varname:node_9665,prsc:2|A-640-OUT,B-136-V;n:type:ShaderForge.SFN_Tex2d,id:1986,x:32505,y:33080,ptovrint:False,ptlb:PaperTexture,ptin:_PaperTexture,varname:_PaperTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8698-UVOUT;proporder:7241-378-3939-8750-4221-1986;pass:END;sub:END;*/

Shader "SeaSerpent/PaperCutout" {
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
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
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
                float4 node_9276 = _Time + _TimeEditor;
                float node_8364 = sin(((_Offset+node_9276.g)*_TimeScale));
                float2 node_8698 = (float2((_UV_Tiling*i.uv0.r),i.uv0.g)+node_8364*float2(0.1,0));
                float4 _Texture_with_alpha_var = tex2D(_Texture_with_alpha,TRANSFORM_TEX(node_8698, _Texture_with_alpha));
                float4 _PaperTexture_var = tex2D(_PaperTexture,TRANSFORM_TEX(node_8698, _PaperTexture));
                float node_4483 = 40.0; // Fog End
                float3 finalColor = lerp(float3(0.7720588,0.8962475,1),(_Color.rgb*_Texture_with_alpha_var.rgb*_PaperTexture_var.rgb),saturate(((node_4483-distance(_WorldSpaceCameraPos,i.posWorld.rgb))/(node_4483-30.0))));
                return fixed4(finalColor,_Texture_with_alpha_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
