using System.ComponentModel.DataAnnotations;


namespace DAL.Models
{
    public abstract class Entity
    {
        [Key]
        public  virtual int Id { get; set; }
    }
}
