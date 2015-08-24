// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7606,x:33061,y:32710,varname:node_7606,prsc:2|custl-5023-OUT,clip-2836-A;n:type:ShaderForge.SFN_Tex2d,id:2836,x:32211,y:32860,ptovrint:False,ptlb:BaseTexture,ptin:_BaseTexture,varname:_BaseTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-328-OUT;n:type:ShaderForge.SFN_Multiply,id:2257,x:32563,y:32838,varname:node_2257,prsc:2|A-6597-RGB,B-2836-RGB;n:type:ShaderForge.SFN_Tex2d,id:6597,x:32241,y:32533,ptovrint:False,ptlb:PaperTexture,ptin:_PaperTexture,varname:_PaperTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-328-OUT;n:type:ShaderForge.SFN_Color,id:3650,x:32508,y:33061,ptovrint:False,ptlb:FogColor,ptin:_FogColor,varname:_FogColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.665279,c2:0.7364948,c3:0.7867647,c4:1;n:type:ShaderForge.SFN_Slider,id:7614,x:33036,y:33398,ptovrint:False,ptlb:FogStrength,ptin:_FogStrength,varname:_FogStrength,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:5023,x:32769,y:33028,varname:node_5023,prsc:2|A-3650-RGB,B-2257-OUT,T-7901-OUT;n:type:ShaderForge.SFN_TexCoord,id:4193,x:31743,y:32673,varname:node_4193,prsc:2,uv:0;n:type:ShaderForge.SFN_ToggleProperty,id:4133,x:31640,y:32366,ptovrint:False,ptlb:flipUVs,ptin:_flipUVs,varname:_flipUVs,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Append,id:328,x:32041,y:32691,varname:node_328,prsc:2|A-7029-OUT,B-4193-V;n:type:ShaderForge.SFN_RemapRange,id:7370,x:31800,y:32449,varname:node_7370,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-4133-OUT;n:type:ShaderForge.SFN_Multiply,id:7029,x:31950,y:32577,varname:node_7029,prsc:2|A-7370-OUT,B-4193-U;n:type:ShaderForge.SFN_FragmentPosition,id:3741,x:31913,y:33115,varname:node_3741,prsc:2;n:type:ShaderForge.SFN_ViewPosition,id:5396,x:31947,y:33395,varname:node_5396,prsc:2;n:type:ShaderForge.SFN_Distance,id:757,x:32146,y:33256,varname:node_757,prsc:2|A-3741-XYZ,B-5396-XYZ;n:type:ShaderForge.SFN_Vector1,id:4465,x:32320,y:33372,cmnt:Fog End,varname:node_4465,prsc:2,v1:45;n:type:ShaderForge.SFN_Vector1,id:216,x:32382,y:33526,cmnt:Fog Start,varname:node_216,prsc:2,v1:33;n:type:ShaderForge.SFN_Subtract,id:144,x:32583,y:33344,varname:node_144,prsc:2|A-4465-OUT,B-757-OUT;n:type:ShaderForge.SFN_Subtract,id:731,x:32608,y:33470,varname:node_731,prsc:2|A-4465-OUT,B-216-OUT;n:type:ShaderForge.SFN_Divide,id:8470,x:32834,y:33426,varname:node_8470,prsc:2|A-144-OUT,B-731-OUT;n:type:ShaderForge.SFN_Clamp01,id:7901,x:32788,y:33272,varname:node_7901,prsc:2|IN-8470-OUT;proporder:2836-6597-3650-7614-4133;pass:END;sub:END;*/

