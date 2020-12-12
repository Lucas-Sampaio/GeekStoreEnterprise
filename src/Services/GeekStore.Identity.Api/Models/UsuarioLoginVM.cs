using System.ComponentModel.DataAnnotations;

namespace GeekStore.Identity.Api.Models
{
    public class UsuarioLoginVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100,ErrorMessage = "O campo {0} precisa ter entre {2} e {1}",MinimumLength = 6)]
        public string Senha { get; set; }
    }
}
