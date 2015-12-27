Shader "Unlit/Averaged Block"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Distortion ("Distortion Normal", 2D) = "bump" {}
		_Magnitude ("Distortion Magnitude", Range(0, 2)) = 1
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque"}
		LOD 100

		GrabPass { "_SharedGrabTexture" }

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

			struct v2f
			{
				float4 uv : TEXCOORD0;
				float2 bump_uv : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _SharedGrabTexture;
			sampler2D _MainTex;
			sampler2D _Distortion;
			float4 _Distortion_ST;
			float _Magnitude;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = o.vertex;
				#if UNITY_UV_STARTS_AT_TOP
				o.uv.y *= 1;
				#endif
				o.bump_uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				float4 projection = i.uv;
				projection.xy = projection.xy / projection.w;
				projection.xy += 1;
				projection.xy *= 0.5;
				projection.z = 0;
				projection.w = 20;
				float3 bump = UnpackNormal(tex2D(_Distortion, _Distortion_ST.xy * i.bump_uv + _Distortion_ST.zw)) * _Magnitude;
				projection.xy += bump.xy;
				fixed4 col = tex2Dlod(_MainTex, projection);
				return col;
			}
			ENDCG
		}
	}
}
