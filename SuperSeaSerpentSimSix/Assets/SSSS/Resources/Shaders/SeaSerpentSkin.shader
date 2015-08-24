// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33592,y:32786,varname:node_3138,prsc:2|custl-5627-OUT;n:type:ShaderForge.SFN_Tex2d,id:6092,x:33266,y:32577,ptovrint:False,ptlb:DiffuseTexture,ptin:_DiffuseTexture,varname:_DiffuseTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5837,x:32944,y:33087,ptovrint:False,ptlb:PaperTexture,ptin:_PaperTexture,varname:_PaperTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8448-OUT;n:type:ShaderForge.SFN_ScreenPos,id:28,x:32231,y:33174,varname:node_28,prsc:2,sctp:0;n:type:ShaderForge.SFN_Multiply,id:8448,x:32687,y:33323,varname:node_8448,prsc:2|A-7874-OUT,B-5170-OUT;n:type:ShaderForge.SFN_Slider,id:5170,x:32357,y:33390,ptovrint:False,ptlb:paperTextureTiling,ptin:_paperTextureTiling,varname:_paperTextureTiling,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.5,max:2;n:type:ShaderForge.SFN_Multiply,id:5627,x:33373,y:33147,varname:node_5627,prsc:2|A-6092-RGB,B-5837-RGB,C-190-RGB;n:type:ShaderForge.SFN_LightVector,id:397,x:32109,y:33845,varname:node_397,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:599,x:32070,y:33675,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:1953,x:32361,y:33763,varname:node_1953,prsc:2,dt:0|A-599-OUT,B-397-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:2251,x:32423,y:33614,varname:node_2251,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5,x:32583,y:33742,varname:node_5,prsc:2|A-2251-OUT,B-1953-OUT;n:type:ShaderForge.SFN_Append,id:2371,x:32825,y:33783,varname:node_2371,prsc:2|A-5-OUT,B-5-OUT;n:type:ShaderForge.SFN_Tex2d,id:190,x:33139,y:33782,ptovrint:False,ptlb:LightingRamp,ptin:_LightingRamp,varname:_LightingRamp,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1d18eb23668704101969afc658edc54c,ntxv:0,isnm:False|UVIN-2371-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:6692,x:31350,y:32890,varname:node_6692,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:9137,x:31141,y:32592,varname:node_9137,prsc:2;n:type:ShaderForge.SFN_Transform,id:5867,x:31377,y:32607,varname:node_5867,prsc:2,tffrom:0,tfto:3|IN-9137-XYZ;n:type:ShaderForge.SFN_Transform,id:5370,x:31545,y:32914,varname:node_5370,prsc:2,tffrom:0,tfto:3|IN-6692-XYZ;n:type:ShaderForge.SFN_Add,id:7434,x:31596,y:32664,varname:node_7434,prsc:2|A-3294-OUT,B-5867-XYZ;n:type:ShaderForge.SFN_Append,id:3294,x:31451,y:32452,varname:node_3294,prsc:2|A-3363-OUT,B-3496-OUT;n:type:ShaderForge.SFN_Slider,id:3496,x:31007,y:32450,ptovrint:False,ptlb:VerticalOffset,ptin:_VerticalOffset,varname:_VerticalOffset,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:3;n:type:ShaderForge.SFN_Slider,id:3363,x:30984,y:32317,ptovrint:False,ptlb:HorizontalOffset,ptin:_HorizontalOffset,varname:_HorizontalOffset,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:3;n:type:ShaderForge.SFN_ComponentMask,id:948,x:31818,y:32743,varname:node_948,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7434-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4493,x:31757,y:32943,varname:node_4493,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5370-XYZ;n:type:ShaderForge.SFN_Distance,id:3420,x:32059,y:32743,varname:node_3420,prsc:2|A-948-R,B-4493-R;n:type:ShaderForge.SFN_Distance,id:3125,x:32076,y:32902,varname:node_3125,prsc:2|A-948-G,B-4493-G;n:type:ShaderForge.SFN_Append,id:7874,x:32303,y:32859,varname:node_7874,prsc:2|A-3420-OUT,B-3125-OUT;proporder:6092-5837-5170-190-3496-3363;pass:END;sub:END;*/

Shader "SeaSerpent/SeaSerpentSkin" {
    Properties {
        _DiffuseTexture ("DiffuseTexture", 2D) = "white" {}
        _PaperTexture ("PaperTexture", 2D) = "white" {}
        _paperTextureTiling ("paperTextureTiling", Range(0.1, 2)) = 0.5
        _LightingRamp ("LightingRamp", 2D) = "white" {}
        _VerticalOffset ("VerticalOffset", Range(0, 3)) = 0
        _HorizontalOffset ("HorizontalOffset", Range(0, 3)) = 0
        
        _DropShadowColor ("DropShadowColor", Color) = (0.5,0.5,0.5,1)
        _ShadowOfset ("ShadowOfset", Range(0, 0.5)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        
        
        
 Pass {
            Name "DropShadow"
            Tags {
                "LightMode"="ForwardBase"
                            "Queue"="Geometry+100"
            "RenderType"="Opaque"
            }
            ZTest Always
          Offset 1, 700            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _ShadowOfset;
            uniform float4 _DropShadowColor;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float2 node_4392 = mul( UNITY_MATRIX_V, float4(float3(-1,0.5,0),0) ).xyz.rgb.rg;
                v.vertex.xyz += mul( float4((float3(node_4392.r,node_4392.g,0.0)*_ShadowOfset),0), UNITY_MATRIX_MV ).xyz.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float3 emissive = _DropShadowColor.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        
        
        
        Pass {
            Name "Paper"
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
            uniform float _VerticalOffset;
            uniform float _HorizontalOffset;
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
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _DiffuseTexture_var = tex2D(_DiffuseTexture,TRANSFORM_TEX(i.uv0, _DiffuseTexture));
                float2 node_948 = (float3(float2(_HorizontalOffset,_VerticalOffset),0.0)+mul( UNITY_MATRIX_V, float4(objPos.rgb,0) ).xyz.rgb).rg;
                float2 node_4493 = mul( UNITY_MATRIX_V, float4(i.posWorld.rgb,0) ).xyz.rgb.rg;
                float2 node_8448 = (float2(distance(node_948.r,node_4493.r),distance(node_948.g,node_4493.g))*_paperTextureTiling);
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
