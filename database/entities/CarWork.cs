namespace ServiceStationApi.database.entities;

public partial class CarWork 
{
    public int IdWork { get; set; }

    public int IdAuto { get; set; }

    public string Mileage { get; set; }

    public string DescriptionWork { get; set; }

    public DateTime? Date { get; set; }

    public string Note { get; set; }

    public virtual Car IdAutoNavigation { get; set; }
}
