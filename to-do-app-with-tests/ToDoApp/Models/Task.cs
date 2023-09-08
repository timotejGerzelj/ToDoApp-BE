// Models/Task.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ToDoApp.Models
{
    [Table("tasks")]
    public class Task
    {    
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Naslov je obvezen.")]
    [Column("naslov")]
    public required string Naslov { get; set; }

    [Required(ErrorMessage = "Opis je obvezen.")]
    [Column("opis")]
    public required string Opis { get; set; } 

    [Required(ErrorMessage = "DatumUstvarjanja polje je obvezno.")]
    [Column("datum_ustvarjanja")]
    [DataType(DataType.Date)]
    public DateTime DatumUstvarjanja { get; set;}

    [Required(ErrorMessage = "Vrednost Opravljeno je obvezna.")]
    [Column("opravljeno")]
    public bool Opravljeno { get; set; }

    public static implicit operator Task?(System.Threading.Tasks.Task? v)
    {
        throw new NotImplementedException();
    } }
}