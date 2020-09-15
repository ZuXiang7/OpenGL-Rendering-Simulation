/* Start Header ------------------------------------------------------
Copyright (C) 2020 DigiPen Institute of Technology.
File Name: main.fs
Purpose: This file contains the implementation
for computing final color for the vertex. It will also
recalculate the normal of the vertex if it's for lighting
and if it contains normal or parallax mapping. If parallax
mapping is on it will use a different UV to access the 
normal map for retrieving the normal of then vertex.
It's also using blinn phong lighting, which I already implemented
last assignment.
Language: C++ Visual Studio 2019 version 16.0.0
Platform: Microsoft Compiler Full Version 192027508, Windows 10
Project: zuxiang.seah_CS300_4
Author: Seah Zu Xiang, zuxiang.seah 390004318
Creation date: 5th August 2020
End Header -------------------------------------------------------*/
#version 330 core

uniform sampler2D colorTex;     /*  Base color texture */
uniform sampler2D normalTex;    /*  Normal texture for normal mapping */
uniform sampler2D bumpTex;      /*  Bump texture for bump mapping */

in vec2 uvCoord;

uniform bool lightOn;
uniform int numLights;
uniform vec4 ambient;
uniform vec4 diffuse;
uniform vec4 specular;
uniform int specularPower;

/*  These could be in view space or tangent space */
in vec3 lightDir[10];
in vec3 viewDir;
in vec3 normal;

uniform bool normalMappingOn;   /*  whether normal mapping is on */
uniform bool parallaxMappingOn; /*  whether parallax mapping is on */


out vec4 fragColor;


void main(void)
{
    if (!lightOn)
    {
        fragColor = vec4(texture(colorTex, uvCoord).rgb, 1.0);
        return;
    }
    
    vec3 nrmToUse;
    vec3 nrmalizedviewDir = normalize(viewDir);
    vec2 uvCoordToUse;

    if(parallaxMappingOn)
    {
        float h = texture(bumpTex,uvCoord).r * 0.15 - 0.005;
        uvCoordToUse = uvCoord + h * nrmalizedviewDir.xy; //new uv for parallax mapping
    }
    else
    {
        uvCoordToUse = uvCoord;
    }
    //use normal from normal mapping
    if(normalMappingOn)
    {
        nrmToUse = vec3(texture(normalTex,uvCoordToUse).rgb);
        nrmToUse = normalize(nrmToUse * 2.0 - 1.0);   
    }
    else
    {
        nrmToUse = normalize(normal); //use interpolated normal
    }

    fragColor = vec4(texture(colorTex, uvCoordToUse).rgb, 1.0);
    vec4 intensity = ambient;

    for (int i = 0; i < numLights; ++i)
    {
      vec3 lightDir_v = normalize(lightDir[i]);
      //get diffuse
      float diff = max(dot(nrmToUse, lightDir_v), 0.0);
      vec4 diffuse_result  = diffuse  * diff;
      vec4 specular_result = vec4(0.0);
      
      vec3 halfwayDir = normalize(lightDir_v + nrmalizedviewDir);  
      float spec = pow(max(dot(nrmToUse, halfwayDir), 0.0), specularPower);
      specular_result = specular * spec; 

      //get result of all lighting
      intensity += diffuse_result + specular_result;
    }
    //multiple base color with lighting
    fragColor = fragColor * intensity;
}