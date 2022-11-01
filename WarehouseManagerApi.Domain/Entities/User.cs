using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int IdRole { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public Role Role { get; set; } = new();
}
