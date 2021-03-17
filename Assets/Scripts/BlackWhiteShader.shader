Shader "Custom/BlackWhiteShader"
{
    Properties
    {
		_MainTex("Texture",2D) = "white"{}
		_Colour("Colour",Color) = (1,1,1,1)
		_ScreenWidth("screen width", float) = 320.0
		_ScreenHeight("screen height", float) = 240.0
		_CellSizeX("size of x cell", float) = 4.0
		_CellSizeY("size of y cell", float) = 4.0
    }
    SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}


		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv:TEXCOORD0;
			};
			struct v2f
			{
				float4 vertex:SV_POSITION;
				float2 uv:TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			sampler2D _MainTex;
			float4 _Colour;
			float _ScreenWidth;
			float _ScreenHeight;
			float _CellSizeX;
			float _CellSizeY;
			float4 frag(v2f i):SV_Target
			{
				float2 uv = i.uv;

				float pixelX = _ScreenWidth / _CellSizeX;
				float pixelY = _ScreenHeight / _CellSizeY;

				float4 colourTMP = tex2D(_MainTex, float2(floor(pixelX * uv.x) / pixelX, floor(pixelY * uv.y) / pixelY));
				float lum = 0.3*colourTMP.r + 0.59*colourTMP.g + 0.11*colourTMP.b;
				lum = 1 - lum;
				float4 colour = float4(lum,lum,lum,colourTMP.a);
				return (colour * _Colour);
			}
			ENDCG
		}
	}
}
