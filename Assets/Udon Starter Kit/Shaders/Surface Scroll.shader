// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "akalink/Surface Scroll"
{
	Properties
	{
		_MainTex1("MainTex", 2D) = "white" {}
		_Color1("Color", Color) = (1,1,1,1)
		_BumpMap1("Normal Map", 2D) = "white" {}
		_MetaliicGlossMap1("MetalicSmoothness", 2D) = "white" {}
		_Smoothness1("Smoothness", Range( 0 , 1)) = 1
		_Metallic1("Metallic", Range( 0 , 1)) = 1
		_Emission1("Emission", 2D) = "black" {}
		_EmissionColor1("EmissionColor", Color) = (0,0,0,0)
		_Direction("Direction", Vector) = (0.5,0.5,0,0)
		_Tiling("Tiling", Vector) = (1,1,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _BumpMap1;
		uniform float2 _Direction;
		uniform float2 _Tiling;
		uniform float4 _Color1;
		uniform sampler2D _MainTex1;
		uniform sampler2D _Emission1;
		uniform float4 _EmissionColor1;
		uniform float _Metallic1;
		uniform sampler2D _MetaliicGlossMap1;
		uniform float _Smoothness1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord18 = i.uv_texcoord * _Tiling;
			float2 panner14 = ( _Time.y * _Direction + uv_TexCoord18);
			float2 UVPan15 = panner14;
			o.Normal = tex2D( _BumpMap1, UVPan15 ).rgb;
			o.Albedo = ( i.vertexColor * _Color1 * tex2D( _MainTex1, UVPan15 ) ).rgb;
			o.Emission = ( tex2D( _Emission1, UVPan15 ) * _EmissionColor1 ).rgb;
			float4 tex2DNode4 = tex2D( _MetaliicGlossMap1, UVPan15 );
			o.Metallic = ( _Metallic1 * tex2DNode4.r );
			o.Smoothness = ( tex2DNode4.a * _Smoothness1 );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
628;73;947;655;2203.095;694.8644;1;True;False
Node;AmplifyShaderEditor.Vector2Node;23;-1797.095,-539.8644;Inherit;False;Property;_Tiling;Tiling;9;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;19;-1631.936,-246.5016;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1557.936,-508.5016;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;20;-1571.936,-381.5016;Inherit;False;Property;_Direction;Direction;8;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;14;-1269.743,-360.1043;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;15;-1054.652,-345.6544;Inherit;False;UVPan;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;17;-880.6527,548.342;Inherit;False;15;UVPan;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;16;-1254.152,134.3437;Inherit;False;15;UVPan;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-613.5119,553.8563;Inherit;False;Property;_Smoothness1;Smoothness;4;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;7;-587.3735,-479.9406;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;-928.4966,344.3377;Inherit;False;Property;_EmissionColor1;EmissionColor;7;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-957.7391,139.3141;Inherit;True;Property;_Emission1;Emission;6;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-595.4785,292.6371;Inherit;False;Property;_Metallic1;Metallic;5;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-667.3765,361.7657;Inherit;True;Property;_MetaliicGlossMap1;MetalicSmoothness;3;0;Create;False;0;0;0;False;0;False;-1;None;d4537034aa11a154f9ff543dbc980080;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-743.9828,-138.3802;Inherit;True;Property;_MainTex1;MainTex;0;0;Create;True;0;0;0;False;0;False;4;None;2d2486c08055272419f3ad54cab9ef8f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-624.0753,-304.5626;Inherit;False;Property;_Color1;Color;1;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;21;-932.155,57.68726;Inherit;False;15;UVPan;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-203.3873,186.5819;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-513.074,245.0039;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-282.739,-123.8147;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-252.0478,334.5645;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-608.293,56.64155;Inherit;True;Property;_BumpMap1;Normal Map;2;0;Create;False;0;0;0;False;0;False;-1;None;bf29a12fbfb20954e9ab26a095930021;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;akalink/Surface Scroll;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;18;0;23;0
WireConnection;14;0;18;0
WireConnection;14;2;20;0
WireConnection;14;1;19;2
WireConnection;15;0;14;0
WireConnection;3;1;16;0
WireConnection;4;1;17;0
WireConnection;2;1;15;0
WireConnection;12;0;5;0
WireConnection;12;1;4;1
WireConnection;10;0;3;0
WireConnection;10;1;1;0
WireConnection;9;0;7;0
WireConnection;9;1;8;0
WireConnection;9;2;2;0
WireConnection;13;0;4;4
WireConnection;13;1;6;0
WireConnection;11;1;21;0
WireConnection;0;0;9;0
WireConnection;0;1;11;0
WireConnection;0;2;10;0
WireConnection;0;3;12;0
WireConnection;0;4;13;0
ASEEND*/
//CHKSM=50E0B10F08BF09306B0BAEDE4451B99490E99DEE