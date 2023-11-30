﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Neuroglia.AsyncApi.FluentBuilders;
using Neuroglia.AsyncApi.Generation;
using Neuroglia.AsyncApi.IO;
using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.AsyncApi.Validation;
using Neuroglia.Serialization;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Defines extensions for <see cref="IServiceCollection"/>s
/// </summary>
public static class IServiceCollectionExtensions
{

    /// <summary>
    /// Adds and configures Async API services (<see cref="ISerializer"/>s, <see cref="IAsyncApiDocumentReader"/>, <see cref="IAsyncApiDocumentWriter"/>, ...)
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAsyncApi(this IServiceCollection services)
    {
        services.AddSerialization();
        services.AddJsonSerializer();
        services.AddYamlDotNetSerializer();
        services.TryAddSingleton<IAsyncApiDocumentReader, AsyncApiDocumentReader>();
        services.TryAddSingleton<IAsyncApiDocumentWriter, AsyncApiDocumentWriter>();
        services.TryAddTransient<IAsyncApiDocumentBuilder, AsyncApiDocumentBuilder>();
        services.AddValidatorsFromAssemblyContaining<AsyncApiDocumentValidator>(ServiceLifetime.Transient);
        return services;
    }

    /// <summary>
    /// Adds and configures code-first AsyncAPI document generation
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAsyncApiGeneration(this IServiceCollection services, Action<IAsyncApiGenerationOptionsBuilder> setup)
    {
        services.AddAsyncApi();
        services.Configure<AsyncApiGenerationOptions>(options =>
        {
            var builder = new AsyncApiGenerationOptionsBuilder(options);
            setup(builder);
        });

        services.TryAddTransient<IJsonSchemaExampleGenerator, JsonSchemaExampleGenerator>();
        services.TryAddTransient<IAsyncApiDocumentGenerator, AsyncApiDocumentGenerator>();

        services.TryAddSingleton<AsyncApiDocumentProvider>();
        services.TryAddSingleton<IAsyncApiDocumentProvider>(provider => provider.GetRequiredService<AsyncApiDocumentProvider>());
        services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<AsyncApiDocumentProvider>());

        return services;
    }
    
    /// <summary>
    /// Adds and configures a new <see cref="AsyncApiDocument"/>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <param name="document">The <see cref="AsyncApiDocument"/> to add</param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAsyncApiDocument(this IServiceCollection services, AsyncApiDocument document)
    {
        services.AddSingleton(document);

        return services;
    }

    /// <summary>
    /// Adds and configures a new <see cref="AsyncApiDocument"/>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="IAsyncApiDocumentBuilder"/> used to build the <see cref="AsyncApiDocument"/> to add</param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAsyncApiDocument(this IServiceCollection services, Action<IAsyncApiDocumentBuilder> setup)
    {
        services.AddSingleton(provider =>
        {
            var builder = provider.GetRequiredService<IAsyncApiDocumentBuilder>();
            setup(builder);
            return builder.Build();
        });

        return services;
    }

}
