using DevFreela.Core.Dtos;
using DevFreela.Core.Services;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService messageBusService;
        private const string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService messageBusService)
        {
            this.messageBusService = messageBusService ?? throw new ArgumentNullException(nameof(messageBusService));
        }

        public void ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDto);
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);
            messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
