﻿// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v3;

namespace StreetLightsApi.Server.Services;

/// <summary>
/// Represents the Smartylighting Streetlights API, which allows to remotely manage the city lights
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="serializer">The service used to serialize/deserialize data to/from JSON</param>
[AsyncApiV2("Streetlights API", "1.0.0", Description = "The Smartylighting Streetlights API allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
[AsyncApiV3("Streetlights API", "1.0.0", Description = "The **Smartylighting Streetlights API** allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
[ChannelV3("lightingMeasuredMQTT", Address = "streets.{streetName}", Description = "This channel is used to exchange messages about lightning measurements.", Servers = ["#/servers/mosquitto"])]
[ChannelParameterV3("lightingMeasured", "streetName", Description = "The name of the **street** the lights to get measurements for are located in")]
public class StreetLightsService(ILogger<StreetLightsService> logger, IJsonSerializer serializer)
    : BackgroundService
{

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from JSON
    /// </summary>
    protected IJsonSerializer Serializer { get; } = serializer;

    /// <summary>
    /// Gets the service used to interact with an MQTT server
    /// </summary>
    protected IMqttClient MqttClient { get; private set; } = null!;

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.MqttClient = new MqttFactory().CreateMqttClient();
        var options = new MqttClientOptions() { ChannelOptions = new MqttClientTcpOptions() { Server = "test.mosquitto.org" } };
        await this.MqttClient.ConnectAsync(options, stoppingToken).ConfigureAwait(false);
        stoppingToken.Register(async () => await this.MqttClient.DisconnectAsync().ConfigureAwait(false));
        this.MqttClient.ApplicationMessageReceivedAsync += (async message =>
        {
            var e = this.Serializer.Deserialize<LightMeasuredEvent>(Encoding.UTF8.GetString(message.ApplicationMessage.PayloadSegment))!;
            await this.OnLightMeasured(e);
            await message.AcknowledgeAsync(stoppingToken);
        });
        await this.MqttClient.SubscribeAsync("onLightMeasured", cancellationToken: stoppingToken).ConfigureAwait(false);
        await this.PublishLightMeasured(new() { Id = Guid.NewGuid(), Lumens = 5, SentAt = DateTime.UtcNow }, stoppingToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Publishes the specified <see cref="LightMeasuredEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="LightMeasuredEvent"/> to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    [ChannelV2("light/measured"), PublishOperationV2(OperationId = "NotifyLightMeasured", Summary = "Notifies remote consumers about environmental lighting conditions for a particular streetlight."), TagV2("light", "A tag for light-related operations"), TagV2("measurement", "A tag for measurement-related operations")]
    [OperationV3("sendLightMeasurement", V3OperationAction.Send, "#/channels/lightingMeasuredMQTT", Description = "Notifies remote **consumers** about environmental lighting conditions for a particular **streetlight**."), TagV3(Reference = "#/components/tags/measurement")]
    public async Task PublishLightMeasured(LightMeasuredEvent e, CancellationToken cancellationToken = default)
    {
        var message = new MqttApplicationMessage()
        {
            Topic = "onLightMeasured",
            ContentType = "application/json",
            PayloadSegment = Encoding.UTF8.GetBytes(this.Serializer.SerializeToText(e))
        };
        await this.MqttClient.PublishAsync(message, cancellationToken);
    }

    /// <summary>
    /// Handles the specified <see cref="LightMeasuredEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="LightMeasuredEvent"/> to handle</param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    [ChannelV2("light/measured"), SubscribeOperationV2(OperationId = "OnLightMeasured", Summary = "Inform about environmental lighting conditions for a particular streetlight"), TagV2("light", "A tag for light-related operations"), TagV2("measurement", "A tag for measurement-related operations")]
    [OperationV3("receiveLightMeasurement", V3OperationAction.Receive, "#/channels/lightingMeasuredMQTT"), TagV3(Reference = "#/components/tags/measurement")]
    protected Task OnLightMeasured(LightMeasuredEvent e)
    {
        this.Logger.LogInformation("Event received:\r\n{json}", this.Serializer.SerializeToText(e));
        return Task.CompletedTask;
    }

}
