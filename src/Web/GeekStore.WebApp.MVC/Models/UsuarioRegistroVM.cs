using System.ComponentModel.DataAnnotations;

namespace GeekStore.WebApp.MVC.Models
{
    public class UsuarioRegistroVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        [DataType(DataType.Password)]
        public string SenhaConfirmacao { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
}
