// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33529,y:32624,varname:node_3138,prsc:2|emission-3498-OUT,alpha-1466-A,voffset-4274-OUT;n:type:ShaderForge.SFN_Tex2d,id:1466,x:32661,y:32591,ptovrint:False,ptlb:SunOutside,ptin:_SunOutside,varname:_SunOutside,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e5792ec5b364e42f985ed0d4a44925c1,ntxv:0,isnm:False|UVIN-6122-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9786,x:32740,y:32244,ptovrint:False,ptlb:SunInterior,ptin:_SunInterior,varname:_SunInterior,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b267b9f85d0664084a363061831f2876,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:3498,x:33257,y:32350,varname:node_3498,prsc:2|A-813-OUT,B-1466-RGB;n:type:ShaderForge.SFN_Multiply,id:813,x:33008,y:32314,varname:node_813,prsc:2|A-9786-RGB,B-9786-A;n:type:ShaderForge.SFN_TexCoord,id:8379,x:32150,y:32485,varname:node_8379,prsc:2,uv:0;n:type:ShaderForge.SFN_Rotator,id:6122,x:32419,y:32535,varname:node_6122,prsc:2|UVIN-8379-UVOUT,ANG-6008-OUT;n:type:ShaderForge.SFN_Time,id:7481,x:32054,y:32729,varname:node_7481,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4274,x:33344,y:33039,varname:node_4274,prsc:2|A-5276-OUT,B-6008-OUT;n:type:ShaderForge.SFN_Slider,id:2049,x:32948,y:33175,ptovrint:False,ptlb:node_2049,ptin:_node_2049,varname:_node_2049,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_FragmentPosition,id:2611,x:32509,y:32853,varname:node_2611,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:1244,x:32639,y:32936,varname:node_1244,prsc:2;n:type:ShaderForge.SFN_Subtract,id:5276,x:32959,y:32851,varname:node_5276,prsc:2|A-2611-XYZ,B-1244-XYZ;n:type:ShaderForge.SFN_Sin,id:6008,x:32294,y:33035,varname:node_6008,prsc:2|IN-7481-T;proporder:1466-9786-2049;pass:END;sub:END;*/

Shader "SeaSerpent/PaperSun" {
    Properties {
        _SunOutside ("SunOutside", 2D) = "white" {}
        _SunInterior ("SunInterior", 2D) = "white" {}
        _node_2049 ("node_2049", Range(0, 1)) = 0
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
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _SunOutside; uniform float4 _SunOutside_ST;
            uniform sampler2D _SunInterior; uniform float4 _SunInterior_ST;
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
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                float4 node_7481 = _Time + _TimeEditor;
                float node_6008 = sin(node_7481.g);
                v.vertex.xyz += ((mul(_Object2World, v.vertex).rgb-objPos.rgb)*node_6008);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _SunInterior_var = tex2D(_SunInterior,TRANSFORM_TEX(i.uv0, _SunInterior));
                float4 node_7481 = _Time + _TimeEditor;
                float node_6008 = sin(node_7481.g);
                float node_6122_ang = node_6008;
                float node_6122_spd = 1.0;
                float node_6122_cos = cos(node_6122_spd*node_6122_ang);
                float node_6122_sin = sin(node_6122_spd*node_6122_ang);
                float2 node_6122_piv = float2(0.5,0.5);
                float2 node_6122 = (mul(i.uv0-node_6122_piv,float2x2( node_6122_cos, -node_6122_sin, node_6122_sin, node_6122_cos))+node_6122_piv);
                float4 _SunOutside_var = tex2D(_SunOutside,TRANSFORM_TEX(node_6122, _SunOutside));
                float3 emissive = ((_SunInterior_var.rgb*_SunInterior_var.a)+_SunOutside_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_SunOutside_var.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                float4 node_7481 = _Time + _TimeEditor;
                float node_6008 = sin(node_7481.g);
                v.vertex.xyz += ((mul(_Object2World, v.vertex).rgb-objPos.rgb)*node_6008);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
/////// Vectors:
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
