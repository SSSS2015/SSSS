// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33439,y:32779,varname:node_3138,prsc:2|emission-5903-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:33069,y:33073,ptovrint:False,ptlb:SkyFog,ptin:_SkyFog,varname:_SkyFog,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9929,x:32614,y:32776,ptovrint:False,ptlb:SkyTexture,ptin:_SkyTexture,varname:_SkyTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1178,x:32607,y:33083,ptovrint:False,ptlb:PaperTexture,ptin:_PaperTexture,varname:_PaperTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2164-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3369,x:32929,y:32894,varname:node_3369,prsc:2|A-9929-RGB,B-1178-RGB;n:type:ShaderForge.SFN_UVTile,id:2384,x:33353,y:33367,varname:node_2384,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:2164,x:32411,y:33142,varname:node_2164,prsc:2,uv:1;n:type:ShaderForge.SFN_Add,id:6958,x:32905,y:33149,varname:node_6958,prsc:2;n:type:ShaderForge.SFN_Vector1,id:6423,x:32565,y:33364,varname:node_6423,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Multiply,id:6885,x:33036,y:33294,varname:node_6885,prsc:2;n:type:ShaderForge.SFN_Lerp,id:5903,x:33290,y:32999,varname:node_5903,prsc:2|A-3369-OUT,B-7241-RGB,T-2052-OUT;n:type:ShaderForge.SFN_Slider,id:2052,x:32812,y:33509,ptovrint:False,ptlb:Fogged amount,ptin:_Foggedamount,varname:_Foggedamount,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:7241-9929-1178-2052;pass:END;sub:END;*/

Shader "SeaSerpent/PaperSky" {
    Properties {
        _SkyFog ("SkyFog", Color) = (0.07843138,0.3921569,0.7843137,1)
        _SkyTexture ("SkyTexture", 2D) = "white" {}
        _PaperTexture ("PaperTexture", 2D) = "white" {}
        _Foggedamount ("Fogged amount", Range(0, 1)) = 0
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _SkyFog;
            uniform sampler2D _SkyTexture; uniform float4 _SkyTexture_ST;
            uniform sampler2D _PaperTexture; uniform float4 _PaperTexture_ST;
            uniform float _Foggedamount;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _SkyTexture_var = tex2D(_SkyTexture,TRANSFORM_TEX(i.uv0, _SkyTexture));
                float4 _PaperTexture_var = tex2D(_PaperTexture,TRANSFORM_TEX(i.uv1, _PaperTexture));
                float3 emissive = lerp((_SkyTexture_var.rgb*_PaperTexture_var.rgb),_SkyFog.rgb,_Foggedamount);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
