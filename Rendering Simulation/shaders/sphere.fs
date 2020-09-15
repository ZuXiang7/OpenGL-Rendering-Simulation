/* Start Header ------------------------------------------------------
Copyright (C) 2020 DigiPen Institute of Technology.
File Name: sphere.fs
Purpose: This file contains the implementation 
for calculating then fragment color of the reflection and refraction.
Language: C++ Visual Studio 2019 version 16.0.0
Platform: Microsoft Compiler Full Version 192027508, Windows 10
Project: zuxiang.seah_CS300_5
Author: Seah Zu Xiang, zuxiang.seah 390004318
Creation date: 21st August 2020
End Header -------------------------------------------------------*/
#version 330 core

uniform samplerCube texCube;

uniform mat4 viewMat;

uniform int sphereRef;

in vec3 view, normal;


out vec4 fragColor;


void main(void)
{

        vec3 reflected, refracted;
        mat3 invViewMat = transpose(mat3(viewMat));
        vec4 reflectedColor, refractedColor;

        if (sphereRef == 0 || sphereRef == 2)       //reflection color
        {
            reflected = reflect(normalize(view), normalize(normal));
            reflected = invViewMat * reflected;
            reflectedColor = texture(texCube, reflected);
        }
        if (sphereRef == 1 || sphereRef == 2)       //refraction color
        {
            refracted = refract(normalize(view), normalize(normal), 1.0 / 1.5);
            refracted = invViewMat * refracted;
            refractedColor = texture(texCube, refracted);
        }

       if (sphereRef == 0)   // reflection
            fragColor = reflectedColor;
       else
       if (sphereRef == 1)    // refraction
           fragColor = refractedColor;
       else                  // refraction and reflection
           fragColor = 0.7 * reflectedColor + 0.3 * refractedColor;
}