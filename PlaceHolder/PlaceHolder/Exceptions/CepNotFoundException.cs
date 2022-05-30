namespace PlaceHolder.Exceptions
{
    [Serializable]
    public class CepNotFoundException : Exception
    {
        public CepNotFoundException() : base("CEP not found") { }

    }
}
