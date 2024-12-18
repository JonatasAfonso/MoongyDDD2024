using System.Net;

namespace ECommerce.Infra.WebServices
{
    public class GatewayPagamentoConnector
    {
        public async Task<bool> SibsInvoker()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://sibs.com.br");
            var response = await client.GetAsync("/api/v1/pagamento");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }
    }
}
