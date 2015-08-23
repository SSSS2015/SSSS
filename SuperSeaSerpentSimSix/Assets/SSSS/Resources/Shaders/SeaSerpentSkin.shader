// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33592,y:32786,varname:node_3138,prsc:2|custl-5627-OUT;n:type:ShaderForge.SFN_Tex2d,id:6092,x:32780,y:32584,ptovrint:False,ptlb:DiffuseTexture,ptin:_DiffuseTexture,varname:_DiffuseTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5837,x:32758,y:32908,ptovrint:False,ptlb:PaperTexture,ptin:_PaperTexture,varname:_PaperTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8448-OUT;n:type:ShaderForge.SFN_ScreenPos,id:28,x:32425,y:32918,varname:node_28,prsc:2,sctp:0;n:type:ShaderForge.SFN_Multiply,id:8448,x:32635,y:33121,varname:node_8448,prsc:2|A-28-UVOUT,B-5170-OUT;n:type:ShaderForge.SFN_Slider,id:5170,x:32305,y:33188,ptovrint:False,ptlb:paperTextureTiling,ptin:_paperTextureTiling,varname:_paperTextureTiling,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.5,cur:0.5,max:10;n:type:ShaderForge.SFN_Multiply,id:5627,x:33322,y:32945,varname:node_5627,prsc:2|A-6092-RGB,B-5837-RGB,C-190-RGB;n:type:ShaderForge.SFN_LightVector,id:397,x:32058,y:33642,varname:node_397,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:599,x:32019,y:33473,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:1953,x:32310,y:33561,varname:node_1953,prsc:2,dt:0|A-599-OUT,B-397-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:2251,x:32371,y:33411,varname:node_2251,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5,x:32531,y:33539,varname:node_5,prsc:2|A-2251-OUT,B-1953-OUT;n:type:ShaderForge.SFN_Append,id:2371,x:32773,y:33581,varname:node_2371,prsc:2|A-5-OUT,B-5-OUT;n:type:ShaderForge.SFN_Tex2d,id:190,x:33088,y:33580,ptovrint:False,ptlb:LightingRamp,ptin:_LightingRamp,varname:_LightingRamp,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1d18eb23668704101969afc658edc54c,ntxv:0,isnm:False|UVIN-2371-OUT;proporder:6092-5837-5170-190;pass:END;sub:END;*/

Shader "SeaSerpent/SeaSerpentSkin" {
    Properties {
        _DiffuseTexture ("DiffuseTexture", 2D) = "white" {}
        _PaperTexture ("PaperTexture", 2D) = "white" {}
        _paperTextureTiling ("paperTextureTiling", Range(0.5, 10)) = 0.5
        _LightingRamp ("LightingRamp", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _DiffuseTexture; uniform float4 _DiffuseTexture_ST;
            uniform sampler2D _PaperTexture; uniform float4 _PaperTexture_ST;
            uniform float _paperTextureTiling;
            uniform sampler2D _LightingRamp; uniform float4 _LightingRamp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _DiffuseTexture_var = tex2D(_DiffuseTexture,TRANSFORM_TEX(i.uv0, _DiffuseTexture));
                float2 node_8448 = (i.screenPos.rg*_paperTextureTiling);
                float4 _PaperTexture_var = tex2D(_PaperTexture,TRANSFORM_TEX(node_8448, _PaperTexture));
                float node_5 = (attenuation*dot(i.normalDir,lightDirection));
                float2 node_2371 = float2(node_5,node_5);
                float4 _LightingRamp_var = tex2D(_LightingRamp,TRANSFORM_TEX(node_2371, _LightingRamp));
                float3 finalColor = (_DiffuseTexture_var.rgb*_PaperTexture_var.rgb*_LightingRamp_var.rgb);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
