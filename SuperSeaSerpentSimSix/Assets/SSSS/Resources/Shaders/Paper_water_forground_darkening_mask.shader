// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:4,bdst:1,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:34164,y:32777,varname:node_3138,prsc:2|custl-241-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32492,y:32684,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:378,x:32393,y:33025,ptovrint:False,ptlb:Depth Gradient,ptin:_DepthGradient,varname:_DepthGradient,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9665-OUT;n:type:ShaderForge.SFN_Time,id:9276,x:30515,y:33468,varname:node_9276,prsc:2;n:type:ShaderForge.SFN_Slider,id:3939,x:30257,y:33204,ptovrint:False,ptlb:TimeScale,ptin:_TimeScale,varname:_TimeScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_TexCoord,id:136,x:31225,y:32688,varname:node_136,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:640,x:31561,y:32647,varname:node_640,prsc:2|A-136-U,B-4221-OUT;n:type:ShaderForge.SFN_Slider,id:4221,x:31147,y:32640,ptovrint:False,ptlb:UV_Tiling,ptin:_UV_Tiling,varname:_UV_Tiling,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:50;n:type:ShaderForge.SFN_Append,id:9665,x:31762,y:32716,varname:node_9665,prsc:2|A-640-OUT,B-136-V;n:type:ShaderForge.SFN_Append,id:3998,x:32586,y:34404,varname:node_3998,prsc:2|A-4799-OUT,B-2947-OUT,C-8976-OUT;n:type:ShaderForge.SFN_Vector1,id:4799,x:32067,y:34429,varname:node_4799,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8976,x:32109,y:34502,varname:node_8976,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:1914,x:31174,y:33776,varname:node_1914,prsc:2|A-3939-OUT,B-307-OUT,C-9276-T;n:type:ShaderForge.SFN_Vector1,id:307,x:30868,y:33875,varname:node_307,prsc:2,v1:3;n:type:ShaderForge.SFN_RemapRange,id:2947,x:31587,y:34339,varname:node_2947,prsc:2,frmn:-1,frmx:1,tomn:-0.2,tomx:0.2|IN-5664-OUT;n:type:ShaderForge.SFN_Sin,id:5664,x:31352,y:33892,varname:node_5664,prsc:2|IN-1914-OUT;n:type:ShaderForge.SFN_Add,id:241,x:33039,y:33103,varname:node_241,prsc:2|A-7241-RGB,B-378-RGB;proporder:7241-378-3939-4221;pass:END;sub:END;*/

Shader "SeaSerpent/PaperWater_foreground_darkening_mask" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _DepthGradient ("Depth Gradient", 2D) = "white" {}
        _TimeScale ("TimeScale", Range(0, 1)) = 1
        _UV_Tiling ("UV_Tiling", Range(0, 50)) = 1
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
            Blend DstColor Zero
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _DepthGradient; uniform float4 _DepthGradient_ST;
            uniform float _UV_Tiling;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
                float2 node_9665 = float2((i.uv0.r*_UV_Tiling),i.uv0.g);
                float4 _DepthGradient_var = tex2D(_DepthGradient,TRANSFORM_TEX(node_9665, _DepthGradient));
                float3 finalColor = (_Color.rgb+_DepthGradient_var.rgb);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
