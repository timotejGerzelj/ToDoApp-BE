namespace ToDoApp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Interfaces;

[Table("todo_tasks")]
public class Task : IEntity
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
    }
}