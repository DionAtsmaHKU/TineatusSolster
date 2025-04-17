Shader "Custom/DepthMaskShader" {

	Subshader{
		Tags {"Queue" = "Geometry+10"}

		ColorMask 0
		ZWrite On

		Pass{}
	}
}