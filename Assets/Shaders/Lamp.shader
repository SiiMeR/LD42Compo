// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Lamp"
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
                    float3 worldPos: TEXCOORD2;
                    float4 vertex : TEXCOORD3;
                 
            };


			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;

			
            v2f vert (appdata v)
            {
                v2f o;
                
                o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex.xyz);   
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }
			
			fixed4 frag (v2f i) : SV_Target
			{
			    float3 pos = float3(0,0,0);
			    float dist = distance(i.worldPos, pos);
			  /*  float2 scrPos = i.screenPos.xy;
			   
			    float2 pt = float2(0.5,0.5);
			   
			    float dist = distance(pt,scrPos);
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
	
				return float4(dist.xxx,1);*/
				
				fixed4 col = fixed4(1,1,0,1);
				
				return dist * col;
			}
			ENDCG
		}
	}
}
