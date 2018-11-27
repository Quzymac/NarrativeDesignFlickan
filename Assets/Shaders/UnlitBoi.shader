Shader "FilipShader/Outline/OutlineShader"
{
	Properties
	{
		_Color("Main Color", Color) = (0,0,0,1)
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineWidth("Outline Width", Range(1.0, 5.0)) = 1.01
	}
		CGINCLUDE
		#include "UnityCG.cginc"

			struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};
		struct v2f {
			float4 pos : POSITION;
			float3 normal : NORMAL;
		};
		float _OutlineWidth;
		float4 _OutlineColor;

		v2f vert(appdata v) 
		{
			v.vertex.xyz *= _OutlineWidth;

			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
		}
		ENDCG
	SubShader
	{
		Tags { "Queue" = "Transparent" }

		Pass
		{
			ZWrite off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : COLOR
			{
				return _OutlineColor;
			}
			ENDCG
		}
		Pass
		{
			ZWrite On

			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}
				Lighting On

			SetTexture[_MainTex]
			{
				ConstantColor[_Color]
			}
			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}
	}
			SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
				#pragma surface surf Standard fullforwardshadows
				#pragma target 3.0

				sampler2D _MainTex;

				struct Input {
					float2 uv_MainTex;
				};

				half _Glossiness;
				half _Metallic;
				fixed4 _Color;

				UNITY_INSTANCING_BUFFER_START(Props)
				UNITY_INSTANCING_BUFFER_END(Props)

				void surf(Input IN, inout SurfaceOutputStandard o) {
					fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					o.Albedo = c.rgb;

					o.Metallic = _Metallic;
					o.Smoothness = _Glossiness;
					o.Alpha = c.a;
				}
				ENDCG
			}
				FallBack "Diffuse"
}
