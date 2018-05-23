#version 130

uniform mat4 modelViewProjectionMatrix;

in vec3 vPosition;
in vec4 vColor;

out vec4 fColor;

void main()
{
	gl_Position = modelViewProjectionMatrix * vec4(vPosition, 1.0);
	fColor = vColor;
}