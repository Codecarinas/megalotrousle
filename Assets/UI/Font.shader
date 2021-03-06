Shader "GUI/Undertale Text Shader" {
	Properties {
		_MainTex ("Font Texture", 2D) = "white" {}
		_Color ("Text Color", Color) = (1,1,1,1)
		_JitterX ("Jitter X", Range(0, 1)) = 0
		_JitterY ("Jitter Y", Range(0, 1)) = 0
		_JitterThreshold ("Jitter Threshold", Range(0, 1)) = 0.001
	}

	SubShader {

		Tags {
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
		}
		Lighting Off Cull Off ZTest Always ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform fixed4 _Color;
			uniform float _JitterX;
			uniform float _JitterY;
			uniform float _JitterThreshold;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				o.screenPos.xy = o.screenPos.xy / o.screenPos.w;
				if (_JitterX > o.screenPos.x - _JitterThreshold && _JitterX < o.screenPos.x + _JitterThreshold) {
					o.vertex.x += (_JitterX * 2 - 1) / 16;
					o.vertex.y += (_JitterY * 2 - 1) / 16;
				}

				o.color = v.color * _Color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = i.color;
				col.a *= tex2D(_MainTex, i.texcoord).a;
				return col;
			}
			ENDCG
		}
	}
}
