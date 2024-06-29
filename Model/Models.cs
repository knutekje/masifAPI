using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel;
using Microsoft.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace masifAPI.Model;

public class FoodItem{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("Id")]
    public long Id { get; set; }

    [Column("Title")]
    public required string Title { get; set; }
    
    [Precision(18, 2)]
    [Column("Price")]
    public decimal Price { get; set; }
   
    [Column("Unit")]
    public required string Unit { get; set; }
   
    [Column("Supplier")]
    public required string Supplier { get; set; }
   
    [Column("ExternalID")]
    public required string ExternalID { get; set; }


}

public class Report{


    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("Id")]
    public long Id { get; set; }
    
    [Column("PictureId")]
    public required long PictureId { get; set; }
    
    [Column("ReportedDate")]
    public DateTime ReportedDate { get; set; }

    [Column("IncidentDate")]
    public DateTime IncidentDate { get; set; }
    
    [Precision(18, 2)]
    [Column("Quantity")]
    public decimal Quantity { get; set; }
    
    [Column("UserID")]
    public long UserID { get; set; }


    [Column("FoodID")]
    public long FoodID { get; set; }


    [Column("Description")]
    public required string Description { get; set; }

}



[PrimaryKey(nameof(ReportID), nameof(IdentityUser))]
public class Incident{
[Column("ReportID")]
public long ReportID { get; set; }

[Column("IdentityUser")]
public long IdentityUser { get; set; }


[Column("FoodID")]
public long FoodID { get; set; }

[Precision(18, 2)]
[Column("ItemPrice")]
public decimal ItemPrice { get; set;}

[Precision(18, 2)]
[Column("ValueIncident")]
public decimal ValueIncident { get; set; } = 00000;


}

public class Picture{

    [Column("Id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public long Id { get; set; }

    [Column("FileName")]
    public required string FileName { get; set;}


    [Column("FilePath")]
    public required string FilePath { get; set;}


    [Column("Description")]
    public required string Description { get; set;}

    
    

    
}




