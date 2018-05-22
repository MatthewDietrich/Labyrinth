#version 130

uniform mat4 modelMatrix;
uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;

in vec3 vPosition;
in vec4 vColor;

out vec4 fColor;

void main()
{
	gl_Position = modelMatrix * viewMatrix * projectionMatrix * vec4(vPosition, 1.0);
	fColor = vColor;
}