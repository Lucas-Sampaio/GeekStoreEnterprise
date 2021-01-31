using GeekStore.Core.DomainObjects;
using System;

namespace GeekStore.Pagamentos.Api.Models
{
    public class Transacao : EntityBase
    {
        public string CodigoAutorizacao { get; set; }
        public string BandeiraCartao { get; set; }
        public DateTime? DataTransacao { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal CustoTransacao { get; set; }
        public EStatusTransacao Status { get; set; }
        public string TID { get; set; } // Id
        public string NSU { get; set; } // Meio (paypal)

        public Guid PagamentoId { get; set; }

        // EF Relation
        public Pagamento Pagamento { get; set; }
    }
}
