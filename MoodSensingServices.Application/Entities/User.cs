using System;
using System.Collections.Generic;

namespace MoodSensingServices.Application.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int Mood { get; set; }

    public string Image { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;
}