Shader "SeaSerpent/Paper_environmentObject" {
    Properties {
        _BaseTexture ("BaseTexture", 2D) = "white" {}
        _PaperTexture ("PaperTexture", 2D) = "white" {}
        _FogColor ("FogColor", Color) = (0.665279,0.7364948,0.7867647,1)
        _FogStrength ("FogStrength", Range(0, 1)) = 0
        [MaterialToggle] _flipUVs ("flipUVs", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        
        
        _DropShadowColor ("DropShadowColor", Color) = (0,0,0,1)
        _dropShadowOpacity ("dropShadowOpacity", Range(0, 1)) = 0.3760684
        _fuzziness ("fuzziness", Range(0, 5)) = 0
        _DropShadow_offsetX ("DropShadow_offsetX", Range(-0.5, 0.5)) = 0
        _DropShadow_offsetY ("DropShadow_offsetY", Range(-0.5, 0.5)) = 0
        [MaterialToggle] _flipUVs ("flipUVs", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5        
                
    }
    
    
    SubShader {


        Pass {
            Name "FORWARD"
            Tags {
				"LightMode"="ForwardBase"
				"Queue"="AlphaTest"
				"RenderType"="TransparentCutout"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _BaseTexture; uniform float4 _BaseTexture_ST;
            uniform sampler2D _PaperTexture; uniform float4 _PaperTexture_ST;
            uniform float4 _FogColor;
            uniform fixed _flipUVs;
            
            
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
                float2 node_328 = float2(((_flipUVs*2.0+-1.0)*i.uv0.r),i.uv0.g);
                float4 _BaseTexture_var = tex2D(_BaseTexture,TRANSFORM_TEX(node_328, _BaseTexture));
                clip(_BaseTexture_var.a - 0.5);
                
				////// Lighting:
                float4 _PaperTexture_var = tex2D(_PaperTexture,TRANSFORM_TEX(node_328, _PaperTexture));
                
                float node_4465 = 50.0; // Fog End
                float3 finalColor = lerp(_FogColor.rgb,(_PaperTexture_var.rgb*_BaseTexture_var.rgb),saturate(((node_4465-distance(i.posWorld.rgb,_WorldSpaceCameraPos))/(node_4465-33.0))));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        
        
        
  Pass {
            Name "DropShadow"
            Tags {
                "LightMode"="ForwardBase"
                            "IgnoreProjector"="True"
            "Queue"="Transparent-20"
            "RenderType"="Transparent"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite off
            Offset 1, 100
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            
            
            uniform sampler2D _BaseTexture; uniform float4 _BaseTexture_ST;
            uniform float4 _FogColor;
            uniform float _FogStrength;
            uniform float4 _DropShadowColor;
            uniform float _dropShadowOpacity;
            uniform float _fuzziness;
            uniform float _DropShadow_offsetX;
            uniform float _DropShadow_offsetY;
            uniform fixed _flipUVs;
            
            
            
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
                v.vertex.xyz += float3(_DropShadow_offsetX,_DropShadow_offsetY,0.0);
                o.posWorld = mul(_Object2World, v.vertex);
                
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            
            
            
            
            
            float4 frag(VertexOutput i) : COLOR {

             //   float3 finalColor = lerp(_DropShadowColor.rgb,_FogColor.rgb,_FogStrength);
                
                float node_4465 = 50.0; // Fog End
              //  float3 finalColor = lerp(_FogColor.rgb,(_DropShadowColor.rgb),saturate(((node_4465-distance(i.posWorld.rgb,_WorldSpaceCameraPos))/(node_4465-33.0))));
                
                float3 finalColor = lerp(_FogColor.rgb,(_DropShadowColor.rgb), saturate(((node_4465 - distance(i.posWorld.rgb, _WorldSpaceCameraPos)) / (node_4465-33.0))));
                
                
                
                float2 node_294 = float2(((_flipUVs*2.0+-1.0)*i.uv0.r),i.uv0.g);
                float4 _BaseTexture_var = tex2Dlod(_BaseTexture,float4(TRANSFORM_TEX(node_294, _BaseTexture),0.0,_fuzziness));
                
                
                
                
                
                
                return fixed4(finalColor,(_BaseTexture_var.a*_dropShadowOpacity));
            }
            ENDCG
        }        
        
        
        
        
        
        
        
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
