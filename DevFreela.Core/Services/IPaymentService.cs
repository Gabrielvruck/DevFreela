using DevFreela.Core.Dtos;

namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentInfoDto paymentInfoDto);
    }
}
