﻿using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Redis;

/// <summary>
/// Represents the object used to configure a <see href="https://redis.io/">Redis</see> message binding
/// </summary>
[DataContract]
public record RedisMessageBindingDefinition
    : RedisBindingDefinition, IMessageBindingDefinition
{



}
