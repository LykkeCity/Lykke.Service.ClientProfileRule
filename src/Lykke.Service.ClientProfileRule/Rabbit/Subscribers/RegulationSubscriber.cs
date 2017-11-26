using System;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Common;
using Common.Log;
using Lykke.RabbitMqBroker;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.ClientProfileRule.Core.Domain;
using Lykke.Service.ClientProfileRule.Core.Exceptions;
using Lykke.Service.ClientProfileRule.Core.Services;
using Lykke.Service.ClientProfileRule.Rabbit.Messages.Incoming;
using Lykke.Service.ClientProfileRule.Settings.ServiceSettings.Rabbit;

namespace Lykke.Service.ClientProfileRule.Rabbit.Subscribers
{
    public class RegulationSubscriber : IStartable, IStopable
    {
        private readonly ILog _log;
        private readonly IRegulationRuleService _regulationRuleService;
        private readonly RegulationQueue _settings;
        private RabbitMqSubscriber<ClientRegulationsMessage> _subscriber;

        public RegulationSubscriber(ILog log, IRegulationRuleService regulationRuleService, RegulationQueue settings)
        {
            _log = log;
            _regulationRuleService = regulationRuleService;
            _settings = settings;
        }

        public void Start()
        {
            var settings = RabbitMqSubscriptionSettings
                .CreateForSubscriber(_settings.ConnectionString, _settings.Exchange, "client-profile-rule")
                .MakeDurable();

            settings.DeadLetterExchangeName = null;

            _subscriber = new RabbitMqSubscriber<ClientRegulationsMessage>(settings,
                    new ResilientErrorHandlingStrategy(_log, settings,
                        TimeSpan.FromSeconds(10),
                        next: new DeadQueueErrorHandlingStrategy(_log, settings)))
                .SetMessageDeserializer(new JsonMessageDeserializer<ClientRegulationsMessage>())
                .SetMessageReadStrategy(new MessageReadQueueStrategy())
                .Subscribe(ProcessMessageAsync)
                .CreateDefaultBinding()
                .SetLogger(_log)
                .Start();
        }

        public void Dispose()
        {
            _subscriber?.Dispose();
        }

        public void Stop()
        {
            _subscriber.Stop();
        }

        private async Task ProcessMessageAsync(ClientRegulationsMessage message)
        {
            var clientRegulations = Mapper.Map<ClientRegulations>(message);

            try
            {
                await _regulationRuleService.UpplyAsync(clientRegulations);

                await _log.WriteInfoAsync(nameof(RegulationSubscriber), nameof(ProcessMessageAsync),
                    $"Client profiles requested. {nameof(message)}: {message.ToJson()}");
            }
            catch (RegulationRuleNotFoundException exception)
            {
                await _log.WriteWarningAsync(nameof(RegulationSubscriber), nameof(ProcessMessageAsync),
                    $"Regulation rule does not exists for regulation '{exception.RegulationId}'. {nameof(message)}: {message.ToJson()}");
            }
            catch (Exception exception)
            {
                throw new Exception(
                    $"An error occurred during upplying client profiles. {nameof(message)}: {message.ToJson()}",
                    exception);
            }
        }
    }
}
