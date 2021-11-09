// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "akalink/Very Basic Water"
{
	Properties
	{
		_DiffuseColor("Diffuse Color", Color) = (0.2158686,0.5460202,0.7264151,0)
		_Normalmap1("Normal map 1", 2D) = "bump" {}
		_Normapmap2("Normap map 2", 2D) = "bump" {}
		_Normal1ScrollSpeed("Normal 1 Scroll Speed", Vector) = (1,1,0,0)
		_Normal1tile("Normal 1 tile", Vector) = (1,1,0,0)
		_Normal2ScollSpeed("Normal 2 Scoll Speed", Vector) = (-1,-1,0,0)
		_Normal2tile("Normal 2 tile", Vector) = (1,1,0,0)
		_Matcap("Matcap", 2D) = "white" {}
		_MatcapColor("Matcap Color", Color) = (0,0,0,0)
		_smoothness("smoothness", Range( 0 , 1)) = 1
		_metallic("metallic", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _Normalmap1;
		uniform float2 _Normal1ScrollSpeed;
		uniform float2 _Normal1tile;
		uniform sampler2D _Normapmap2;
		uniform float2 _Normal2ScollSpeed;
		uniform float2 _Normal2tile;
		uniform float4 _DiffuseColor;
		uniform float4 _MatcapColor;
		uniform sampler2D _Matcap;
		uniform float _metallic;
		uniform float _smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord16 = i.uv_texcoord * _Normal1tile;
			float2 panner14 = ( _Time.x * _Normal1ScrollSpeed + uv_TexCoord16);
			float2 uv_TexCoord23 = i.uv_texcoord * _Normal2tile;
			float2 panner22 = ( _Time.x * _Normal2ScollSpeed + uv_TexCoord23);
			float3 normalmap13 = ( UnpackNormal( tex2D( _Normalmap1, panner14 ) ) * UnpackNormal( tex2D( _Normapmap2, panner22 ) ) );
			o.Normal = normalmap13;
			float4 diffuse42 = _DiffuseColor;
			o.Albedo = diffuse42.rgb;
			float4 MatcapTexture11 = ( _MatcapColor * tex2D( _Matcap, ( ( mul( UNITY_MATRIX_V, float4( (WorldNormalVector( i , normalmap13 )) , 0.0 ) ).xyz * 0.5 ) + 0.5 ).xy ) );
			o.Emission = MatcapTexture11.rgb;
			o.Metallic = _metallic;
			o.Smoothness = _smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
729;73;664;655;658.3253;554.7725;1.741581;True;False
Node;AmplifyShaderEditor.CommentaryNode;35;-2698.725,-160.9125;Inherit;False;1452.68;995.4976;Comment;14;33;34;16;18;23;24;17;25;14;22;21;12;26;13;Normal maps;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;34;-2638.725,353.2298;Inherit;False;Property;_Normal2tile;Normal 2 tile;6;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;33;-2648.725,-105.7702;Inherit;False;Property;_Normal1tile;Normal 1 tile;4;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;17;-2449.507,64.87952;Inherit;False;Property;_Normal1ScrollSpeed;Normal 1 Scroll Speed;3;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;24;-2448.824,651.5851;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;25;-2456.941,489.4511;Inherit;False;Property;_Normal2ScollSpeed;Normal 2 Scoll Speed;5;0;Create;True;0;0;0;False;0;False;-1,-1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;23;-2461.14,358.7608;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-2443.516,-110.9125;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;18;-2445.909,195.9394;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;14;-2163.78,25.47543;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;22;-2188.755,478.6137;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;12;-1965.505,37.69645;Inherit;True;Property;_Normalmap1;Normal map 1;1;0;Create;True;0;0;0;False;0;False;-1;0fbb89d46a5e98945acb980cefa58b11;0fbb89d46a5e98945acb980cefa58b11;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;21;-1983.967,489.1643;Inherit;True;Property;_Normapmap2;Normap map 2;2;0;Create;True;0;0;0;False;0;False;-1;4a9954e9c65079549a3aca340260fb6e;4a9954e9c65079549a3aca340260fb6e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1640.457,363.1102;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;13;-1470.045,313.0897;Inherit;True;normalmap;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;8;-1494.159,-614.1763;Inherit;False;1656.782;381.8823;Comment;11;11;9;10;7;6;5;4;3;2;1;28;Matcap;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;28;-1473.324,-482.298;Inherit;False;13;normalmap;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;2;-1247.614,-470.0424;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewMatrixNode;1;-1167.426,-564.1763;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-1008.883,-534.3904;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-1019.701,-406.0156;Float;False;Constant;_Float0;Float 0;-1;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-841.6309,-497.9445;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-666.3215,-447.8677;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;7;-525.4762,-434.1513;Inherit;True;Property;_Matcap;Matcap;7;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-478.7986,-603.7198;Inherit;False;Property;_MatcapColor;Matcap Color;8;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-192.7985,-491.92;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;-36.19342,-479.0533;Inherit;False;MatcapTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;32;-589.6594,-135.4418;Inherit;False;Property;_DiffuseColor;Diffuse Color;0;0;Create;True;0;0;0;False;0;False;0.2158686,0.5460202,0.7264151,0;0.2158686,0.5460202,0.7264151,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;30;-351.4583,258.5372;Inherit;False;Property;_smoothness;smoothness;9;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-338.4583,171.5372;Inherit;False;Property;_metallic;metallic;10;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;29;-309.2943,96.07745;Inherit;False;11;MatcapTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;27;-317.7911,18.63715;Inherit;False;13;normalmap;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;42;-334.1174,-98.05741;Inherit;False;diffuse;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;akalink/Very Basic Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;0;34;0
WireConnection;16;0;33;0
WireConnection;14;0;16;0
WireConnection;14;2;17;0
WireConnection;14;1;18;1
WireConnection;22;0;23;0
WireConnection;22;2;25;0
WireConnection;22;1;24;1
WireConnection;12;1;14;0
WireConnection;21;1;22;0
WireConnection;26;0;12;0
WireConnection;26;1;21;0
WireConnection;13;0;26;0
WireConnection;2;0;28;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;5;0;4;0
WireConnection;5;1;3;0
WireConnection;6;0;5;0
WireConnection;6;1;3;0
WireConnection;7;1;6;0
WireConnection;9;0;10;0
WireConnection;9;1;7;0
WireConnection;11;0;9;0
WireConnection;42;0;32;0
WireConnection;0;0;42;0
WireConnection;0;1;27;0
WireConnection;0;2;29;0
WireConnection;0;3;31;0
WireConnection;0;4;30;0
ASEEND*/
//CHKSM=C4CFAE2E36B181F8C0101A99828588E4F013485A