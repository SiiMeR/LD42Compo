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
			#pragma vertex vert_img
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

            uniform float4 Player;
            
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;
			float MaxLightRadius;
			float SmallLightRadius;
			uniform sampler2D LowResDarkness;
	/*		
            v2f vert (appdata v)
            {
                v2f o;
                
                o.pos = UnityObjectToClipPos(v.vertex.xyz);   
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }
*/
			
			fixed4 frag (v2f_img i) : COLOR
			{
			   float4 c = tex2D(_MainTex, i.uv);	   
			   float4 c2 = tex2D(LowResDarkness, i.uv);
			   float aspect =  _ScreenParams.x / _ScreenParams.y;
               float2 ratio = float2(1, 1/ aspect);
   
//			   float ray = length((Player.xy - i.uv.xy) *    ratio);
			   float ray = distance(Player.xy,i.uv.xy) * ratio;
			   
			   c.rgb *= smoothstep((_SinTime.w / 100) + Player.w, 0 ,ray) * 0.9;
			   c.rgb *= smoothstep((_SinTime.w / 100) + Player.z, 0 ,ray) ;
			   return c;


			}
			ENDCG
		}
		
       
	}
}
