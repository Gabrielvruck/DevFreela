using DevFreela.Core.Dtos;

namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        void ProcessPayment(PaymentInfoDto paymentInfoDto);
    }
}
