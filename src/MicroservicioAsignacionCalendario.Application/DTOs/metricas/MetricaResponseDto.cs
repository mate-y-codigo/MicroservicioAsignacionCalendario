namespace MicroservicioAsignacionCalendario.Application.DTOs.metricas
{
    public class MetricaResponseDto
    {
        public List<GraficoItemDto> Grafiquitos { get; set; }
        public List<ComplianceHistoryDto> ComplianceHistory { get; set; }
        public List<StrengthDataDto> StrengthData { get; set; }
        public List<LoadDataDto> LoadData { get; set; }
        public List<WeeklyComplianceDto> WeeklyCompliance { get; set; }
        public List<SessionGapDataDto> SessionGapData { get; set; }
        public List<PrsDto> Prs { get; set; }
    }
}