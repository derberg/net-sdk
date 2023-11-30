﻿namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Represents an object used to define a security scheme
/// </summary>
[DataContract]
public record SecuritySchemeDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> containing additional external documentation.
    /// </summary>
    [DataMember(Order = 1, Name = "type"), JsonPropertyOrder(1), JsonPropertyName("type"), YamlMember(Order = 1, Alias = "type")]
    public virtual SecuritySchemeType Type { get; set; }

    /// <summary>
    /// Gets/sets the name of the header, query or cookie parameter to be used.
    /// </summary>
    [DataMember(Order = 2, Name = "name"), JsonPropertyOrder(2), JsonPropertyName("name"), YamlMember(Order = 2, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets a short description of the security scheme. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 3, Name = "description"), JsonPropertyOrder(3), JsonPropertyName("description"), YamlMember(Order = 3, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets the location of the API key. Valid values are "user" and "password" for <see cref="SecuritySchemeType.ApiKey"/> and "query", "header" or "cookie" for <see cref="SecuritySchemeType.HttpApiKey"/>.
    /// </summary>
    [DataMember(Order = 4, Name = "in"), JsonPropertyOrder(4), JsonPropertyName("in"), YamlMember(Order = 4, Alias = "in")]
    public virtual string? In { get; set; }

    /// <summary>
    /// Gets/sets the name of the HTTP Authorization scheme to be used in the Authorization header as defined in RFC7235.
    /// </summary>
    [DataMember(Order = 5, Name = "scheme"), JsonPropertyOrder(5), JsonPropertyName("scheme"), YamlMember(Order = 5, Alias = "scheme")]
    public virtual string? Scheme { get; set; }

    /// <summary>
    /// Gets/sets an object containing configuration information for the flow types supported.
    /// </summary>
    [DataMember(Order = 6, Name = "bearerFormat"), JsonPropertyOrder(6), JsonPropertyName("bearerFormat"), YamlMember(Order = 6, Alias = "bearerFormat")]
    public virtual string? BearerFormat { get; set; }

    /// <summary>
    /// Gets/sets an object containing configuration information for the flow types supported.
    /// </summary>
    [DataMember(Order = 7, Name = "flows"), JsonPropertyOrder(7), JsonPropertyName("flows"), YamlMember(Order = 7, Alias = "flows")]
    public virtual OAuthFlowDefinitionCollection? Flows { get; set; }

    /// <summary>
    /// Gets/sets the OpenId Connect <see cref="Uri"/> to discover OAuth2 configuration values.
    /// </summary>
    [DataMember(Order = 8, Name = "openIdConnectUrl"), JsonPropertyOrder(8), JsonPropertyName("openIdConnectUrl"), YamlMember(Order = 8, Alias = "openIdConnectUrl")]
    public virtual Uri? OpenIdConnectUrl { get; set; }

    /// <inheritdoc/>
    public override string ToString() => EnumHelper.Stringify(Type);

}
