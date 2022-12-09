 Shader "Custom/Window" {
     Properties {
         _Skybox ("Skybox Cubemap", Cube) = "grey" {}
     }
     
     SubShader {
         Tags { "RenderType"="Opaque" }
 
         CGPROGRAM
         #pragma surface surf Lambert noambient
         #pragma target 3.0
 
         samplerCUBE _Cube;
 
         struct Input {
             float3 viewDir;
         };
 
         void surf (Input IN, inout SurfaceOutput o) {
             o.Emission = texCUBE (_Cube, IN.viewDir);
         }
         ENDCG
     }
 }