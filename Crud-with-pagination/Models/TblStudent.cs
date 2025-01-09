using System;
using System.Collections.Generic;

namespace Crud_with_pagination.Models;

public partial class TblStudent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Department { get; set; } = null!;

    public DateTime Date { get; set; }

    
}
