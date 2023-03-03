using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookish.Models;

public class CopyOfBookRequestModel
{
    public string Condition { get; set; }

    // FOREIGN KEY (Catalogue_id) REFERENCES Catalogue(id)
    public int Catalogue_id { get; set; }
    [ForeignKey("Catalogue_id")]
    public Catalogue Catalogue { get; set; }


}