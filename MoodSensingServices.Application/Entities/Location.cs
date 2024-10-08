using System;
using System.Collections.Generic;

namespace MoodSensingServices.Application.Entities;

public partial class Location
{
    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid LocationId { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
