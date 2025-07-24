namespace ExtraClasesApp.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalTutores { get; set; }
        public int TotalEstudiantes { get; set; }
        public int TotalClases { get; set; }
        public int TotalNotificaciones { get; set; }

        public List<string> ChartLabels { get; set; } = new();
        public List<int> ChartClases { get; set; } = new();
        public List<int> ChartNotis { get; set; } = new();

        public string Desde { get; set; } = string.Empty;
        public string Hasta { get; set; } = string.Empty;
    }
}
