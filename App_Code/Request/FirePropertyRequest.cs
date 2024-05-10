using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FirePropertyRequest: BaseRequest
{
    public decimal BodyOfWaterDistance { get; set; }
    public string BoundaryFront { get; set; }
    public string BoundaryLeft { get; set; }
    public string BoundaryRear { get; set; }
    public string BoundaryRight { get; set; }
    public decimal FloorArea { get; set; }
    public string LossDetails { get; set; }
    public string MortgageeName { get; set; }
    public string NameOfResident { get; set; }
    public string NatureOfOccupancy { get; set; }
    public long NoOfStoreys { get; set; }
    public string OwnershipStatus { get; set; }
    public string PropertyAddress { get; set; }
    public string PropertyProvince { get; set; }
    public string PreviousLossStatus { get; set; }
    public string PropertyCity { get; set; }
    public long PropertyAge { get; set; }
    public string PropertyMortgageStatus { get; set; }
    public string TypeBeams { get; set; }
    public string TypeColumns { get; set; }
    public string TypeExteriorWalls { get; set; }
    public string TypeInnerPartitions { get; set; }
    public string TypeOfHome { get; set; }
    public string TypeRoof { get; set; }


}

