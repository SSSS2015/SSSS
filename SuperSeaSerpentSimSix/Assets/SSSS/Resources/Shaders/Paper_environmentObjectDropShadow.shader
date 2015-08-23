// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:True,qofs:-20,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7606,x:33061,y:32710,varname:node_7606,prsc:2|custl-5023-OUT,alpha-9771-OUT;n:type:ShaderForge.SFN_Tex2d,id:2836,x:32385,y:33108,ptovrint:False,ptlb:BaseTexture,ptin:_BaseTexture,varname:_BaseTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|MIP-6465-OUT;n:type:ShaderForge.SFN_Color,id:3650,x:32131,y:32761,ptovrint:False,ptlb:FogColor,ptin:_FogColor,varname:_FogColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.665279,c2:0.7364948,c3:0.7867647,c4:1;n:type:ShaderForge.SFN_Slider,id:7614,x:32076,y:33001,ptovrint:False,ptlb:FogStrength,ptin:_FogStrength,varname:_FogStrength,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:5023,x:32395,y:32718,varname:node_5023,prsc:2|A-4447-RGB,B-3650-RGB,T-7614-OUT;n:type:ShaderForge.SFN_Color,id:4447,x:32022,y:32579,ptovrint:False,ptlb:DropShadowColor,ptin:_DropShadowColor,varname:_DropShadowColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:9771,x:32765,y:33209,varname:node_9771,prsc:2|A-2836-A,B-3759-OUT;n:type:ShaderForge.SFN_Slider,id:3759,x:32499,y:33295,ptovrint:False,ptlb:dropShadowOpacity,ptin:_dropShadowOpacity,varname:_dropShadowOpacity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3760684,max:1;n:type:ShaderForge.SFN_Vector1,id:8806,x:32123,y:33323,varname:node_8806,prsc:2,v1:2;n:type:ShaderForge.SFN_Slider,id:6465,x:32069,y:33388,ptovrint:False,ptlb:fuzziness,ptin:_fuzziness,varname:_fuzziness,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;proporder:2836-3650-7614-4447-3759-6465;pass:END;sub:END;*/

Shader "SeaSerpent/Paper_environmentObjectDropShadow" {
    Properties {
        _BaseTexture ("BaseTexture", 2D) = "white" {}
        _FogColor ("FogColor", Color) = (0.665279,0.7364948,0.7867647,1)
        _FogStrength ("FogStrength", Range(0, 1)) = 0
        _DropShadowColor ("DropShadowColor", Color) = (0,0,0,1)
        _dropShadowOpacity ("dropShadowOpacity", Range(0, 1)) = 0.3760684
        _fuzziness ("fuzziness", Range(0, 5)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent-20"
            "RenderType"="Transparent"
        }
        LOD 200
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
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform sampler2D _BaseTexture; uniform float4 _BaseTexture_ST;
            uniform float4 _FogColor;
            uniform float _FogStrength;
            uniform float4 _DropShadowColor;
            uniform float _dropShadowOpacity;
            uniform float _fuzziness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
                float3 finalColor = lerp(_DropShadowColor.rgb,_FogColor.rgb,_FogStrength);
                float4 _BaseTexture_var = tex2Dlod(_BaseTexture,float4(TRANSFORM_TEX(i.uv0, _BaseTexture),0.0,_fuzziness));
                fixed4 finalRGBA = fixed4(finalColor,(_BaseTexture_var.a*_dropShadowOpacity));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
