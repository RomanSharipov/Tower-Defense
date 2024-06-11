Shader "Shader /Sample Texture 2D"
{
Properties
{
[NoScaleOffset]_Palette("Palette", 2D) = "white" {}
_Color("Color", Color) = (0, 0, 0, 0)
_Metallic("Metallic", Float) = 0
_Smoothness("Smoothness", Float) = 0.25
[NoScaleOffset]_Emmision_Map("Emmision Map", 2D) = "white" {}
[HDR]_EmmisionColor("EmmisionColor", Color) = (2, 2, 2, 0)
}
SubShader
{
Tags
{
// RenderPipeline: <None>
"RenderType"="Opaque"
"Queue"="Geometry"
// DisableBatching: <None>
"ShaderGraphShader"="true"
}
Pass
{
    // Name: <None>
    Tags
    {
        // LightMode: <None>
    }

    // Render State
    // RenderState: <None>

    // Debug
    // <None>

    // --------------------------------------------------
    // Pass

    HLSLPROGRAM

    // Pragmas
    #pragma vertex vert
#pragma fragment frag

    // Keywords
    // PassKeywords: <None>
    // GraphKeywords: <None>

    // Defines
    #define ATTRIBUTES_NEED_TEXCOORD0
    #define VARYINGS_NEED_TEXCOORD0
    /* WARNING: $splice Could not find named fragment 'PassInstancing' */
    #define SHADERPASS SHADERPASS_PREVIEW
#define SHADERGRAPH_PREVIEW 1

    // Includes
    /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */

    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Packing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"
#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariables.hlsl"
#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"

    // --------------------------------------------------
    // Structs and Packing

    /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

    struct Attributes
{
 float3 positionOS : POSITION;
 float4 uv0 : TEXCOORD0;
#if UNITY_ANY_INSTANCING_ENABLED
 uint instanceID : INSTANCEID_SEMANTIC;
#endif
};
struct Varyings
{
 float4 positionCS : SV_POSITION;
 float4 texCoord0;
#if UNITY_ANY_INSTANCING_ENABLED
 uint instanceID : CUSTOM_INSTANCE_ID;
#endif
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
 FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
#endif
};
struct SurfaceDescriptionInputs
{
 float4 uv0;
};
struct VertexDescriptionInputs
{
};
struct PackedVaryings
{
 float4 positionCS : SV_POSITION;
 float4 texCoord0 : INTERP0;
#if UNITY_ANY_INSTANCING_ENABLED
 uint instanceID : CUSTOM_INSTANCE_ID;
#endif
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
 FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
#endif
};

    PackedVaryings PackVaryings (Varyings input)
{
PackedVaryings output;
ZERO_INITIALIZE(PackedVaryings, output);
output.positionCS = input.positionCS;
output.texCoord0.xyzw = input.texCoord0;
#if UNITY_ANY_INSTANCING_ENABLED
output.instanceID = input.instanceID;
#endif
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
output.cullFace = input.cullFace;
#endif
return output;
}

Varyings UnpackVaryings (PackedVaryings input)
{
Varyings output;
output.positionCS = input.positionCS;
output.texCoord0 = input.texCoord0.xyzw;
#if UNITY_ANY_INSTANCING_ENABLED
output.instanceID = input.instanceID;
#endif
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
output.cullFace = input.cullFace;
#endif
return output;
}


    // --------------------------------------------------
    // Graph

    // Graph Properties
    CBUFFER_START(UnityPerMaterial)
float _Metallic;
float _Smoothness;
float4 _Emmision_Map_TexelSize;
float4 _Palette_TexelSize;
float4 _Color;
float4 _EmmisionColor;
CBUFFER_END


// Object and Global properties
SAMPLER(SamplerState_Linear_Repeat);
TEXTURE2D(_Emmision_Map);
SAMPLER(sampler_Emmision_Map);
TEXTURE2D(_Palette);
SAMPLER(sampler_Palette);

    // Graph Includes
    // GraphIncludes: <None>

    // -- Property used by ScenePickingPass
    #ifdef SCENEPICKINGPASS
    float4 _SelectionID;
    #endif

    // -- Properties used by SceneSelectionPass
    #ifdef SCENESELECTIONPASS
    int _ObjectId;
    int _PassValue;
    #endif

    // Graph Functions
    // GraphFunctions: <None>

    /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

    // Graph Vertex
    // GraphVertex: <None>

    /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreSurface' */

    // Graph Pixel
    struct SurfaceDescription
{
float4 Out;
};

SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
{
SurfaceDescription surface = (SurfaceDescription)0;
UnityTexture2D _Property_303db4adaf0a434ca670c7c725cb953a_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_Palette);
float4 _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_303db4adaf0a434ca670c7c725cb953a_Out_0_Texture2D.tex, _Property_303db4adaf0a434ca670c7c725cb953a_Out_0_Texture2D.samplerstate, _Property_303db4adaf0a434ca670c7c725cb953a_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
float _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_R_4_Float = _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4.r;
float _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_G_5_Float = _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4.g;
float _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_B_6_Float = _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4.b;
float _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_A_7_Float = _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4.a;
surface.Out = all(isfinite(_SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4)) ? half4(_SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4.x, _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4.y, _SampleTexture2D_bc36cb12861442bab90444305c2fd6c5_RGBA_0_Vector4.z, 1.0) : float4(1.0f, 0.0f, 1.0f, 1.0f);
return surface;
}

    // --------------------------------------------------
    // Build Graph Inputs

    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
{
    SurfaceDescriptionInputs output;
    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

    /* WARNING: $splice Could not find named fragment 'CustomInterpolatorCopyToSDI' */






    #if UNITY_UV_STARTS_AT_TOP
    #else
    #endif


    output.uv0 =                                        input.texCoord0;
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
#else
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
#endif
#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

    return output;
}

    // --------------------------------------------------
    // Main

    #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/PreviewVaryings.hlsl"
#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/PreviewPass.hlsl"

    ENDHLSL
}
}
CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
FallBack "Hidden/Shader Graph/FallbackError"
}