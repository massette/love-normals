uniform Image u_normalMap;
uniform vec3 u_ambientColor = vec3(0.0, 0.0, 0.0);
uniform vec3 u_lightColor = vec3(1.0, 1.0, 1.0);
uniform vec3 u_lightCoords;

vec4 effect(vec4 textureColor, Image texture, vec2 textureCoords, vec2 screenCoords) {
    vec3 fragCoords = vec3(screenCoords, 0);

    // ambient
    const float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * u_ambientColor;

    // normals
    vec3 norm = Texel(u_normalMap, textureCoords).rgb;
    norm = normalize(norm * 2.0 - 1.0);

    // diffuse
    vec3 lightDir = normalize(u_lightCoords - fragCoords);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * u_lightColor;

    // specular
    const float specularStrength = 0.5;
    const vec3 viewCoords = vec3(0.0, 0.0, 3000.0);

    vec3 viewDir = normalize(viewCoords - fragCoords);
    vec3 reflectDir = reflect(-lightDir, norm);

    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * u_lightColor;

    // 
    vec3 res = (ambient + diffuse + specular) * Texel(texture, textureCoords).rgb;
    return vec4(res, textureColor.a);
}