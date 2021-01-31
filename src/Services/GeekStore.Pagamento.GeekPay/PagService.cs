namespace GeekStore.Pagamentos.GeekPay
{
    public class PagService
    {
        public readonly string ApiKey;
        public readonly string EncryptionKey;

        public PagService(string apiKey, string encryptionKey)
        {
            ApiKey = apiKey;
            EncryptionKey = encryptionKey;
        }
    }
}