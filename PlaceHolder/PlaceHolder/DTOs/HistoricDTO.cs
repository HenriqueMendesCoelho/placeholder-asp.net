namespace PlaceHolder.DTOs
{
    public class HistoricDTO
    {
        public string Text { get; set; }

        public HistoricDTO() {  }

        public HistoricDTO(string text)
        {
            this.Text = text;
        }
    }
}
