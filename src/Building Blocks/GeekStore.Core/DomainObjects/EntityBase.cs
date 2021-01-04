using GeekStore.Core.Messages;
using System;
using System.Collections.Generic;

namespace GeekStore.Core.DomainObjects
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();
        public void AdicionarEvento(Event evento)
        {
            _notificacoes = _notificacoes ?? new List<Event>();
            _notificacoes.Add(evento);
        }
        public void RemoverEvento(Event eventItem)
        {
            _notificacoes?.Remove(eventItem);
        }
        public void LimparEventos()
        {
            _notificacoes?.Clear();
        }
        public override string ToString()
        {
            return $"{GetType().Name} [Id = {Id}]";
        }
       
        #region Comparacoes
        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(this, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }
        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 97 + Id.GetHashCode();
        }
        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }
        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }
        #endregion
    }
}
 