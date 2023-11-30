﻿using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Kafka;

/// <summary>
/// Represents the object used to configure a Kafka server binding
/// </summary>
[DataContract]
public record KafkaServerBindingDefinition
    : KafkaBindingDefinition, IServerBindingDefinition
{



}
