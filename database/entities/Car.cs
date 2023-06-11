namespace ServiceStationApi.database.entities;

public partial class Car
{
    public int IdAuto { get; set; }

    public int IdUser { get; set; }

    public DateTime YearsOfManufacture { get; set; }

    public string NameAuto { get; set; }

    public string VinCode { get; set; }

    public virtual ICollection<CarWork> CarWorks { get; set; } = new List<CarWork>();

    public virtual Owner IdUserNavigation { get; set; }
}
