using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppCliente.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; } 

        [Required(ErrorMessage ="Campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo nome da mãe é obrigatório.")]
        [DisplayName("Nome da mãe")]
        public string NomeMae { get; set; }

        [Required(ErrorMessage = "Campo endereço é obrigatório.")]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo idade é obrigatório.")]
        public int? Idade { get; set; }

        public string? Sexo { get; set; }
        [DisplayName("Estado Civil")]
        public string? EstadoCivil { get; set; }
    }
}
