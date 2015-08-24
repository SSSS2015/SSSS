// Shader created with Shader Forge v1.17 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.17;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33901,y:32551,varname:node_3138,prsc:2|custl-7692-OUT;n:type:ShaderForge.SFN_VertexColor,id:3653,x:32478,y:32632,varname:node_3653,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2408,x:33078,y:32383,varname:node_2408,prsc:2|A-3653-R,B-460-OUT,C-2683-OUT;n:type:ShaderForge.SFN_Slider,id:460,x:32689,y:32404,ptovrint:False,ptlb:Red Vertex Channel,ptin:_RedVertexChannel,varname:node_460,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.04273507,max:1;n:type:ShaderForge.SFN_Slider,id:8936,x:32736,y:32729,ptovrint:False,ptlb:Green Vertex Channel,ptin:_GreenVertexChannel,varname:_RedTest_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5769231,max:1;n:type:ShaderForge.SFN_Slider,id:7363,x:32678,y:33012,ptovrint:False,ptlb:Blue Vertex Channel,ptin:_BlueVertexChannel,varname:_GreenTest_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1991,x:32636,y:33368,ptovrint:False,ptlb:Alpha Vertex Channel,ptin:_AlphaVertexChannel,varname:_BlueTest_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:2628,x:33099,y:32680,varname:node_2628,prsc:2|A-3653-G,B-8936-OUT,C-6472-OUT;n:type:ShaderForge.SFN_Multiply,id:7905,x:33092,y:32967,varname:node_7905,prsc:2|A-3653-B,B-7363-OUT,C-2169-OUT;n:type:ShaderForge.SFN_Multiply,id:2303,x:33070,y:33318,varname:node_2303,prsc:2|A-3653-A,B-1991-OUT;n:type:ShaderForge.SFN_Add,id:7692,x:33587,y:32752,varname:node_7692,prsc:2|A-2408-OUT,B-2628-OUT,C-7905-OUT,D-2303-OUT;n:type:ShaderForge.SFN_Vector3,id:2683,x:33007,y:32560,varname:node_2683,prsc:2,v1:1,v2:0.8,v3:0.8;n:type:ShaderForge.SFN_Vector3,id:6472,x:32982,y:32834,varname:node_6472,prsc:2,v1:0.8,v2:1,v3:0.8;n:type:ShaderForge.SFN_Vector3,id:2169,x:32964,y:33114,varname:node_2169,prsc:2,v1:0.8,v2:0.8,v3:1;proporder:460-8936-7363-1991;pass:END;sub:END;*/

Shader "OQ/vertColor_display" {
    Properties {
        _RedVertexChannel ("Red Vertex Channel", Range(0, 1)) = 0.04273507
        _GreenVertexChannel ("Green Vertex Channel", Range(0, 1)) = 0.5769231
        _BlueVertexChannel ("Blue Vertex Channel", Range(0, 1)) = 0
        _AlphaVertexChannel ("Alpha Vertex Channel", Range(0, 1)) = 0
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _RedVertexChannel;
            uniform float _GreenVertexChannel;
            uniform float _BlueVertexChannel;
            uniform float _AlphaVertexChannel;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
				return fixed4(i.vertexColor.xyy, 1);

                //float3 finalColor = ((i.vertexColor.r*_RedVertexChannel*float3(1,0.8,0.8))+(i.vertexColor.g*_GreenVertexChannel*float3(0.8,1,0.8))+(i.vertexColor.b*_BlueVertexChannel*float3(0.8,0.8,1))+(i.vertexColor.a*_AlphaVertexChannel));
                //return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
