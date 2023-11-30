﻿using MQTTnet;
using MQTTnet.Client;
using Neuroglia.AsyncApi;
using Neuroglia.Serialization;
using StreetLightsApi.Server.Messages;
using System.Text;

namespace StreetLightsApi.Server.Services;

[AsyncApi("Movement Sensor API", "1.0.0", Description = "The Movement Sensor API allows you to get remotely notified about movements captured by sensors.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
public class MovementSensorService(ILogger<MovementSensorService> logger, IJsonSerializer serializer)
        : BackgroundService
{
    protected ILogger Logger { get; } = logger;

    protected IJsonSerializer Serializer { get; } = serializer;

    protected IMqttClient MqttClient { get; private set; } = null!;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.MqttClient = new MqttFactory().CreateMqttClient();
        var options = new MqttClientOptions() { ChannelOptions = new MqttClientTcpOptions() { Server = "test.mosquitto.org" } };
        await this.MqttClient.ConnectAsync(options, stoppingToken).ConfigureAwait(false);
        stoppingToken.Register(async () => await this.MqttClient.DisconnectAsync());
        this.MqttClient.ApplicationMessageReceivedAsync += async message =>
        {
            var e = this.Serializer.Deserialize<MovementDetectedEvent>(Encoding.UTF8.GetString(message.ApplicationMessage.PayloadSegment))!;
            await this.OnMovementDetected(e).ConfigureAwait(false);
            await message.AcknowledgeAsync(stoppingToken).ConfigureAwait(false);
        };
        await this.MqttClient.SubscribeAsync("OnMovementDetected", cancellationToken: stoppingToken).ConfigureAwait(false);
    }

    [Tag("movement", "A tag for movement-related operations"), Tag("sensor", "A tag for sensor-related operations")]
    [Channel("movement/detected"), SubscribeOperation(OperationId = "OnMovementDetected", Summary = "Inform about movement captured by sensors")]
    protected Task OnMovementDetected(MovementDetectedEvent e)
    {
        this.Logger.LogInformation("Movement detected by sensor with id '{sensorId}' at {sentAt}", e.SensorId, e.SentAt);
        return Task.CompletedTask;
    }

}
