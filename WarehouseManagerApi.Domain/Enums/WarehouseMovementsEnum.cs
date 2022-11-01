using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Enums;
public enum WarehouseMovementsEnum
{
    [Description("Przyjęcie towaru")]
    AcceptTheGood = 0,

    [Description("Przemieszczenie")]
    Displacement = 1,

    [Description("Skierowanie na reklamcję")]
    Complaint = 2
}
