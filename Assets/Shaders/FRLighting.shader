// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/FRLighting"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			

			#include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
             struct v2f{
                    float4 pos:SV_POSITION;
                    float2 uv:TEXCOORD0;
                    float4 screenPos:TEXCOORD1;
            };


			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;
			float MaxLightRadius;
			float SmallLightRadius;
			uniform sampler2D LowResDarkness;
			
            v2f vert (appdata v)
            {
                v2f o;
                
                o.pos = UnityObjectToClipPos(v.vertex.xyz);   
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

			
			fixed4 frag (v2f i) : SV_Target
			{
			

	          float2 scrPos = i.screenPos.xy ;
	          
			   float2 pt = float2(0.5 ,0.5);
			   
			   float aspect =  _ScreenParams.x / _ScreenParams.y;
               float2 ratio = float2(1, 1/ aspect);
               
			   float dist = length((pt - i.uv.xy) * ratio) ; 
			   
			   if(dist > (_SinTime.w / 100) + MaxLightRadius){
			        float4 blk = float4(0,0,0,0.8);
			   
			        return blk;
			   }
			   if(dist > (_SinTime.w / 100) + SmallLightRadius){
			   		float4 gr = float4(0,0,0,0.4);
			   
			        return gr;
			        
			   }
			   
			   return float4(1,1,1,1);


			}
			ENDCG
		}
		
       
	}
}
