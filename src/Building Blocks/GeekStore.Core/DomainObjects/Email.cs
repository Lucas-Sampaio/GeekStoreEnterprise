using System.Text.RegularExpressions;

namespace GeekStore.Core.DomainObjects
{
    public class Email
    {
        public const int MaxLenght = 254;
        public const int MinLenght = 5;
        public string Endereco { get; private set; }
        protected Email()
        {

        }
        public Email(string endereco)
        {
            if (!Validar(endereco)) throw new DomainException("Email inválido");
            Endereco = endereco;
        }
        public static bool Validar(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            return rg.IsMatch(email);
        }
    }
}
