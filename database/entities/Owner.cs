namespace ServiceStationApi.database.entities;

public partial class Owner
{
    public int IdUser { get; set; }

    public string NameOwner { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
