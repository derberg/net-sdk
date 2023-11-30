﻿namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Amqp;

/// <summary>
/// Represents the object used to configure an AMQP queue based channel
/// </summary>
[DataContract]
public record AmqpQueueDefinition
{

    /// <summary>
    /// Gets/sets the name of the queue. It MUST NOT exceed 255 characters long.
    /// </summary>
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the queue should survive broker restarts or not.
    /// </summary>
    [DataMember(Order = 2, Name = "durable"), JsonPropertyOrder(2), JsonPropertyName("durable"), YamlMember(Order = 2, Alias = "durable")]
    public virtual bool Durable { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the queue should be used only by one connection or not.
    /// </summary>
    [DataMember(Order = 3, Name = "exclusive"), JsonPropertyOrder(3), JsonPropertyName("exclusive"), YamlMember(Order = 3, Alias = "exclusive")]
    public virtual bool Exclusive { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the queue should be deleted when the last queue is unbound from it.
    /// </summary>
    [DataMember(Order = 4, Name = "autoDelete"), JsonPropertyOrder(4), JsonPropertyName("autoDelete"), YamlMember(Order = 4, Alias = "autoDelete")]
    public virtual bool AutoDelete { get; set; }

    /// <summary>
    /// Gets/sets the virtual host of the queue. Defaults to '/'.
    /// </summary>
    [DataMember(Order = 5, Name = "vhost"), JsonPropertyOrder(5), JsonPropertyName("vhost"), YamlMember(Order = 5, Alias = "vhost")]
    public virtual string VirtualHost { get; set; } = "/";

    /// <inheritdoc/>
    public override string? ToString() => Name;

}
