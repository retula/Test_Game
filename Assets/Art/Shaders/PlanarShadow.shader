Shader "Custom/PlanarShadow" 
{

	Properties {
		_ShadowColor ("Shadow Color", Color) = (0,0,0,1)
		_ShadowHeight ("planeHeight", Float) = 0
	}

	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		
		Pass {   
			
			ZWrite On
			ZTest LEqual 
			Blend SrcAlpha OneMinusSrcAlpha
			
			Stencil {
				Ref 0
				Comp Equal
				Pass IncrWrap
				ZFail Keep
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos	: SV_POSITION;
			};

			float4 _ShadowColor;
			float _ShadowHeight = 0;
			
			v2f vert( appdata_base v)
			{
				v2f o;
         	            
				float4 vPosWorld = mul( unity_ObjectToWorld, v.vertex);
				float4 lightDirection = -normalize(_WorldSpaceLightPos0);

				const float opposite = vPosWorld.y - _ShadowHeight;
				const float hypotenuse = opposite / -lightDirection.y;
				float3 vPos = vPosWorld.xyz + ( lightDirection * hypotenuse );

				o.pos = mul (UNITY_MATRIX_VP, float4(vPos.x, _ShadowHeight, vPos.z ,1));  

				return o;
			}


			fixed4 frag(v2f i) : COLOR
			{
				return _ShadowColor;
			}

			ENDCG
		}
	}
}
