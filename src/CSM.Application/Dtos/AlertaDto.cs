namespace CSM.Application.Dtos
{
    public class AlertaDto
    {
        public Guid Id { get; set; }
        public string Simbolo { get; set; }
        public decimal? PrecoAlvo { get; set; }
        public decimal? NivelVolume { get; set; }
        public decimal? NivelMacd { get; set; }
        public decimal? NivelRsi { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
