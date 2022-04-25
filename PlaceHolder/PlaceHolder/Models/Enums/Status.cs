namespace PlaceHolder.Models
{
    public class Status
    {

        public enum StatusEnum
        {
            ABERTO,
            EM_ATENDIMENTO,
            EM_ESPERA,
            RESOLVIDO,
            FECHADO,
            CANCELADO
        }

        public Status() { }
    }
}
